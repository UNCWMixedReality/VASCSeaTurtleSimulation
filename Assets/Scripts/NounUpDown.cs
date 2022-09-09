using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class NounUpDown : MonoBehaviour
{
    // Here we create a list for nouns to be selected
    List<string> noun = new List<string>();

    // These are the input fields that we are populating for the create profile
    public TMP_InputField field1;
    public TMP_InputField field2;
    public TMP_InputField field3;

    // This is the field that shows the choice that they made after they select the noun
    public TMP_InputField selected;

    // we use this to keep the fields from skipping over in play mode.
    // aka a counter
    private int i = 1;


    void Start()
    {
        // List of different Nouns feel free to change whatever you think is best

        noun.Add("Lobster");
        noun.Add("Starfish");
        noun.Add("Oyster");
        noun.Add("Squid");
        noun.Add("Fish");
        noun.Add("Eel");
        noun.Add("Coral");
        noun.Add("Shrimp");
        noun.Add("Pelican");
        noun.Add("Otter");
        noun.Add("Seagull");
        noun.Add("Shark");
        noun.Add("Shell");
        noun.Add("Dolphan");
        noun.Add("Whale");

        // Randomize it, the kids just want to play and honestly
        // just choose the first one they see

        noun = noun.OrderBy(i => Random.value).ToList();

        // set the first 3 choices in the field
        field1.text = noun[i - 1];
        field2.text = noun[i];
        field3.text = noun[i + 1];
    }

    // This is our go down script which is an onclick button for the down triangle
    // on the bottom right side.

    public void goDown()
    {
        //Debug.Log(i);

        // Our Counter
        i++;

        // This is for the majority of the list where we dont want to see the end
        // of the list or the beginning of the list.
        if (i > 0 && i < 14)
        {
            field1.text = noun[i - 1];
            field2.text = noun[i];
            field3.text = noun[i + 1];

        }
        // 15 and 0 are pretty much equal compared to our counter i
        // we just want 0 to be in the center showing the last of the list
        // and the beginning of the list.
        if (i == 15 || i == 0)
        {
            field1.text = noun[14];
            field2.text = noun[0];
            field3.text = noun[1];
            i = 0;
        }
        // if our counter i is at 14, we want 14 to be in the center but also show
        // the beginning of the list at the bottom
        if (i == 14)
        {
            field1.text = noun[13];
            field2.text = noun[14];
            field3.text = noun[0];
        }
    }

    // This is an onclick button for the top right triangle of the username
    // selection screen
    public void goUp()
    {
        // Counter
        i--;

        //Debug.Log(i);

        // if our counter is -1 we need to make it 14 instead because we cant have
        // a negative number 
        if (i == -1)
        {
            i = 14;
        }

        // like the goDown method, this is the majority, just changing out
        // the fields based on our counter
        if (i > 0 && i < 14)
        {
            field1.text = noun[i - 1];
            field2.text = noun[i];
            field3.text = noun[i + 1];

        }
        // if our counter is at 14 we want to show the beginning of the list at
        // the bottom
        if (i == 14)
        {
            field1.text = noun[13];
            field2.text = noun[14];
            field3.text = noun[0];

        }

        // like before 15 and 0 are equivalient thats why we use or(||) just want
        // the beginning of the list in the middle and the end of the list at the top.
        if (i == 15 || i == 0)
        {
            field1.text = noun[14];
            field2.text = noun[0];
            field3.text = noun[1];

        }
    }

    // When the user makes the decison on what they want as an Noun,
    // finder is called on click and we return the i value and put it in
    // the text field on the next screen to confirm that is what they want.
    public void finder()
    {
        selected.text = noun[i];
    }
}
