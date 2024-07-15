from __future__ import annotations
import datetime
import time
import random
import string


class Token:
    def __init__(self) -> None:
        self.user_id: str = ""
        self.token: str = ""
        self.made_at: int = -1

    def to_dict(self) -> dict:
        return {
            "userId": self.user_id,
            "token": self.token,
            "madeAt": self.made_at,
        }

    # static
    def from_dict(data: dict) -> Token:
        instance = Token()
        instance.user_id = data["userId"]
        instance.token = data["token"]
        instance.made_at = data["madeAt"]

        return instance

    # static
    def create(user_id: str) -> Token:
        instance = Token()
        instance.user_id = user_id
        instance.token = Token.create_token()
        instance.made_at = Token.create_time()

        return instance

    # static
    def create_time() -> int:
        # unix time
        now = datetime.datetime.now()
        return int(time.mktime(now.timetuple()))

    # static
    def create_token() -> str:
        # https://qiita.com/Scstechr/items/c3b2eb291f7c5b81902a
        return "".join(
            random.choices(string.ascii_letters + string.digits, k=15)
        )
