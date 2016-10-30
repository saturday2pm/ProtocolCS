using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtocolCS//.Utility
{
    public class UriBuilder
    {
        public static string Create(string host, UserType userType, string playerId, string accessToken)
        {
            if (host.StartsWith("ws://") == false &&
                host.StartsWith("wss://") == false)
                throw new ArgumentException("`host` must start with [ws://, wss://]");

            return host + 
                string.Format("?userType={0}&playerId={1}&accessToken={2}&version={3}",
                userType.ToString().ToLower(),
                playerId,
                accessToken,
                Constants.ProtocolVersion.version);
        }
    }
}
