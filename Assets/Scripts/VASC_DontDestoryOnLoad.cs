/*
 *  VASC
 *  VASC_DontDestroyOnLoad:
 *      attach this script to any gameobject that
 *      should remain persistant throughout the simulation
 *      
 *  Daniel vaughn
 *  5/6/2019
 */

using UnityEngine;

public class VASC_DontDestoryOnLoad : MonoBehaviour
{

    void Start()
    {
        DontDestroyOnLoad(this);
    }

}
