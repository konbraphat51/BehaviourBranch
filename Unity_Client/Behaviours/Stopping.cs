using BehaviourBranch.Variables;
using Simulation.Objects.Fighters;

namespace BehaviourBranch.Behaviours
{
    public class Stopping : BehaviourRunner
    {
        public Stopping(Fighter fighter, NodeAction action)
            : base(fighter, action)
        {
            satisfied = true;
        }

        protected override void Start()
        {
            //do nothing
        }

        public override void Update()
        {
            //do nothing
        }
    }
}
