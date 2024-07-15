namespace BehaviourBranch.Controls
{
    public class StartStopwatch : Control
    {
        public StartStopwatch(NodeControl nodeControl)
            : base(nodeControl) { }

        public override void ExecuteFirst(BehaviourBranchAI ai, BehaviourBranchAgent agentInterface)
        {
            //stop repeating
            ai.StartTime();
        }

        public override void ExecuteUpdate(BehaviourBranchAI behaviourBranchAI) { }
    }
}
