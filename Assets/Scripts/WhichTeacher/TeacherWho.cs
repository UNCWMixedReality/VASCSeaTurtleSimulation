using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.UI;
using TMPro;


public class TeacherWho : MonoBehaviour
{
    // Before you delete this script make sure you tell it

    //"PREPARE TO DIE"

    //if youve seen princess bride you've found the 6 fingered man.

    // thats all lol




    public GameObject profileBrowser;
    public GameObject teacherCanvas;
    public GameObject confirmCanvas;
    public GameObject buttonPrefab;
    public GameObject buttonParent;

    public TMP_InputField nameOfTeacher; 


    public int teacherNumber;

    
    private List<GameObject> teacherbuttons = new List<GameObject>();
    

    private List<string> teacherNames = new List<string>();
    

    string[] files = Directory.GetFiles(@"C:\Users\abw1717\Unity Projects\VASCSeaTurtleSimulation\Assets\Scripts\textFiles", "*.txt");
    int fileCount = Directory.GetFiles(@"C:\Users\abw1717\Unity Projects\VASCSeaTurtleSimulation\Assets\Scripts\textFiles", "*.txt").Length;

    // Start is called before the first frame update

    public void BeginTeacherFill()
    {
        for (int i = 0; i <= fileCount - 1; i++)
        {
            string name = files[i].ToString();
            string newName = name.Remove(0, 81);
            string correctName = newName.Remove(newName.LastIndexOf("."), 4);
            teacherNames.Add(correctName);


            GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
            newButton.GetComponent<ProfileButton>().profileText.text = correctName;
            teacherbuttons.Add(newButton);
            newButton.GetComponent<Button>().onClick.AddListener(() =>
                                    SelectTeacher(correctName));
        }

    }
    public void SelectTeacher(string teacher)
    {
        Debug.Log(teacher);
        int teacherNumber = teacherNames.IndexOf(teacher);
        teacherCanvas.SetActive(false);
        confirmCanvas.SetActive(true);
        nameOfTeacher.text = teacher;

        

    }

    public int getTeacherNumber()
    {
        return teacherNumber;
    }
}
