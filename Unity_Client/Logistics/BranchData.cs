using System;
using BehaviourBranch.Logistics.PythonConnection;

namespace BehaviourBranch.Logistics
{
    [Serializable]
    public class BranchData : DataClass
    {
        public NodeData[] nodes;
        public int commandId = -1;
    }
}
