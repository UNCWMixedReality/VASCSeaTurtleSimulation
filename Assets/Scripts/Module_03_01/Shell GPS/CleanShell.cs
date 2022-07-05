using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// handles progressing tasks when cloth touches shell
public class CleanShell : MonoBehaviour
{
	public TaskManagerM3_1 taskMan;

	//tracks if cloth has touched shell
	void OnTriggerEnter(Collider col)
	{
		Debug.Log("collision detected");
		if (col.name == "Cloth")
		{
			//deactivate self
			gameObject.SetActive(false);

			taskMan.MarkTaskCompletion(3);
		}
	}
}

