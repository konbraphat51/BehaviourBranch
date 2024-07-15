namespace BehaviourBranch.Logistics.HttpConnections
{
    /// <summary>
    /// holds access data to API
    /// </summary>
    public static class CognitoAccessData
    {
        public static string token { get; private set; }

        public static void SetToken(string token)
        {
            CognitoAccessData.token = token;
        }
    }
}
