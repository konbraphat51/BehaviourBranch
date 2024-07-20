using UnityEngine;
using UnityEngine.Events;

namespace BehaviourBranch.Logistics
{
    /// <summary>
    /// Connect with Python and receive new BehaviourBranch
    /// </summary>
    public abstract class BranchFetcher : MonoBehaviour
    {
        [SerializeField]
        protected string promptName = "";

        public BranchFetcher instance
        {
            get { return this; }
        }

        protected UnityEvent<BranchData> newBranchEvent = new UnityEvent<BranchData>();

        protected virtual void Awake()
        {
            PrepareConnection();
        }

        public abstract void SendCommand(string command, int commandId = -1);

        public virtual void RegisterAction(UnityAction<BranchData> action)
        {
            newBranchEvent.AddListener(action);
        }

        // AI connection
        private void PrepareConnection()
        {
            BranchFetcher fetcher = GetComponent<BranchFetcher>();
            fetcher.RegisterAction(ReceiveNewBranch);
            Debug.Log("Connection prepared");
        }
    }
}
