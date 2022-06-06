using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script By: Cameron Detig 10/02/2020
//Was used for older version of module 1 for the changing of the tables with the different activities.

public class TableManager : MonoBehaviour
{
    //not currently used
    public GameObject[] tables;
    public float speed = .1f;
    public Vector3 activePosition = new Vector3(1, 0, 0);
    public Vector3 unactivePosition = new Vector3(1, -2f, 0);

    public GameObject floorHole;
    //public Table_Measuring_Manager tableMeasuringManager;
    public Table_Turtle_Manager tableTurtleManager;

    private int currentTableNumber = -1;


    void Start()
    {
        for (int i=0; i < tables.Length; i++)
        {
            //Set the position of the tables to be below the floor.
            tables[i].transform.position = unactivePosition;
            tables[i].SetActive(true);
        }

        floorHole.transform.localPosition = new Vector3(3, -.1f, -1.1f);

        nextTable(); //Start the simulation
        StartCoroutine(MoveHole(new Vector3(0, 0, -1.1f), 1f));
    }

    public void nextTable()
    {
        if (currentTableNumber < tables.Length - 1) //Don't run if at the end of the list
        {
            if (currentTableNumber == -1) //If this is the first table
            {
                StartCoroutine(MoveTo(tables[0], activePosition, 2f));
            }
            else //If this is the second or third table
            {
                StartCoroutine(SwitchTables());
            }

            currentTableNumber += 1;
        }
    }

    IEnumerator SwitchTables()
    {
        StartCoroutine(MoveHole(new Vector3(3, -.1f, -1.1f), 1f));
        yield return new WaitForSeconds(1);
        //print("Run " + currentTableNumber);
        StartCoroutine(MoveTo(tables[currentTableNumber - 1], unactivePosition, 2f));
        yield return new WaitForSeconds(2);
        StartCoroutine(MoveTo(tables[currentTableNumber], activePosition, 2f));
        yield return new WaitForSeconds(2);
        //print("Current Table: " + currentTableNumber);
        if (currentTableNumber == 1)
            tableTurtleManager.nextTurtle(); //Starts the turtle identification activity.
        StartCoroutine(MoveHole(new Vector3(0, 0, -1.1f), 1f));
    }


    //Used to move the tables up or down
    public IEnumerator MoveTo(GameObject table, Vector3 endPos, float duration)
    {
        float timer = 0f;
        Vector3 startPos = table.transform.position;
        Vector3 distance = endPos - startPos;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            table.transform.position = startPos + distance * timer / duration; //Move the table
            yield return null;
        }
    }

    //Used to move the hole in the floor
    public IEnumerator MoveHole(Vector3 endPos, float duration)
    {
        float timer = 0f;
        Vector3 startPos = floorHole.transform.localPosition;
        Vector3 distance = endPos - startPos;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            floorHole.transform.localPosition = startPos + distance * timer / duration; //Move the table
            yield return null;
        }
    }
}
