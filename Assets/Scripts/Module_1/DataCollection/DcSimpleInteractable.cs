using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

namespace DataCollection
{
    public class DcSimpleInteractable : XRSimpleInteractable
    {
        public string currentChoice = null;
        public string correctChoice = null;
        public string taskID = null;
        protected override void OnSelectExit(XRBaseInteractor baseInteractable)
        {
            if (correctChoice == null)
            {
                correctChoice = DcDataLogging.CorrectAnswers[taskID];
            }
            base.OnSelectExit(baseInteractable);
            DcDataLogging.LogDecision(new Models.Decision(
                SceneManager.GetActiveScene().name,
                DateTime.Now,
                currentChoice,
                correctChoice,
                taskID
                ));
        }
    }
}