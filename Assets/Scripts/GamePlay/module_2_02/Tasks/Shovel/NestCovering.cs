using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//handles covering nest with sand, attached to nest
public class NestCovering : MonoBehaviour
{	
	public GameObject Shovel;
	public GameObject shovelSand;
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
		if(col.tag == "Shovel")
		{
			//play anim and toggle sand active on top of nest (since collider is currently on top of nest, this is just the position of the collider)
			sandLayer.SetActive(true);
			part.Play();

			//deactivate collider/shovel sand
			shovelSand.SetActive(false);
			gameObject.SetActive(false);

			//task completed
			taskMan.MarkTaskCompletion(4);
		}
	}
}
