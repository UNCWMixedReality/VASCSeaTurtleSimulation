using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SliderCountdown : MonoBehaviour
{

    public GameObject self;
    public GameObject previous;
    public GameObject next;
    public float timer = 4f;
    public float countdown;
    

    // Start is called before the first frame update
    void Start()
    {
        countdown = timer;
    }

    // Update is called once per frame
    void Update()
    {
        
        countdown -= 1 * Time.deltaTime;
        if (countdown <= 0f)
        {
                         
                self.SetActive(false);
                next.SetActive(false);
                countdown = timer;           
            
        }
       

       
       

        

       
    }
    private void Awake()
    {
       
    }


}
