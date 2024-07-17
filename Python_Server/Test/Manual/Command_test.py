from Behaviour.Config import Config
from Behaviour.BehaviourController import BehaviourController

config = Config()
config.verbose = 2
config.logging = True

thinker = BehaviourController(config)

result = thinker.command("go right", "fetching_en")

print(result)
