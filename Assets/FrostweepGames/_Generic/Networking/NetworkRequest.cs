#pragma warning disable 0618

using UnityEngine.Networking;
using System.Text;
using UnityEngine;
using System.Collections.Generic;

namespace FrostweepGames.Plugins.GoogleCloud
{
    public class NetworkRequest
    {
        public long netPacketIndex;
        public NetworkEnumerators.RequestType requestType;
        public object[] parameters;

        public NetworkMethod request;

        public NetworkRequest(string uri, string data, long index, NetworkEnumerators.RequestType type, object[] param = null, bool checkCeritifcates = false)
        {
            requestType = type;
            netPacketIndex = index;
            parameters = param;

            request = new NetworkMethod(uri, data, checkCeritifcates, type, NetworkConstants.NETWORK_METHOD);
        }

        public void Send()
        {
            request.Send();
        }
    }

    public class NetworkMethod
    {
        private string _uri,
                       _data;

        private bool _checkCertificate;

        private NetworkEnumerators.RequestType _requestType;

        private WWW _wwwRequest; 
        private UnityWebRequest _webRequest;

        private NetworkEnumerators.NetworkMethod _method;

        public bool isDone
        {
            get
            {
                switch(_method)
                {
                    case NetworkEnumerators.NetworkMethod.WWW:
                        return _wwwRequest.isDone;
                    case NetworkEnumerators.NetworkMethod.WEB_REQUEST:
                        return _webRequest.isDone;
                    default: break;
                }

                return false;
            }
        }

        public string text
        {
            get
            {
                switch (_method)
                {
                    case NetworkEnumerators.NetworkMethod.WWW:
                        return _wwwRequest.text;
                    case NetworkEnumerators.NetworkMethod.WEB_REQUEST:
                        return _webRequest.downloadHandler.text;
                    default: break;
                }

                return string.Empty;
            }
        }

        public string error
        {
            get
            {
                switch (_method)
                {
                    case NetworkEnumerators.NetworkMethod.WWW:
                        return _wwwRequest.error;
                    case NetworkEnumerators.NetworkMethod.WEB_REQUEST:
                        return _webRequest.error;
                    default: break;
                }

                return string.Empty;
            }
        }

        public NetworkMethod(string uri, string data, bool checkCeritifcates, NetworkEnumerators.RequestType type, NetworkEnumerators.NetworkMethod method)
        {
            _uri = uri;
            _data = data;
            _requestType = type;
            _checkCertificate = checkCeritifcates;
            _method = method;

            switch (method)
            {
                case NetworkEnumerators.NetworkMethod.WEB_REQUEST:
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(_data);

                        if (_requestType == NetworkEnumerators.RequestType.GET)
                            _webRequest = new UnityWebRequest(uri, UnityWebRequest.kHttpVerbGET);
                        else
                            _webRequest = new UnityWebRequest(uri, UnityWebRequest.kHttpVerbPOST);

                        if (!string.IsNullOrEmpty(data))
                            _webRequest.uploadHandler = new UploadHandlerRaw(bytes);

                        _webRequest.downloadHandler = new DownloadHandlerBuffer();
                        _webRequest.SetRequestHeader("Content-Type", "application/json");

                        if (checkCeritifcates)
                        {
#if UNITY_ANDROID
                            _webRequest.SetRequestHeader("X-Android-Package", NetworkConstants.PACKAGE_NAME);
                            _webRequest.SetRequestHeader("X-Android-Cert", NetworkConstants.KEY_SIGNATURE);
#elif UNITY_IOS
                            // need to check are they correct keys
                           // _webRequest.SetRequestHeader("X-IOS-Package", NetworkConstants.PACKAGE_NAME);
                          //  _webRequest.SetRequestHeader("X-IOS-Cert", NetworkConstants.KEY_SIGNATURE);
#endif
                        }
                    }
                    break;
                default: break;
            }
        }

        public void Send()
        {
            switch (_method)
            {
                case NetworkEnumerators.NetworkMethod.WEB_REQUEST:
                    _webRequest.SendWebRequest();
                    break;
                case NetworkEnumerators.NetworkMethod.WWW:
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(_data);

                        var headers = new Dictionary<string, string>();
                        headers.Add("Content-Type", "application/json");

                        if (_checkCertificate)
                        {
#if UNITY_ANDROID
                            headers.Add("X-Android-Package", NetworkConstants.PACKAGE_NAME);
                            headers.Add("X-Android-Cert", NetworkConstants.KEY_SIGNATURE);
#elif UNITY_IOS
                            //need to check are they correct keys
                            //headers.Add("X-IOS-Package", NetworkConstants.PACKAGE_NAME);
                            //headers.Add("X-IOS-Cert", NetworkConstants.KEY_SIGNATURE);
#endif
                        }

                        if (_requestType == NetworkEnumerators.RequestType.POST)
                            _wwwRequest = new WWW(_uri, bytes, headers);
                        else
                            _wwwRequest = new WWW(_uri);

                    }
                    break;
                default: break;
            }
        }
    }
}