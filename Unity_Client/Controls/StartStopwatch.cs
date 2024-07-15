namespace BehaviourBranch.Controls
{
    public class StartStopwatch : Control
    {
        public StartStopwatch(NodeControl nodeControl)
            : base(nodeControl) { }

        public override void ExecuteFirst(
            BehaviourBranchController ai,
            BehaviourBranchAgent agentInterface
        )
        {
            //stop repeating
            ai.StartTime();
        }

        public override void ExecuteUpdate(BehaviourBranchController behaviourBranchAI) { }
    }
}
