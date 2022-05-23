using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollider : MonoBehaviour
{
    public Collider collider1;
    public Collider collider2;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(collider1.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
