using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VASC_TaskType
{
    Objective = 0x00,
    Optional = 0x01
}

public abstract class VASC_Task : MonoBehaviour
{

    /**
     * Fields
     **/
    public string TaskName = "DefaultTaskName";

    /**
     * Properties
     **/
    public VASC_TaskType TaskType = VASC_TaskType.Objective;
    public abstract List<VASC_SubTask> SubTasks { get; }

    /**
     * Constructors
     **/
    public VASC_Task() { }
    public VASC_Task(string taskName) { TaskName = taskName; }

    /**
     * Virtuals
     **/
    public virtual string getTaskName() { return TaskName; }
    public virtual VASC_TaskType getTaskType() { return TaskType; }

    /**
     * Must Implement
     **/
    public abstract void TaskBegin();
    public abstract void TaskEnd();

}
