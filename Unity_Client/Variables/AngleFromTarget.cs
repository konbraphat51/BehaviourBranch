using Simulation;
using Simulation.Objects.Fighters;

namespace AI.BehaviourBranch.Variables
{
    public class AngleFromTarget : Variable
    {
        public AngleFromTarget(FighterWatcher fighterWatcher)
            : base(fighterWatcher) { }

        public override float Get()
        {
            float angleAbsoluteEnemyHeading = Utils.VectorToHorizontalAngle(
                fighterWatcher.target.transform.forward
            );

            float angleAbsoluteFromEnemy = Utils.VectorToHorizontalAngle(
                fighterWatcher.transform.position - fighterWatcher.target.transform.position
            );

            float result = angleAbsoluteEnemyHeading - angleAbsoluteFromEnemy;

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
