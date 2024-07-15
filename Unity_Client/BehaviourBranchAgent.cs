using BehaviourBranch.Behaviours;

namespace BehaviourBranch
{
    public abstract class BehaviourBranchAgent
    {
        public abstract float ConvertVariable(string variableName);

        public abstract BehaviourRunner CreateInstance(
            BehaviourBranchAgent agentInterface,
            NodeAction nodeAction
        );
    }
}
