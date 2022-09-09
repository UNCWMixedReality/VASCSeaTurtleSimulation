using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomOrder : MonoBehaviour
{

    public List<int> randomize(int size)
    {
        var aList = Enumerable.Range(1, size).ToList();

        var shuffledList = aList.OrderBy(x => Random.value).ToList();

        return shuffledList;
    }
}
