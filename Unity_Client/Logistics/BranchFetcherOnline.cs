using System;
using System.Collections.Generic;
using BehaviourBranch.Logistics.HttpConnections;
using UnityEngine;

namespace BehaviourBranch.Logistics
{
    /// <summary>
    /// Connect with API Server and receive new BehaviourBranch
    /// </summary>
    public class BranchFetcherOnline : BranchFetcher
    {
        [SerializeField]
        private string apiUrl;

        private HttpConnectionModule connectionModule = new HttpConnectionModule();

        public override void SendCommand(string command, int commandId = -1)
        {
            Debug.Log("Fetcher Sending Command: " + command);

            Dictionary<string, string> header = new Dictionary<string, string>
            {
                { "authorization", CognitoAccessData.token },
                { "Content-Type", "application/json" }
            };

            Request request = new Request() { command = command, commandId = commandId };

            StartCoroutine(
                connectionModule.Post<Request, Response>(
                    apiUrl,
                    request,
                    header,
                    (Response response) =>
                    {
                        Debug.Log("Fetcher Received Response");
                        newBranchEvent.Invoke(response);
                    },
                    (errorText) =>
                    {
                        Debug.LogError("Failed to fetch new branch: " + errorText);
                    }
                )
            );
        }

        [Serializable]
        private class Request
        {
            public string command;
            public int commandId = -1;
            public string battleId = "none";
        }

        [Serializable]
        private class Response : BranchData { }
    }
}
