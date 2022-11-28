using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles digging sand to put over nest, attached to sand pile
public class NestDigging : MonoBehaviour
{
	public GameObject shovelSand;
	public GameObject particle;

	//handles digging in sand with shovel and moving sand onto shovel
	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Shovel")
		{
			//play anim and remove the sand pile
			particle.GetComponent<ParticleSystem>().Play();
			Destroy(gameObject);

			//set the sand object attached to the shovel to active
			shovelSand.SetActive(true);
		}
	}
}
