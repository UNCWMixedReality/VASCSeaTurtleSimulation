using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;

public class fixNullStudent : MonoBehaviour
{
    public void SetStudentName()
    {
        if (DcDataLogging.Student == null)
        {
            DcDataLogging.Student = new DataCollection.Models.Student("NO NAME ENTERED", "");
        }
    }
}
