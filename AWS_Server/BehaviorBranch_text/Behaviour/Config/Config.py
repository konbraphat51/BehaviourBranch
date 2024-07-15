import os
from typing import Literal
from pathlib import Path


class Config:
    def __init__(self, language_speech: Literal["ja", "en"] = "ja"):
        self.library_directory = Path(__file__).parent.parent
        self.cache_directory = self.library_directory / "cache"
        self.audio_directory = self.cache_directory / "audio"

        # language
        self.language_speech = language_speech

        # whisper
        self.whisper_model_size = (
            "zh-plus/faster-whisper-large-v2-japanese-5k-steps"
        )
        self.whisper_device = "cuda"

        # behaviour
        if self.language_speech == "ja":
            self.prompt_path = (
                Path(__file__).parent / "CodePrompt" / "CodePromptJP.txt"
            ).as_posix()
        elif self.language_speech == "en":
            self.prompt_path = (
                Path(__file__).parent / "CodePrompt" / "CodePromptEN.txt"
            ).as_posix()
        self.prompt_alias = "[PROMPT]"

        # tokens
        self.token_llm = os.environ["TOKEN_LLM"]

        # verbose
        # 0: no verbose
        # 1: verbose
        # 2: debug log
        self.verbose = 1

        # logging
        self.logging = False
        self.log_file = (Path(__file__).parent / "log.txt").as_posix()
