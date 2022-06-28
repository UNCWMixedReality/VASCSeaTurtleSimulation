using System;
using UnityEngine;

namespace DataCollection
{
    public class DcExitHandler : MonoBehaviour
    {
        private void OnApplicationQuit()
        {
            DcDataLogging.EndSession();
        }

        public void EndSession()
        {
            DcDataLogging.EndSession();
        }
    }
}