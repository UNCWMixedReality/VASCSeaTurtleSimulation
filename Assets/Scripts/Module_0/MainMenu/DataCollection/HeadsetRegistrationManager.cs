using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class HeadsetRegistrationManager : MonoBehaviour
{

    public GameObject RegisteringHeadsetContainer;
    public GameObject RegistrationCompleteContainer;
    public GameObject HeadsetRefIDContainer;
    public TextMesh HeadsetRefText;

    bool registered = false;


    // Start is called before the first frame update
    async void Start()
    {
        await CheckHeadsetRegistrationStatus();
        await PopulateSettingsScreen();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async Task CheckHeadsetRegistrationStatus()
    {

        (bool, bool, string) results = await APIManager.CheckHeadsetRegistrationStatus(SystemInfo.deviceUniqueIdentifier);

        // Check if call failed in the network layer
        if (results.Item1 is false)
        {
            LogManager.LogMessage($"Headset Registration Check Failure: {results.Item3}", true);
        }
        else
        {
            registered = results.Item2;
        }

    }

    async Task RegisterHeadset()
    {
        (bool, bool, string) results = await APIManager.RegisterHeadset(SystemInfo.deviceUniqueIdentifier);

        // Check if call failed in the network layer
        if (results.Item1 is false)
        {
            LogManager.LogMessage($"Headset Registration Failure: {results.Item3}", true);
        }
        else
        {
            LogManager.LogMessage(results.Item3);
        }

    }



    async Task PopulateSettingsScreen()
    {

        if (registered)
        {
            RegistrationCompleteContainer.SetActive(true);
            HeadsetRefIDContainer.SetActive(true);
            HeadsetRefText.text = $"{HeadsetRefText.text}{SystemInfo.deviceUniqueIdentifier}";
        } else
        {
            RegisteringHeadsetContainer.SetActive(true);
            await RegisterHeadset();
            RegisteringHeadsetContainer.SetActive(false);
            RegistrationCompleteContainer.SetActive(true);
            HeadsetRefIDContainer.SetActive(true);
            HeadsetRefText.text = $"{HeadsetRefText.text}{SystemInfo.deviceUniqueIdentifier}";

        }
    }




}
