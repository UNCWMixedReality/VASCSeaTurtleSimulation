using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VASC_WaypointsTask : VASC_Task
{

    public new string TaskName = "SampleTask";
    public new VASC_TaskType TaskType = VASC_TaskType.Optional;

    private List<VASC_SubTask> subTasks = new List<VASC_SubTask>();
    public override List<VASC_SubTask> SubTasks { get { return subTasks; } }

    public VASC_WaypointsTask()
    {
        //subTasks.Add();
    }

    public override string getTaskName() { return TaskName; }
    public override void TaskBegin() { throw new System.NotImplementedException(); }
    public override void TaskEnd() { throw new System.NotImplementedException(); }
}
