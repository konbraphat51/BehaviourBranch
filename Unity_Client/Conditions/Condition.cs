using System;
using BehaviourBranch.Variables;
using UnityEngine;

namespace BehaviourBranch.Conditions
{
    /// <summary>
    /// Base class for all conditions.
    ///
    /// Check if the Fighter should change behaviour
    /// </summary>
    public class Condition
    {
        protected const float EQUAL_THRESHOLD = 0.5f;

        protected BehaviourBranchAgent agentInterface;
        protected NodeCondition nodeCondition;

        public Condition(BehaviourBranchAgent agentInterface, NodeCondition nodeCondition)
        {
            this.agentInterface = agentInterface;
            this.nodeCondition = nodeCondition;
        }

        public virtual bool Check()
        {
            switch (nodeCondition.conditionName)
            {
                case "<":
                    return agentInterface.ConvertVariable(nodeCondition.args[0])
                        < agentInterface.ConvertVariable(nodeCondition.args[1]);
                case ">":
                    return agentInterface.ConvertVariable(nodeCondition.args[0])
                        > agentInterface.ConvertVariable(nodeCondition.args[1]);
                case "==":
                    //approximate equality
                    float difference =
                        agentInterface.ConvertVariable(nodeCondition.args[0])
                        - agentInterface.ConvertVariable(nodeCondition.args[1]);
                    return Math.Abs(difference) < EQUAL_THRESHOLD;
                default:
                    Debug.Log("Unknown condition: " + nodeCondition.conditionName);
                    return true;
            }
        }
    }
}
