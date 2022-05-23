using UnityEngine;

//Script By: Cameron Detig 02/24/2020
//Attached to colliders on calipers to detect collisions with turtle colliders.
//NOT USED
public class CalipersCollisionEgg : MonoBehaviour
{
    [HideInInspector]
    public bool eggOne;
    [HideInInspector]
    public bool eggTwo;

    [HideInInspector]
    public bool eggThree;
    [HideInInspector]
    public bool eggFour;


    private void Start()
    {
        eggOne = false;
        eggTwo = false;
        eggThree = false;
        eggFour = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "EggColliderOneFront" || col.gameObject.name == "EggColliderOneBack")
        {
            eggOne = true;
        }
        else if (col.gameObject.name == "EggColliderTwoFront" || col.gameObject.name == "EggColliderTwoBack")
        {
            eggTwo = true;
        }

        if (col.gameObject.name == "EggColliderThreeFront" || col.gameObject.name == "EggColliderThreeBack")
        {
            eggThree = true;
        }
        if (col.gameObject.name == "EggColliderFourFront" || col.gameObject.name == "EggColliderFourBack")
        {
            eggFour = true;
        }
        //print("General Coll: " + col.gameObject.name);
    }

    void OnTriggerExit(Collider col)
    {
        eggOne = false;
        eggTwo = false;
        eggThree = false;
        eggFour = false;
    }

}
