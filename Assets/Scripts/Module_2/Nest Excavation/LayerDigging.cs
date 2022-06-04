using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerDigging : MonoBehaviour
{
    public GameObject particle;
    public GameObject prevLayer;
    //public GameObject nextLayer;
    private ParticleSystem part;

    //plays the particle effect when digging
    void OnTriggerEnter(Collider col)
    {
        if (prevLayer == null)//if the previous has already been destroyed
        {
            if (col.name == "GloveL" || col.name == "GloveR" || col.tag == "Hands")
            {
                particle.transform.position = gameObject.transform.position;
                part.Play();
                //move next layer up
                //nextLayer.transform.position = gameObject.transform.position;
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        part = particle.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
