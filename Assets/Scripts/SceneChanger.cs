using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.5f;

    IEnumerator LoadLevel(string name)
    {
        // Play Anim
        transition.SetTrigger("Start");
        // Wait

        yield return new WaitForSeconds(transitionTime);

        // Load Scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    public void changeScene(string name)
    {
        StartCoroutine(LoadLevel(name));
    }
}
