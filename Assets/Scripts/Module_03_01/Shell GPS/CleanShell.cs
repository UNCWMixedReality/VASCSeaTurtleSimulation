using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// handles progressing tasks when cloth touches shell
public class CleanShell : MonoBehaviour
{
	public TaskManagerM3_1 taskMan;
	public Material[] dirtPhases;
	public GameObject turtle;
	public ParticleSystem dirtParticles;
	public ParticleSystem cleanParticles;
	public int cleaned = 0;

	//tracks if cloth has touched shell
	void OnTriggerEnter(Collider col)
	{
		Debug.Log("collision detected");
		if (col.name == "Cloth")
		{
			// update turtle shell material everytime a collision happens
			// After 3 collisions the task will be complete
			switch (cleaned)
            {
				case 0:
					turtle.GetComponent<MeshRenderer>().material = dirtPhases[1];
					dirtParticles.Play();
					break;
				case 1:
					turtle.GetComponent<MeshRenderer>().material = dirtPhases[2];
					dirtParticles.Play();
					break;
				case 2:
					turtle.GetComponent<MeshRenderer>().material = dirtPhases[0];
					cleanParticles.Play();
					taskMan.MarkTaskCompletion(3);
					break;
			}

			cleaned += 1;

			// play shine particle for a few collision after done
			if ((3 <= cleaned) && (cleaned < 7)) {
				cleanParticles.Play();
			} 
		}
	}
}

