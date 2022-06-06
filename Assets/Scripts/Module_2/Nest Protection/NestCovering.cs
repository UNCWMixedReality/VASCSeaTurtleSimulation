using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//handles covering nest with sand, attached to nest
public class NestCovering : MonoBehaviour
{
	public NestDigging dug;
	
	public GameObject placeholder;
	public GameObject Shovel;
	public GameObject sandLayer;
	public GameObject particle;
	public GameObject nextplaceholder;
	public GameObject nextCol;
	public NestCage cage;
		
	public bool isCovered;
	
	private ParticleSystem part;

	IEnumerator Pause()
	{
		yield return new WaitForSeconds(2);
		nextplaceholder.SetActive(true);
	}

	// Start is called before the first frame update
	void Start()
    {
		nextplaceholder.SetActive(false);
        part = particle.GetComponent<ParticleSystem>();
        isCovered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(cage.taskDone){
			Destroy(nextplaceholder);
		}
    }
	
	//handles moving sand over nest from shovel
	void OnTriggerEnter(Collider col)
	{
		if(col.name == "Shovel" && dug.isDug && !isCovered){
			part.Play();
			Shovel.gameObject.transform.GetChild(6).gameObject.SetActive(false);
			sandLayer.transform.position = placeholder.transform.position;
			isCovered = true;
			Debug.Log("Nest Covered");
			//move current collider away
			transform.position = new Vector3(0, 0f, 0);
			//move next collider into place
			StartCoroutine(Pause());
			nextCol.transform.position = nextplaceholder.transform.position;
		}
	}
}
