from __future__ import annotations
import logging
import traceback
import json
import time
from contextlib import contextmanager
from concurrent.futures import ThreadPoolExecutor
import boto3
from Behaviour.Config import Config
from Behaviour.BehaviourController import BehaviourController
from CommandTable import Command

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)


@contextmanager
def timer(name: str):
    start = time.time()
    yield
    print(f"{name}: {time.time() - start:.2f} sec")


def log_error(e: Exception) -> None:
    logger.error(
        e.__class__.__name__
        + ": "
        + str(e)
        + " [traceback] "
        + traceback.format_exc()
    )


def lambda_handler(event, context) -> dict:
    try:
        validate_request(event["body"])

        body = json.loads(event["body"])

        def _get_branch():
            with timer("get_behaviour_branch"):
                branch = get_behaviour_branch(body["command"])
                
            return branch

        def _access_db():
            with timer("access_dynamodb"):
                dynamodb = boto3.resource("dynamodb")
                command_table = dynamodb.Table("PokeAiCommands")

            return command_table

        with timer("threading"):
            with ThreadPoolExecutor() as executor:
                future_db = executor.submit(_access_db)
                future_branch = executor.submit(_get_branch)
                
                branch = future_branch.result()
                command_table = future_db.result()

        branch = {
            "nodes": branch,
            "commandId": body["commandId"],
        }

        with timer("record_command"):
            record_command(
                command_table,
                event["requestContext"]["authorizer"]["claims"]["sub"],
                body["command"],
                branch["nodes"],
                body["battleId"],
            )

        return {"statusCode": 200, "body": json.dumps(branch)}

    except ValidationException as e:
        log_error(e)

        return {"statusCode": 400, "body": "ValidationException"}

    except Exception as e:
        # log error
        log_error(e)

        return {"statusCode": 500, "body": "internal lambda error"}


def validate_request(body: str) -> None:
    # try JSON conversion
    try:
        body = json.loads(body)
    except:
        raise ValidationException()

    for key in [
        "command",
        "commandId",
        "battleId",
    ]:
        if key not in body:
            raise ValidationException()

    # validate command
    if (
        # more than 80 letters
        (len(body["command"]) > 80)
        # only English character
        or (not body["command"].isascii())
    ):
        raise ValidationException()

    # validation succeeded
    return


def get_behaviour_branch(command: str) -> list[dict]:
    config = Config(language_speech="en")
    controller = BehaviourController(config)

    return controller.command(command)


def record_command(
    command_table, user_id: str, command_text: str, branch: str, battle_id: int
) -> None:
    command = Command.create(user_id, command_text, branch, battle_id)

    command_table.put_item(Item=command.to_dict())


class ValidationException(Exception):
    pass
