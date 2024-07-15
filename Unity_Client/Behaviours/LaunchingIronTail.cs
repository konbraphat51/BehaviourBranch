using Simulation.Objects.Fighters;
using Simulation.Objects.Fighters.Actions;

namespace BehaviourBranch.Behaviours
{
    public class LaunchingIronTail : BehaviourRunner
    {
        private bool launched = false;
        private bool launchedFormerFrame = false;

        public LaunchingIronTail(Fighter fighter, NodeAction action)
            : base(fighter, action)
        {
            satisfied = false;
        }

        protected override void Start() { }

        public override void Update()
        {
            //if IronTail is not launched...
            if (!launched)
            {
                //...try to launch it
                launched = fighter.StartAction(typeof(IronTail));
                launchedFormerFrame = launched;
            }

            //satisfied one frame after the launch
            if (launchedFormerFrame)
            {
                satisfied = true;
                launchedFormerFrame = false;
            }
        }
    }
}
