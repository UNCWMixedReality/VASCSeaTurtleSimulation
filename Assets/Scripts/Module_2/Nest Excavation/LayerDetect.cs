using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerDetect : MonoBehaviour
{
    public GameObject particle;
    public GameObject prevLayer;
    public GameObject nextLayer;
    public TaskManager taskMan;
		
	public bool taskDone = false;
    	
    private ParticleSystem part;

    //detects if the player is digging and moves the sand gameobjects
    void OnTriggerEnter(Collider col)
    {
        if (prevLayer == null)//if the previous has already been destroyed
        {
            if (col.name == "GloveL" || col.name == "GloveR" || col.tag == "Hands")
            {
                particle.transform.position = gameObject.transform.position;
                print("collision");
                part.Play();
                //move next layer up
                nextLayer.transform.position = gameObject.transform.position;
				taskDone = true;
                taskMan.MarkTaskCompletion();
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        part = particle.GetComponent<ParticleSystem>();
    }
}
