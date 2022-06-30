using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//handles covering nest with sand, attached to nest
public class NestCovering : MonoBehaviour
{	
	public GameObject Shovel;
	public GameObject sandLayer;
	public GameObject particle;

	public TaskManagerM2_2 taskMan;
		
	private ParticleSystem part;

	// Start is called before the first frame update
	void Start()
    {
        part = particle.GetComponent<ParticleSystem>();
    }
	
	//handles moving sand over nest from shovel
	void OnTriggerEnter(Collider col)
	{
		if(col.name == "Shovel" && Shovel.gameObject.transform.GetChild(6).gameObject.activeSelf)
		{
			//play anim and put sand on top of nest (since collider is currently on top of nest, this is just the position of the collider)
			part.Play();
			sandLayer.transform.position = transform.position;

			//deactivate collider/shovel sand
			Shovel.gameObject.transform.GetChild(6).gameObject.SetActive(false);
			gameObject.SetActive(false);

			//task completed
			taskMan.MarkTaskCompletion(4);
		}
	}
}
