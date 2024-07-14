using UnityEngine;
using Simulation;
using Simulation.Objects.Fighters;

namespace AI.BehaviourBranch.Variables
{
    public class HeadingAngle : Variable
    {
        public HeadingAngle(FighterWatcher fighterWatcher)
            : base(fighterWatcher) { }

        public override float Get()
        {
            Vector3 positionRelative =
                fighterWatcher.target.transform.position - fighterWatcher.position;
            Vector3 headingDirection = fighterWatcher.transform.forward;

            float angleRelative = Utils.VectorToHorizontalAngle(positionRelative);
            float angleHeading = Utils.VectorToHorizontalAngle(headingDirection);

            float result = angleHeading - angleRelative;

            while (result < 0)
            {
                result += 360;
            }
            while (result > 360)
            {
                result -= 360;
            }

            return result;
        }
    }
}
