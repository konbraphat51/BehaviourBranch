using BehaviourBranch.Behaviours;

namespace BehaviourBranch
{
    public interface BehaviourBranchAgent
    {
        public float ConvertVariable(string variableName);

        public BehaviourRunner CreateInstance(
            BehaviourBranchAgent agentInterface,
            NodeAction nodeAction
        );
    }
}
