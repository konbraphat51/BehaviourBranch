using System;
using BehaviourBranch.Logistics.PythonConnection;
using UnityEngine;
using UnityEngine.Events;

namespace BehaviourBranch.Logistics
{
    /// <summary>
    /// Connect with local Python and receive new BehaviourBranch
    /// </summary>
    [RequireComponent(typeof(PythonConnector))]
    [RequireComponent(typeof(BranchDecoder))]
    public class BranchFetcherLocal : BranchFetcher
    {
        protected PythonConnector pythonConnector;

        protected override void Awake()
        {
            pythonConnector = GetComponent<PythonConnector>();

            pythonConnector.StartConnection();
        }

        private void OnDisable()
        {
            pythonConnector.StopConnection();
        }

        public override void SendCommand(string command, int commandId = -1)
        {
            Command instance = new Command()
            {
                command = command,
                commandId = commandId,
                promptName = promptName
            };

            pythonConnector.Send("command", instance);
        }

        public override void RegisterAction(UnityAction<BranchData> action)
        {
            base.RegisterAction(action);

            pythonConnector.RegisterAction(typeof(BranchData), ReceiveNewBranch);
        }

        private void ReceiveNewBranch(DataClass data)
        {
            BranchData branchData = data as BranchData;

            if (branchData != null)
            {
                newBranchEvent.Invoke(branchData);
            }
        }

        [Serializable]
        private class Command : DataClass
        {
            public string command;
            public int commandId = -1;
            public string promptName = "";
        }
    }
}
