using System.Collections;
using UnityEngine;


public class CoData
{
    public Coroutine coroutine { get; private set; }
    public object result;
    private IEnumerator target;

    public CoData(MonoBehaviour owner, IEnumerator target)
    {
        this.target = target;
        this.coroutine = owner.StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (target.MoveNext())
        {
            result = target.Current;
            yield return result;
        }
    }
}

public class CoData<T>
{
    public Coroutine coroutine { get; private set; }
    public T result;
    private IEnumerator target;

    public CoData(MonoBehaviour owner, IEnumerator target)
    {
        this.target = target;
        this.coroutine = owner.StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (target.MoveNext())
        {
            result = (T)target.Current;
            yield return result;
        }
    }
}