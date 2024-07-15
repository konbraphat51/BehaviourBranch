using BehaviourBranch.Behaviours;
using BehaviourBranch.Variables;

namespace BehaviourBranch
{
    public abstract class BehaviourBranchAgent
    {
        /// <summary>
        /// Implement this method to convert variable name to Variable Node
        /// </summary>
        public float ConvertVariable(string variableName)
        {
            // if the variable is a float...
            if (float.TryParse(variableName, out float result))
            {
                // ...return it
                return result;
            }
            else if (variableName == "True")
            {
                return Variable.TRUE;
            }
            else if (variableName == "False")
            {
                return Variable.FALSE;
            }

            return ConvertVariableNode(variableName).Get();
        }

        /// <summary>
        /// Convert variable node name to Variable instance
        ///
        /// use switch case or something
        /// </summary>
        protected abstract Variable ConvertVariableNode(string variableName);

        public abstract BehaviourRunner CreateInstance(
            BehaviourBranchAgent agentInterface,
            NodeAction nodeAction
        );
    }
}
