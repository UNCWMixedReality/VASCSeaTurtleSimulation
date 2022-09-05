using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DataCollection
{
    public class DcUnityEvents : MonoBehaviour
    {
        // set to false if you don't want to start a session in this scene
        public bool BeginSession = true;
        private void OnApplicationQuit()
        {
            // ends the session if the app unexpectedly crashes or exits for whatever reason
            DcDataLogging.EndSession();
        }
        void Start()
        {
            // if data collection is wanted in this scene
            if (BeginSession)
            {
                // if the session hasn't already been started like it should have, start now
                if (DcDataLogging.Session == null)
                {
                    DcDataLogging.BeginSession();
                }
                // Sets the scene of the session to the current scene
                DcDataLogging.Session.SessionScene = SceneManager.GetActiveScene().name;
            }
            
        }
    }
}