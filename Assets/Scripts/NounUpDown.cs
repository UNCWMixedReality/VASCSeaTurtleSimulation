using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NounUpDown : MonoBehaviour
{

    List<string> noun = new List<string>();


    public TMP_InputField field1;
    public TMP_InputField field2;
    public TMP_InputField field3;
    private int i = 1;

    public TMP_InputField selected;


    void Start()
    {
        noun.Add("Lobster");
        noun.Add("Starfish");
        noun.Add("Oyster");
        noun.Add("Squid");
        noun.Add("Barracuda");
        noun.Add("Eel");
        noun.Add("Coral");
        noun.Add("Barnacle");
        noun.Add("Pelican");
        noun.Add("Otter");
        noun.Add("Seagull");
        noun.Add("Sea Urchin");
        noun.Add("Anemone");
        noun.Add("Piranha");
        noun.Add("Orca");


        field1.text = noun[i - 1];
        field2.text = noun[i];
        field3.text = noun[i + 1];
    }

    public void goDown()
    {
        Debug.Log(i);
        i++;
        if (i > 0 && i < 14)
        {
            field1.text = noun[i - 1];
            field2.text = noun[i];
            field3.text = noun[i + 1];

        }
        if (i == 15 || i == 0)
        {
            field1.text = noun[14];
            field2.text = noun[0];
            field3.text = noun[1];
            i = 0;
        }
        if (i == 14)
        {
            field1.text = noun[13];
            field2.text = noun[14];
            field3.text = noun[0];

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
            field1.text = noun[i - 1];
            field2.text = noun[i];
            field3.text = noun[i + 1];

        }

        if (i == 14)
        {
            field1.text = noun[13];
            field2.text = noun[14];
            field3.text = noun[0];

        }
        if (i == 15 || i == 0)
        {
            field1.text = noun[14];
            field2.text = noun[0];
            field3.text = noun[1];

        }

    }
    public void finder()
    {
        selected.text = noun[i];
    }
}
