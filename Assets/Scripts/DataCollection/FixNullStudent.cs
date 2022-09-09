using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using DataCollection.Models;

public class FixNullStudent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (DcDataLogging.Student == null)
        {
            DcDataLogging.Student = new Student("NO NAME ENTERED", "");
        }

        if (DcDataLogging.Session == null)
        {
            DcDataLogging.BeginSession(DcDataLogging.Student);
        }
    }

    
}
