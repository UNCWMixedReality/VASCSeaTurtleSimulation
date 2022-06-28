using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles moving the wireframe cage over the covered nest, attached to a placeholder
public class NestCage : MonoBehaviour
{
	public TaskManagerM2_2 taskMan;		
	public NestCovering cover;
	
	//tracks if the cage has been set in place
	void OnTriggerEnter(Collider col)
	{
		if(col.name == "Cage")
		{
			//snap cage in place
			col.transform.position = transform.position;

			//deactivate self
			gameObject.SetActive(false);

			taskMan.MarkTaskCompletion();
		}
	}
}
