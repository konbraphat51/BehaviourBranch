from __future__ import annotations
import datetime
import time


class Command:
    def __init__(self) -> None:
        self.user_id: str = ""
        self.command_text: str = ""
        self.branch_made: str = ""
        self.battle_id: int = -1
        self.made_at: int = -1

    def to_dict(self) -> dict:
        return {
            "userId": self.user_id,
            "commandText": self.command_text,
            "branchMade": self.branch_made,
            "battleId": self.battle_id,
            "madeAt": self.made_at,
        }

    # static
    def from_dict(data: dict) -> Command:
        instance = Command()
        instance.user_id = data["userId"]
        instance.command_text = data["commandText"]
        instance.branch_made = data["branchMade"]
        instance.battle_id = data["battleId"]
        instance.made_at = data["madeAt"]

        return instance

    # static
    def create(
        user_id: str, command_text: str, branch_made: str, battle_id: int
    ) -> Command:
        instance = Command()
        instance.user_id = user_id
        instance.command_text = command_text
        instance.branch_made = branch_made
        instance.battle_id = battle_id
        instance.made_at = Command.create_time()

        return instance

    # static
    def create_time() -> int:
        # unix time
        now = datetime.datetime.now()
        return int(time.mktime(now.timetuple()))
