using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//keeps track of the egg inspection task and subtasks
//NOT USED
public class EggManager : MonoBehaviour
{
	public EggInspection goodEgg1;
	public EggInspection goodEgg2;
	public EggInspection goodEgg3;
	public EggInspection goodEgg4;
	public EggInspection goodEgg5;
	public EggInspection goodEgg6;
	public EggInspection badEgg1;
	public EggInspection badEgg2;
	public EggInspection halfEgg1;
	public EggInspection halfEgg2;
		
	public int wrongBucket = 0;
	
	public bool taskDone = false;

	public AudioFeedback audiofeedback;
	
	private bool oneDone = false;
	private bool twoDone = false;
	private bool threeDone = false;
	private bool fourDone = false;
	private bool fiveDone = false;
	private bool sixDone = false;
	private bool sevenDone = false;
	private bool eightDone = false;
	private bool nineDone = false;
	private bool tenDone = false;

    //tracks each egg being sorted and gives audio feedback
    void Update()
    {
        if(goodEgg1.isSorted && oneDone == false){
			oneDone = true;
			audiofeedback.playGood();
			Debug.Log("Good Egg 1 Sorted");
		}
		if(goodEgg2.isSorted && twoDone == false){
			twoDone = true;
			audiofeedback.playGood();
			Debug.Log("Good Egg 2 Sorted");
		}
		if(badEgg1.isSorted && threeDone == false){
			threeDone = true;
			audiofeedback.playGood();
			Debug.Log("Bad Egg 1 Sorted");
		}
		if(badEgg2.isSorted && fourDone == false){
			fourDone = true;
			audiofeedback.playGood();
			Debug.Log("Bad Egg 2 Sorted");
		}
		if(goodEgg3.isSorted && fiveDone == false)
        {
			fiveDone = true;
			audiofeedback.playGood();
			Debug.Log("Good Egg 3 Sorted");
        }
		if(goodEgg4.isSorted && sixDone == false)
        {
			sixDone = true;
			audiofeedback.playGood();
			Debug.Log("Good Egg 4 Sorted");
        }
		if(goodEgg5.isSorted && sevenDone == false)
        {
			sevenDone = true;
			audiofeedback.playGood();
			Debug.Log("Good Egg 5 Sorted");
        }
		if(goodEgg6.isSorted && eightDone == false)
        {
			eightDone = true;
			audiofeedback.playGood();
			Debug.Log("Good Egg 6 Sorted");
        }
		if(halfEgg1.isSorted && nineDone == false)
        {
			nineDone = true;
			audiofeedback.playGood();
			Debug.Log("Half Egg 1 Sorted");
        }
		if(halfEgg2.isSorted && tenDone == false)
        {
			tenDone = true;
			audiofeedback.playGood();
			Debug.Log("Half Egg 2 Sorted");
        }
		if(oneDone && twoDone && threeDone && fourDone && fiveDone && sixDone && sevenDone && eightDone && nineDone && tenDone && !taskDone){
			taskDone = true;
			wrongBuckets();
		}
    }
	
	void wrongBuckets(){
		wrongBucket = goodEgg1.wrong + goodEgg2.wrong + badEgg1.wrong + badEgg2.wrong + goodEgg3.wrong + goodEgg4.wrong + goodEgg5.wrong + goodEgg6.wrong + halfEgg1.wrong + halfEgg2.wrong;
	}
}
