using BehaviourBranch.Variables;

namespace BehaviourBranch.Controls
{
    public class QuitRepeating : Control
    {
        public const string name = "QuitRepeating";

        public QuitRepeating(NodeControl nodeControl)
            : base(nodeControl) { }

        public static NodeControl CreateInstance()
        {
            return new NodeControl(name);
        }

        public override void ExecuteFirst(
            BehaviourBranchController ai,
            BehaviourBranchAgent agentInterface
        )
        {
            //stop repeating
            ai.StopRepeating();
        }

        public override void ExecuteUpdate(BehaviourBranchController behaviourBranchAI) { }
    }
}
