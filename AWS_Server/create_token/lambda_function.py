import boto3
from botocore.exceptions import ClientError
from TokenTable import Token
from logging import getLogger

logger = getLogger(__name__)

def lambda_handler(event, context) -> dict:
    try:
        user_id = event["requestContext"]["authorizer"]["claims"]["sub"]
        token = create_token(user_id)

        return {"statusCode": 200, "token": token}
    except Exception as e:
        # log error
        logger.error(e.__class__.__name__ + ": " + str(e))
        
        return {"statusCode": 500, "body": "internal lambda error"}


def create_token(user_id: str) -> str:
    dynamodb = boto3.resource("dynamodb")
    table = dynamodb.Table("PokeAiTokens")

    new_token = Token.create(user_id)

    table.put_item(Item=new_token.to_dict())

    return new_token.token
