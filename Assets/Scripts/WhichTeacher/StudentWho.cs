using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class StudentWho : MonoBehaviour
{
    //GameObjects to be used 
    public GameObject StudentBrowser;
    public GameObject buttonPrefab;
    public GameObject buttonParent;

    // Input feild that gets the name of the Teacher
    public TMP_InputField nameOfTeacher;

    // getting the correct file
    public int thisOne;
    
    // list of student buttons
    private List<GameObject> studentbuttons = new List<GameObject>();

    //list of teacher names
    private List<string> teacherNames = new List<string>();

    // list of file names
    string[] files = Directory.GetFiles(@"C:\Users\abw1717\Unity Projects\VASCSeaTurtleSimulation\Assets\Scripts\textFiles", "*.txt");
    //Number of files
    int fileCount = Directory.GetFiles(@"C:\Users\abw1717\Unity Projects\VASCSeaTurtleSimulation\Assets\Scripts\textFiles", "*.txt").Length;

    //Fills the screen with user data
    public void BeginSudentFill()
    {
        
        for (int i = 0; i <= fileCount - 1; i++)
        {
            //leave just the name of the file without .txt
            string name = files[i].ToString();
            string newName = name.Remove(0, 81);
            string correctName = newName.Remove(newName.LastIndexOf("."), 4);
            //add teachernames to the list
            teacherNames.Add(correctName);
            if (correctName == nameOfTeacher.text)
            {
                //use this file to populate with students
                thisOne = i;
            }
        }

        //Debug.Log(fileCount);
        //Debug.Log(files[fileCount - 1]);
        //int num = something.getTeacherNumber();

        // open this file 
        StreamReader textfile = new StreamReader(files[thisOne]);
        string line;

        //read all the lines
        while ((line = textfile.ReadLine()) != null)
        {
            // create new button for each student
            
            GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
            newButton.GetComponent<ProfileButton>().profileText.text = line;
            studentbuttons.Add(newButton);
            //Debug.Log(line);
            newButton.GetComponent<Button>().onClick.AddListener(() =>
                                    SelectStudent(line)); // <-- fix this 
        }
    }
    public void SelectStudent(string studentname)
    {
        // gotta fix this 
        Debug.Log(studentname);
    }
}
