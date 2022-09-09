using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class TestOutput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/Testing.txt";
		if(!File.Exists(path)){
			File.WriteAllText(path, "Speen");
			Debug.Log("Speen");
		}
		else{
			File.AppendAllText(path, "Speen 2");
			Debug.Log("Speen 2");
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
