using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cube;
    public float spacing = .001f;

    // Start is called before the first frame update
    void Start()
    {
        /*
        for (int i = 1; i <= 10; i++)
        {
            GameObject a = Instantiate(cube, new Vector3(transform.position.x - (spacing * i), transform.position.y, transform.position.z), Quaternion.identity);

            for (int j = 1; j <= 10; j++)
            {
                GameObject b = Instantiate(cube, new Vector3(transform.position.x - (spacing * j), transform.position.y, transform.position.z - (spacing * j)), Quaternion.identity);

                for (int k = 1; k <= 10; k++)
                {
                    GameObject c = Instantiate(cube, new Vector3(transform.position.x - (spacing * k), transform.position.y + (spacing * k), transform.position.z), Quaternion.identity);
                }
            }
        }
        */

        for (int x = 1; x <= 10; x++)
        {
            for (int y = 1; y <= 10; y++)
            {
                for (int z = 1; z <= 10; z++)
                {
                    GameObject t = Instantiate(cube, new Vector3(transform.position.x - (spacing * x), transform.position.y + (spacing * y), transform.position.z - (spacing * z)), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
