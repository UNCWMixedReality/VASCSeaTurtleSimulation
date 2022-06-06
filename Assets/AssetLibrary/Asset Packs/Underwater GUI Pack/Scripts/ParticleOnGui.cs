using UnityEngine;
using System.Collections;

public class ParticleOnGui : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		ParticleSystem ps = GetComponent<ParticleSystem> ();
		ps.GetComponent<Renderer>().sortingLayerName = "Particles";
	}
	
}