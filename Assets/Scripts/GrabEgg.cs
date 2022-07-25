using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabEgg : MonoBehaviour
{
    public Animator anim;


    public void Start()
    {
        anim.SetBool("GrabEgg", false);
    }

    public void OpenHand()
    {
        anim.SetBool("GrabEgg", false);

    }
    public void CloseHand()
    {
        anim.SetBool("GrabEgg", true);
    }

}

