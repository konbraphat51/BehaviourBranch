using UnityEngine.Events;

namespace AI.BehaviourBranch.Logistics
{
    /// <summary>
    /// Connect with Python and receive new BehaviourBranch
    /// </summary>
    public abstract class BranchFetcher : SingletonMonoBehaviour<BranchFetcher>
    {
        protected UnityEvent<BranchData> newBranchEvent = new UnityEvent<BranchData>();

        public abstract void SendCommand(string command, int commandId = -1);

        public virtual void RegisterAction(UnityAction<BranchData> action)
        {
            newBranchEvent.AddListener(action);
        }
    }
}
