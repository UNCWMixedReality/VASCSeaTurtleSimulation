using UnityEngine;
using System.Collections;

public class ButtonMonster : MonoBehaviour {
public static int Monster;
private Animator animator;

	void Awake ()
	{
		Monster = 1;
		animator = GetComponent<Animator>();
	
	}

	public void ClickMonsterRight() {

		Monster++;
		Debug.Log (Monster);

		if (Monster > 5) {
			Monster = 1; 
		}
	}

	public void ClickMonsterLeft() {

		Monster--;
		Debug.Log (Monster);

		if (Monster < 1) {
			Monster = 5; 
		}
	}

	public void AnimMonster() {
		if (Monster == 1) {
			
			animator.SetTrigger("monster_1");
			animator.Play("monster_1");
		}	

		if (Monster == 2) {
			
			animator.SetTrigger("monster_2");
			animator.Play("monster_2");
		}	

		if (Monster == 3) {

			animator.SetTrigger("monster_3");
			animator.Play("monster_3");
		}

		if (Monster == 4) {
			
			animator.SetTrigger("monster_4");
			animator.Play("monster_4");
		}

		if (Monster == 5) {
			
			animator.SetTrigger("monster_5");
			animator.Play("monster_5");
		}

		if (Monster == 6) {
			
			animator.SetTrigger("monster_6");
			animator.Play("monster_6");
		}

		if (Monster == 7) {
			
			animator.SetTrigger("monster_7");
			animator.Play("monster_7");
		}

		if (Monster == 8) {
			
			animator.SetTrigger("monster_8");
			animator.Play("monster_8");
		}

		if (Monster == 9) {
			
			animator.SetTrigger("monster_9");
			animator.Play("monster_9");
		}

		if (Monster == 10) {
			
			animator.SetTrigger("monster_10");
			animator.Play("monster_10");
		}
	}
}