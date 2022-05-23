using System;
using System.Collections.Generic;

namespace FrostweepGames.Plugins.GoogleCloud
{
    public class Networking : IDisposable
    {
        public event Action<NetworkResponse> NetworkResponseEvent;

        private List<NetworkRequest> _networkRequests;
        private List<NetworkResponse> _networkResponses;

        private long _packetIndex = 0;


        public Networking()
        {
            _networkRequests = new List<NetworkRequest>();
            _networkResponses = new List<NetworkResponse>();
        }

        public void Update()
        {
            for(int i = 0; i < _networkRequests.Count; i++)
            {
                if (_networkRequests[i].request.isDone)
                {
                    NetworkResponse response = new NetworkResponse(_networkRequests[i].request.text,
                                                                   _networkRequests[i].request.error,
                                                                   _networkRequests[i].netPacketIndex, 
                                                                   _networkRequests[i].requestType,
                                                                   _networkRequests[i].parameters);

                    _networkResponses.Add(response);

                    if (NetworkResponseEvent != null)
                        NetworkResponseEvent(response);

                    _networkRequests.RemoveAt(i--);
                }
            }
        }

        public void Dispose()
        {
            _networkRequests.Clear();
            _networkResponses.Clear();
        }

        public long SendRequest(string uri, string data, NetworkEnumerators.RequestType requestType, object[] param = null, bool checkCertificates = false)
        { 
            long netIndex = _packetIndex++;

            NetworkRequest netRequest = new NetworkRequest(uri, data, netIndex, requestType, param, checkCertificates);

            _networkRequests.Add(netRequest);

            netRequest.Send();

            return netIndex;
        }
    }
}