using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndBob : MonoBehaviour
{

    private int bobCycle = 1, bobCount = 1;

    void Update()
    {
        if (bobCount > 90) { bobCycle *= -1; bobCount = 1; }

        gameObject.transform.Rotate(0, 1, 0, Space.Self);
        gameObject.transform.Translate(0, bobCycle * -0.001f, 0);

        bobCount++;

    }
}
