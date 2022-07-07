using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ProfileManager : MonoBehaviour
{
    // instantiation stuff for profiles
    public static ProfileManager Instance;
    public UserProfile[] userProfile;
    
    // these are to store user profile pics
    // if you add more here then add a conditional statement for it in the ProfileBrowser
    public Sprite monster0;
    public Sprite monster1;
    public Sprite monster2;
    public Sprite monster3;
    public Sprite monster4;
    
    // idk what this does but its probably really important
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}

[Serializable]
public class UserProfile
{
    // here is where any attributes of a user profile should be declared
    public string username;
    // the profile picture is just being declared as an integer and then equivalency statements are used to translate that into a picture
    public int character_num;
}
