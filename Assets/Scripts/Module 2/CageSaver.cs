using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageSaver : MonoBehaviour
{
    //keeps the cage from getting lost by falling out of bounds too far
    public GameObject cageplaceholder;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, cageplaceholder.transform.position) > 20)
        {
            this.transform.position = cageplaceholder.transform.position;
            this.transform.rotation = cageplaceholder.transform.rotation;
        }
    }
}
