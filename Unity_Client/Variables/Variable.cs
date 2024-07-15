using System;
using UnityEngine;

namespace BehaviourBranch.Variables
{
    public abstract class Variable
    {
        public const float TRUE = 100f;
        public const float FALSE = -100f;

        protected BehaviourBranchAgent fighterWatcher;

        public Variable(BehaviourBranchAgent fighterWatcher)
        {
            this.fighterWatcher = fighterWatcher;
        }

        public abstract float Get();

        protected float EvaluateBool(bool condition)
        {
            if (condition)
            {
                return TRUE;
            }
            else
            {
                return FALSE;
            }
        }
    }
}
