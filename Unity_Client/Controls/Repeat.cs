using BehaviourBranch.Variables;

namespace BehaviourBranch.Controls
{
    public class Repeat : Control
    {
        public Repeat(NodeControl nodeControl)
            : base(nodeControl)
        {
            nodeControl.flagAfterSatisfaction = true;
        }

        public static bool JudgeEndRepeatition(Node newBranchRoot)
        {
            Node nodeLooking = newBranchRoot;
            while (nodeLooking.nodeNext != null)
            {
                if (nodeLooking is NodeAction)
                {
                    return true;
                }

                nodeLooking = nodeLooking.nodeNext;
            }

            return false;
        }

        public override void ExecuteFirst(
            BehaviourBranchController ai,
            BehaviourBranchAgent agentInterface
        )
        {
            int repitition = (int)agentInterface.ConvertVariable(nodeThis.args[0]);

            ai.StartRepeating(nodeThis.nodeNext, repitition);
        }

        public override void ExecuteUpdate(BehaviourBranchController behaviourBranchAI) { }
    }
}
