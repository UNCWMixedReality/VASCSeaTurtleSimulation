     //ForcedReset.cs
 
     using System;
     using UnityEngine;
     using UnityEngine.SceneManagement;
     using UnityStandardAssets.CrossPlatformInput;
     using UnityEngine.UI;
     //change GUITexture to Image
     [RequireComponent(typeof (Image))]
     
     public class ForcedReset : MonoBehaviour
     {
         private void Update()
         {
             // if we have forced a reset ...
             if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
             {
                 //... reload the scene
                 SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
             }
         }
     }