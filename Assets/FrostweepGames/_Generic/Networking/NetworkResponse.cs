namespace FrostweepGames.Plugins.GoogleCloud
{
    public class NetworkResponse
    {
        public long netPacketIndex;
        public NetworkEnumerators.RequestType requestType;
        public object[] parameters;

        public string response;
        public string error;

        public NetworkResponse(string resp, string err, long index, NetworkEnumerators.RequestType type, object[] param)
        {
            requestType = type;
            netPacketIndex = index;
            response = resp;
            error = err;
            parameters = param;
        }
    }
}