using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//**************************
//Created 8/4/2021
//by Matt Jarrett
//
//This file receives information from task_handler and the simulation
//and outputs that information to a new text file
//**************************
public class TexttoFile : MonoBehaviour
{
    public static string pin = "TestLog";
		
	//these will get start times for individual activities
	private InstructionManager IMOne;
	private InstructionManagerM2 IMTwo;
	
	//these will get completion times for individual activities
	private Activity_Manager AMOne;
	private Activity_Manager_M2 AMTwo;

    private bool said = false;
	private bool sceneChange = false;
	private bool go = true;
	private bool fileExists = true;
	//private string path = Directory.GetCurrentDirectory() + "/Output Data/";
	private string path;
    private string FileName = "";
	private float timeAtSceneChange = 0;
    private int testnumber = 1;
    private string[] DataList = new string[64];

    private bool setFileName()
    {
        FileName = pin + "-" + testnumber;
        if (FileName != "" && FileName != null)
        {
            return true;
        }
        return false;
    }
	
	private float getDifference(float topnum, float bottomnum){
		return topnum - bottomnum;
	}

    public void OnApplicationQuit()
    {
		if(go){
			//name the file
			setFileName();
			//determine if a file with that name already exists (meaning the user with this PIN has done the simulation before)
			//compare through all files currently in directory
			DirectoryInfo dir = new DirectoryInfo(path);
			FileInfo[] files = dir.GetFiles("*.*");
			while (fileExists)						//while it's possible a file already exists with an identical name
			{
				bool fileFound = false;				//a duplicate has not been found
				foreach(FileInfo f in files){		//check each file in the directory
					if(f.Name == FileName + ".txt")	//if a file with the PIN already exists
					{
						testnumber++;
						setFileName();				//rename this one with the new number
						fileFound = true;
					}
				}
				if(!fileFound){						//if a duplicate is not found
					fileExists = false;				//there is no possibility for a duplicate, break the loop
				}
			}
			//modify basic info
			DataList[0] = "Participant ID Number is:" + pin;
			DataList[1] = "This is test number: " + testnumber;
			DataList[2] = "Total runtime is: " + Time.time + " seconds";
			DataList[4] = "****************************************************";
			DataList[23] = "****************************************************";
			DataList[50] = "****************************************************";
			//create file and write information
			System.IO.File.WriteAllLines(path + FileName + ".txt", DataList);
			if(File.Exists(FileName + ".txt")){
				Debug.Log("Output file has been created: " + FileName + ".txt in folder " + path);
			}
		}
    }

    public void Awake()				//makes sure there is a directory to place the output file in, gets the output template to modify
    {
		path = Application.persistentDataPath + "/Output Data/";
		//path = "/ mnt / sdcard /";
		DontDestroyOnLoad(this);
		//check to see if the output directory exists, create it if it doesn't
		if(!Directory.Exists (path)){
			Directory.CreateDirectory (path);
		}
		//check for the template file and add it to list if it exists
		IMOne = GameObject.Find("[Manager]").GetComponent<InstructionManager>();
		AMOne = GameObject.Find("[Manager]").GetComponent<Activity_Manager>();
    }

    public void Update()
    {
        if(pin != "TestLog" && pin != null && !said)
        {
            Debug.Log(pin + " is Participant ID Number");
            said = true;
        }
		if(SceneManager.GetActiveScene().name == "Module_02" && !sceneChange){
			sceneChange = true;
			IMTwo = GameObject.Find("[Manager]").GetComponent<InstructionManagerM2>();
			AMTwo = GameObject.Find("[Manager]").GetComponent<Activity_Manager_M2>();
			Debug.Log("Module 2 managers successfully get");
		}
    }
	
	public void GetMOneInfo(){
		timeAtSceneChange = AMOne.totalTime;
		//******module 1******
		//a1
		DataList[8] = "Start: " + IMOne.oneStart + " seconds";
		DataList[9] = "Finish: " + AMOne.oneTime + " seconds";
		//a2
		DataList[12] = "Start: " + IMOne.twoStart + " seconds";
		DataList[13] = "Finish: " + AMOne.twoTime + " seconds";
		DataList[14] = "Incorrect guesses: " + AMOne.twoWrong;
		//a3
		DataList[17] = "Start: " + IMOne.threeStart + " seconds";
		DataList[18] = "Finish: " + AMOne.threeTime + " seconds";
		DataList[19] = "Incorrect guesses: " + AMOne.threeWrong;
		//completion
		DataList[21] = "Completion Timestamp: " + AMOne.totalTime + " seconds";
		//totals
		DataList[53] = "M1 Activity 1: " + (AMOne.oneTime - IMOne.oneStart) + " seconds";
		DataList[54] = "M1 Activity 2: " + (AMOne.twoTime - IMOne.twoStart) + " seconds";
		DataList[55] = "M1 Activity 3: " + (AMOne.threeTime - IMOne.threeStart) + " seconds";
		DataList[56] = "Module 1: " + AMOne.totalTime + " seconds";
		
		Debug.Log("M1 Info get");
	}
	
	public void GetMTwoInfo(){
		//******module 2******
		//a1
		DataList[27] = "Start: " + IMTwo.oneStart + " seconds";
		DataList[28] = "Finish: " + AMTwo.oneTime + " seconds";
		//a2
		DataList[31] = "Start: " + IMTwo.twoStart + " seconds";
		DataList[32] = "Finish: " + AMTwo.twoTime + " seconds";
		DataList[33] = "Eggs misplaced: " + AMTwo.wrongBucket;
		DataList[34] = "Eggs placed before measured: beans";
		//a3
		DataList[37] = "Start: " + IMTwo.threeStart + " seconds";
		DataList[38] = "Finish: " + AMTwo.threeTime + " seconds";
		//a4
		DataList[41] = "Start: " + IMTwo.fourStart + " seconds";
		DataList[42] = "Finish: " + AMTwo.fourTime + " seconds";
		//a5
		DataList[45] = "Start: " + IMTwo.fiveStart + " seconds";
		DataList[46] = "Finish: " + AMTwo.fiveTime + " seconds";
		
		DataList[48] = "Completion Timestamp: " + AMTwo.totalTime + " seconds";
		//******totals******
		DataList[58] = "M2 Activity 1: " + (AMTwo.oneTime - IMTwo.oneStart) + " seconds";
		DataList[59] = "M2 Activity 2: " + (AMTwo.twoTime - IMTwo.twoStart) + " seconds";
		DataList[60] = "M2 Activity 3: " + (AMTwo.threeTime - IMTwo.threeStart) + " seconds";
		DataList[61] = "M2 Activity 4: " + (AMTwo.fourTime - IMTwo.fourStart) + " seconds";
		DataList[62] = "M2 Activity 5: " + (AMTwo.fiveTime - IMTwo.fiveStart) + " seconds";
		DataList[63] = "Module 2: " + (AMTwo.totalTime - timeAtSceneChange) + " seconds";
		
		Debug.Log("M2 info get");
	}
}
