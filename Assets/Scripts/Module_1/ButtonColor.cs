using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour
{
    public Button button;
	//public Image incorrect;
    //public Image correct;
	
	void Start()
	{
		//ColorBlock colors = button.colors;
		//colors.normalColor = Color.white;
		//colors.highlightedColor = new Color32(108, 218, 253, 255);
		//colors.pressedColor = new Color32(76, 159, 185, 255);
		//button.colors = colors;
		//Debug.Log("Colors assigned");
	}
	
	public void Highlight(){
		//Debug.Log("should be highlighted");
		ColorBlock colors = button.colors;
		colors.normalColor = new Color32(108, 218, 253, 255);
		button.colors = colors;

	}
	
	public void Selected(){
		//Debug.Log("should be selected");
		ColorBlock colors = button.colors;
		colors.normalColor = new Color32(76, 159, 185, 255);
		button.colors = colors;
	}
	
	public void Reset(){
			//Debug.Log("color reset");
			ColorBlock colors = button.colors;
			colors.normalColor = Color.white;
			button.colors = colors;
	}
	
	/*public void Correct()
	{
		correct.color = new Color(1, 1, 1, 1);
        incorrect.color = new Color(1, 1, 1, 0);

	}
	
	public void Incorrect()
	{
		correct.color = new Color(1, 1, 1, 0);
        incorrect.color = new Color(1, 1, 1, 1);

	}*/
	
}
