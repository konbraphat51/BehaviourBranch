"""
Module where executing the generated behaviour code

This highly depends on the prompt template and behaviour code generated by the LLMFetcher
"""

from Behaviour.BehaviourController.BehaviourNodes import (
    BehaviourNode,
    DummyNode,
    ActionNode,
    ConditionNode,
    ControlNode,
)
from Behaviour.BehaviourController.BehaviourBranch import BehaviourBranch


def read_behaviour_code(code: str) -> BehaviourBranch:
    """
    Read the behaviour code and return the root node of the behaviour branch
    """
    root = DummyNode()
    branch = BehaviourBranch(root)

    entire_list = eval(code)

    for component in entire_list:
        # if component is a single node...
        if isinstance(component, NodeAdder):
            branch.connect_next(component.to_node())
        # if component is a list of nodes...
        elif isinstance(component, list):
            branch.connect_next(_process_condition_list(component))
        else:
            raise TypeError(f"Unexpected type {type(component)}")

    # exclude dummy node
    branch = BehaviourBranch(root.next)

    return branch


def _process_condition_list(condition_list: list) -> BehaviourBranch:
    """
    Process a list of condition nodes and return the root node of the subbranch
    """
    condition = condition_list[0].to_node()
    # assertion
    if not isinstance(condition, ConditionNode):
        raise TypeError(f"Unexpected type {type(condition)}")

    root_dummy = DummyNode()
    branch = BehaviourBranch(root_dummy)

    for adder in condition_list[1:]:
        # if condition is a single node...
        if isinstance(adder, NodeAdder):
            branch.connect_next(adder.to_node())
        # if condition is a list of nodes...
        elif isinstance(adder, list):
            branch.connect_next(_process_condition_list(adder))
        else:
            raise TypeError(f"Unexpected type {type(adder)}")

    # overwrite the dummy node with a condition node
    condition.connect_true(root_dummy.next)

    return BehaviourBranch(condition)


# functions for read behaviour code


def add_action_behaviour(action, *args):
    adder = ActionAdder(action, list(args))

    return adder


def add_condition_behaviour(variable0, condition, variable1):
    return ConditionAdder(variable0, condition, variable1)


def add_control_behaviour(control, *args):
    adder = ControlAdder(control, list(args))

    return adder


class NodeAdder:
    def __init__(self, node_type: str):
        self.node_type = node_type


class ActionAdder(NodeAdder):
    def __init__(self, action: str, args: list):
        super().__init__("action")
        self.action = action
        self.args = args.copy()

    def to_node(self) -> ActionNode:
        return ActionNode(self.action, self.args)


class ConditionAdder(NodeAdder):
    def __init__(self, variable0, condition, variable1):
        super().__init__("condition")
        self.variable0 = variable0
        self.condition = condition
        self.variable1 = variable1

    def to_node(self) -> ConditionNode:
        return ConditionNode(self.variable0, self.condition, self.variable1)


class ControlAdder(NodeAdder):
    def __init__(self, control, args):
        super().__init__("control")
        self.control = control
        self.args = args.copy()

    def to_node(self) -> ControlNode:
        return ControlNode(self.control, self.args)
