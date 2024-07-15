using BehaviourBranch.Behaviours;

namespace BehaviourBranch
{
    public abstract class BehaviourBranchAgent
    {
        /// <summary>
        /// Implement this method to convert variable name to Variable Node
        /// </summary>
        public abstract float ConvertVariable(string variableName);

        public abstract BehaviourRunner CreateInstance(
            BehaviourBranchAgent agentInterface,
            NodeAction nodeAction
        );
    }
}
