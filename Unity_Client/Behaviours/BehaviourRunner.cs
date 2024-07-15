using System;
using UnityEngine;

namespace BehaviourBranch.Behaviours
{
    /// <summary>
    /// Base class for all behaviours.
    ///
    /// Show what the agentInterface should do in this behaviour state
    /// The agentInterface will be controlled by this as long as the behaviour is active
    /// </summary>
    public abstract class BehaviourRunner
    {
        public bool satisfied { get; protected set; } = true;

        protected BehaviourBranchAgent agentInterface;
        protected NodeAction nodeAction;

        public BehaviourRunner(BehaviourBranchAgent agentInterface, NodeAction nodeAction)
        {
            this.agentInterface = agentInterface;
            this.nodeAction = nodeAction;

            Start();
        }

        protected abstract void Start();
        public abstract void Update();
    }
}
