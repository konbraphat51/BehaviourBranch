using BehaviourBranch.Variables;
using Simulation;
using Simulation.Objects.Fighters;

namespace BehaviourBranch.Behaviours
{
    public class RunningTowardsTarget : BehaviourRunner
    {
        private const float REQUIRED_TIME = 0.5f;

        private float angle;

        public RunningTowardsTarget(BehaviourBranchAgent fighter, NodeAction action)
            : base(fighter, action)
        {
            satisfied = false;
        }

        protected override void Start()
        {
            angle = Variable.ConvertVariable(nodeAction.args[0]);

            Timer timer = new Timer(REQUIRED_TIME, OnRequiredTimeFinished);
        }

        public override void Update()
        {
            fighter.TurnAndRun(1f, angle, true);
        }

        private void OnRequiredTimeFinished()
        {
            satisfied = true;
        }
    }
}
