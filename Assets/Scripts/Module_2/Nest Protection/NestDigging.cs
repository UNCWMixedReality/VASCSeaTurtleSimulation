using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles digging sand to put over nest, attached to sand pile
public class NestDigging : MonoBehaviour
{
	public GameObject sandLayer;
	public GameObject Shovel;
	public GameObject particle;
	public GameObject placeholder;
	public GameObject nextCol;
	public bool isDug;
	
	private ParticleSystem part;
	
    // Start is called before the first frame update
    void Start()
    {
        part = particle.GetComponent<ParticleSystem>();
		isDug = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	//handles digging in sand with shovel and moving sand onto shovel
	void OnTriggerEnter(Collider col)
	{
		if(col.name == "Shovel"){
			Debug.Log("Sand Digged");
			part.Play();
			Shovel.gameObject.transform.GetChild(6).gameObject.SetActive(true);
			sandLayer.transform.position = new Vector3(0, 0f, 0);
			isDug = true;
			//move next collider into place
			nextCol.transform.position = placeholder.transform.position;
		}
	}
}
