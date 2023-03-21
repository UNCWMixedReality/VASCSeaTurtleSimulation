using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Characters : MonoBehaviour
{
    List<string> character = new List<string>();
    List<int> charArray = new List<int>();

    public TMP_InputField selectedcharacter;
    public TMP_InputField monsterNum;

    private int i = 0;

    

    // Start is called before the first frame update
    void Start()
    {

        charArray.Add(1);
        charArray.Add(2);
        charArray.Add(3);
        charArray.Add(4);
        charArray.Add(5);
        charArray.Add(6);
        charArray.Add(7);
        charArray.Add(8);



        character.Add("Blue Octopus");
        character.Add("Pink Octopus");
        character.Add("Turtle");
        character.Add("Orange Octopus");
        character.Add("Jelly");
        character.Add("Dolphan");
        character.Add("Stingray");
        character.Add("Seahorse");

        selectedcharacter.text = character[i];
    }

    public void goUp()
    {
        i++;
        if(i == 8)
        {
            i = 0;
            selectedcharacter.text = character[i];
        }
        else
        {
            selectedcharacter.text = character[i];
        }
    }
    public void goDown()
    {
        i--;
        if(i == -1)
        {
            i = 7;
            selectedcharacter.text = character[i];
        }
        else{
            selectedcharacter.text = character[i];
        }
        
    }

    public void finder()
    {
        selectedcharacter.text = character[i];

        monsterNum.text = charArray[i].ToString();
    }

    
    
}
