using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSaver : MonoBehaviour
{
    //prevents eggs from getting lost from moving too far out of bounds
    public GameObject eggplaceholder;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position, eggplaceholder.transform.position) > 7)
        {
            this.transform.position = eggplaceholder.transform.position;
        }
    }
}
