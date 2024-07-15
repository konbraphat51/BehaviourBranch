import json
import requests
from pathlib import Path
from Behaviour.Config import Config
from Behaviour.Utils import Verboser


class LLMFetcher:
    def __init__(self, config: Config) -> None:
        self.config = config
        self.verboser = Verboser(config)

        self._set_up()

    def fetch_next_action(self, prompt_sending: str) -> list[str]:
        texts_generated = self._fetch_texts(prompt_sending)
        texts_validated = self._clean_output(texts_generated)

        return texts_validated

    def _set_up(self) -> None:
        self.headers = {
            "Authorization": f"Bearer {self.config.token_llm}",
            "Content-Type": "application/json",
        }

    def _fetch_texts(self, prompt_sending: str) -> list[str]:
        self.verboser.start_timer()

        body = {
            "model": "codellama/CodeLlama-70b-hf",
            "prompt": prompt_sending,
            "max_tokens": 120,
            "temperature": 0.1,
            "stream": True,
        }

        session = requests.Session()
        session.headers.update(self.headers)

        response = session.post(
            "https://api.together.xyz/v1/completions",
            data=json.dumps(body),
            stream=True,
        )

        text_target = ""
        bracket_opening = 1
        bracket_closing = 0
        completed = False
        for chunk in response.iter_lines(chunk_size=None, decode_unicode=True):
            # detect the end of the response
            if chunk == "" or chunk.endswith("[DONE]"):
                continue

            # there is "data:" at the head of the response
            chunk = chunk.replace("data:", "")

            chunk = json.loads(chunk)
            came = chunk["choices"][0]["text"] or ""

            for char in came:
                # count "["
                bracket_opening += char.count("[")

                # count "]"
                bracket_closing += char.count("]")

                text_target += char

                if bracket_opening == bracket_closing:
                    completed = True
                    break

            if completed:
                break

        self.verboser.finish_timer("POST connection")

        texts = [text_target]

        cnt = 0
        for text in texts:
            self.verboser.print(f"[text_fetched{cnt}]", 2)
            self.verboser.print(text, 2)

            cnt += 1

        return texts

    def _clean_output(self, texts_generated: list[str]) -> list[str]:
        """
        Clean the generated text.

        Highly dependent on the format of the template and the generated text.
        """

        # extract the action
        texts_extracted = []
        for text_generated in texts_generated:
            try:
                texts_extracted.append(self._extract_action(text_generated))
            except ValueError:
                pass

        # validate the action
        texts_validated = []
        for text_extracted in texts_extracted:
            try:
                if self._validate_generation(text_extracted):
                    texts_validated.append(text_extracted)
            except ValueError as e:
                print(e)

        cnt = 0
        for text in texts_validated:
            self.verboser.print(f"[texts validated {cnt}]", 2)
            self.verboser.print(text, 2)

            cnt += 1

        return texts_validated

    def _extract_action(self, text_generated: str) -> str:
        """
        Extract the action from the generated text.

        Highly dependent on the format of the template and the generated text.
        """

        text_processed = "[\n" + text_generated

        bracket_opening = 0
        bracket_closing = 0

        for cnt in range(len(text_processed)):
            if text_processed[cnt] == "[":
                bracket_opening += 1
            elif text_processed[cnt] == "]":
                bracket_closing += 1

            if bracket_opening == bracket_closing:
                break

        text_extracted = text_processed[: cnt + 1]

        return text_extracted

    def _validate_generation(self, text_generated: str) -> bool:
        """
        Check Executableness of the generated text.
        """
        # TODO: implement
        return True
