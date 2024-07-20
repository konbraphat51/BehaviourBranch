using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace BehaviourBranch.Logistics.HttpConnections
{
    public class HttpConnectionModule
    {
        /// <summary>
        /// Conduct a POST request
        /// </summary>
        /// <param name="callback">response text will be returned</param>
        public IEnumerator Post(
            string url,
            string requestText,
            Dictionary<string, string> headers,
            UnityAction<string> callback,
            UnityAction<string> onFailed = null
        )
        {
            using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestText);

                request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);

                request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }

                Debug.Log("Sending: " + requestText);

                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    //failed
                    Debug.LogError(request.error);

                    if (onFailed != null)
                    {
                        onFailed.Invoke(request.error);
                    }
                }
                else
                {
                    //success
                    Debug.Log("Received: " + request.downloadHandler.text);
                    callback.Invoke(request.downloadHandler.text);
                }
            }
        }

        /// <summary>
        /// Conduct a POST request
        /// </summary>
        /// <param name="request">RequestType request</param>
        /// <param name="callback">ResponseType response will be returned</param>
        /// <typeparam name="RequestType">need [Serialize]</typeparam>
        /// <typeparam name="ResponseType">need [Serialize]</typeparam>
        public IEnumerator Post<RequestType, ResponseType>(
            string url,
            RequestType request,
            Dictionary<string, string> headers,
            UnityAction<ResponseType> callback,
            UnityAction<string> onFailed = null
        )
        {
            string json = JsonUtility.ToJson(request);

            yield return Post(
                url,
                json,
                headers,
                (string response) =>
                {
                    ResponseType responseObj = JsonUtility.FromJson<ResponseType>(response);
                    callback.Invoke(responseObj);
                },
                onFailed
            );
        }
    }
}
