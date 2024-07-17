import os
from typing import Literal
from pathlib import Path
from pynput.keyboard import Key


class Config:
    def __init__(self, language_speech: Literal["ja", "en"] = "ja"):
        self.library_directory = Path(__file__).parent.parent
        self.cache_directory = self.library_directory / "cache"
        self.audio_directory = self.cache_directory / "audio"

        # language
        self.language_speech = language_speech

        # speech recognition
        self.key_listen = Key.space

        # whisper
        self.whisper_model_size = (
            "zh-plus/faster-whisper-large-v2-japanese-5k-steps"
        )
        self.whisper_device = "cuda"

        # behaviour
        self.prompt_path = {
            "p_ja": Path(__file__).parent / "CodePrompt" / "CodePromptJP.txt",
            "p_en": Path(__file__).parent / "CodePrompt" / "CodePromptEN.txt",
            "fetching_en": Path(__file__).parent / "CodePrompt" / "fetching_prompt_en.txt",
            "fetching_jp": Path(__file__).parent / "CodePrompt" / "fetching_prompt_jp.txt",
        }
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
