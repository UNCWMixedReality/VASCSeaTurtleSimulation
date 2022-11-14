using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Credit to Seth Angell https//sethangell.com
public class loadTeacherPortalData : MonoBehaviour
{
    void Awake()
    {
        //DO django tutorial
        // rest api endpoint
        // MAKE THIS IN SEPARATE STARTUP SCENE.
        // request object should be a global variable
        // loading scene for api request progress bar.
        // No loading scene, default demo scene no data logging
        /* http verbs (look it up, seths orders)
         * execution: game turns on, starts up, GET request to teacher portal on endpoint(API/"demoOrLogin" [do strings for flexibility in the future])
         * 
         * 
         */
        //Step 1 StartCoroutine() , while execution of startcouroutine isn't complete, sleep 1 sec, increment timeout integer. if timeout > 30  then load default scene
        StartCoroutine(requestComplete());

    }

    IEnumerator requestComplete()
    {
        timeout = 0;
        webReq = false //api endpoint request

        while(!webReq.Complete)
        {
            WaitForSeconds(1);
            timeout++;
            if timeout >= 30
                {
                
                //Load demo scene
            }
        }
    }

    void Start()
    {

    }

    void whichScene()
    {
        //if 
    }

    // Update is called once per frame
    void Update()
    {

    }
}


/* Notes
 *  Life Cycle- 
 * 
 * 
 * 
 * 
 * 
 * 
 */