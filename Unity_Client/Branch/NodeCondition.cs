using BehaviourBranch.Conditions;

namespace BehaviourBranch
{
    public class NodeCondition : Node
    {
        public string conditionName;
        public Node nodeTrue;
        public Condition condition;

        public override string GetName()
        {
            return conditionName;
        }

        public void ConnectTrue(Node node)
        {
            nodeTrue = node;
            node.nodePrevious = this;
        }

        public Node GetTrueBottom()
        {
            if (nodeTrue == null)
            {
                return null;
            }

            Node node = nodeTrue;
            while (node.nodeNext != null)
            {
                node = node.nodeNext;
            }
            return node;
        }

        public bool Check(BehaviourBranchAgent agentInterface)
        {
            //if this is the first time...
            if (condition == null)
            {
                //...create new condition instance
                condition = new Condition(agentInterface, this);
            }

            //check condition
            return condition.Check();
        }
    }
}
