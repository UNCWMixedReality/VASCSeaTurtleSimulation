using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//handles measuring and sorting eggs
public class EggInspection : MonoBehaviour
{
    //determines whether an egg has been properly sorted or not
    public GameObject tryAgainText;
    public GameObject successText;
    
    public GameObject egg;
  
    public Vector3 startPosition;

	public bool isSorted = false;
    public bool isCounted = false;
	
	public int wrong = 0;

    public AudioFeedback audiofeedback;
    public EggCounter eggCounter;
 
    //Removes the success text and destroys the current egg
    public void successFalse(){
        successText.SetActive(false);
        gameObject.SetActive(false);
        isCounted = false;
    }

    //Removes the success text 
    public void tryAgainFalse()
    {
        tryAgainText.SetActive(false);
        audiofeedback.playBad();
		egg.GetComponent<Rigidbody>().useGravity = false;
        egg.transform.position = startPosition;
    }


    public void OnTriggerEnter(Collider collider)
    {
		egg.GetComponent<Rigidbody>().useGravity = true;
        
        //good egg in good bucket
        if (tag== "Egg" && collider.name == "Sand Layer Good")
        {
            if(tryAgainText.activeSelf == true){
                tryAgainText.SetActive(false);
            }
			isSorted = true;
            if (!isCounted)
            {
                eggCounter.increment(1);
                isCounted = true;
            }
            //successText.SetActive(true);
            Invoke("successFalse", 1);
          
        }

        //good egg in bad bucket
        if (tag == "Egg" && collider.name == "Sand Layer Bad")
        {
            if (successText.activeSelf == true)
            {
                successText.SetActive(false);
            }

            //tryAgainText.SetActive(true);
			wrong++;
            Invoke("tryAgainFalse", 1);
        }

        //bad egg in bad bucket
        if (tag == "BrokenEgg" && collider.name == "Sand Layer Bad")
        {
            if (tryAgainText.activeSelf == true)
            {
                tryAgainText.SetActive(false);
            }
			isSorted = true;
            if (!isCounted)
            {
                eggCounter.increment(0);
                isCounted = true;
            }
            //successText.SetActive(true);  
            Invoke("successFalse", 1);

        }

        //bad egg in good bucket
        if (tag == "BrokenEgg" && collider.name == "Sand Layer Good")
        {
            if (successText.activeSelf == true)
            {
                successText.SetActive(false);
            }
            //tryAgainText.SetActive(true);
            wrong++;
            Invoke("tryAgainFalse", 1);

        }
    }
}

