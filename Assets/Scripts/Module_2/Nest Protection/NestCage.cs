using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles moving the wireframe cage over the covered nest, attached to a placeholder
public class NestCage : MonoBehaviour
{
	public GameObject placeholder;
	public GameObject cage;
	public GameObject held;
		
	public NestCovering cover;
	
	public bool taskDone;
	
    // Start is called before the first frame update
    void Start()
    {
        taskDone = false;
    }
	
	void Update()
	{
		if(taskDone){
			Destroy(held);
		}
	}
	
	//tracks if the cage has been set in place
	void OnTriggerEnter(Collider col)
	{
		if(col.name == "Cage Hold" && !taskDone){
			cage.transform.position = placeholder.transform.position;
			cage.transform.rotation = placeholder.transform.rotation;
			placeholder.transform.position = new Vector3(0, 0f, 0);
			taskDone = true;
			Debug.Log("Cage Placed");
		}
	}
}
