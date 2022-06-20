using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class adjUpDown : MonoBehaviour
{
    List<string> adj = new List<string>();
    

    public TMP_InputField field1;
    public TMP_InputField field2;
    public TMP_InputField field3;
    private int i = 1;
    
    public TMP_InputField selected;

    // Start is called before the first frame update
    void Start()
    {
        adj.Add("Adventurous");
        adj.Add("TrustWorthy");
        adj.Add("Clever");
        adj.Add("Friendly");
        adj.Add("Awesome");
        adj.Add("Jolly");
        adj.Add("Eager");
        adj.Add("Lovely");
        adj.Add("Creative");
        adj.Add("Super");
        adj.Add("Slimy");
        adj.Add("Pretty");
        adj.Add("Loyal");
        adj.Add("Polite");
        adj.Add("Lucky");

        field1.text = adj[i - 1];
        field2.text = adj[i];
        field3.text = adj[i + 1];

    }
    public void Update()
    {
        
    }

    public void goDown()
    {
        Debug.Log(i);
        i++;
        if(i >0 && i<14)
        {
            field1.text = adj[i - 1];
            field2.text = adj[i];
            field3.text = adj[i + 1];
            
        }
        if (i == 15 || i == 0)
        {
            field1.text = adj[14];
            field2.text = adj[0];
            field3.text = adj[1];
            i=0;
        }
        if(i == 14)
        {
            field1.text = adj[13];
            field2.text = adj[14];
            field3.text = adj[0];
            
        }
    }

    public void goUp()
    {
        i--;
        Debug.Log(i);
        if (i == -1)
        {
            i = 14;
        }
        
        if (i > 0 && i < 14)
        {
            field1.text = adj[i - 1];
            field2.text = adj[i];
            field3.text = adj[i + 1];

        }

        if (i == 14)
        {
            field1.text = adj[13];
            field2.text = adj[14];
            field3.text = adj[0];
            
        }
        if (i == 15 || i == 0)
        {
            field1.text = adj[14];
            field2.text = adj[0];
            field3.text = adj[1];
            
        }


        

    }
    public void finder()
    {
        selected.text = adj[i];
    }
    

}
