using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VASC_SubTaskType
{
    Waypoint = 0x00,
    ObjectPlace = 0x01,
    RegionEnter = 0x02,
    RegionExit = 0x03,
    PlayerLookAt = 0x04,
    Custom = 0x05
}

public abstract class VASC_SubTask : MonoBehaviour
{

    /**
    * Fields
    **/
    public string SubTaskName = "WaypointSubTask";

    public static void SubTaskComplete()
    {

    }

    /**
    * Must Implement
     **/
    public abstract void SubTaskBegin();
    public abstract void SubTaskRestart();
    public abstract void SubTaskFail();
    public abstract void SubTaskEnd();

}
