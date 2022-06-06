using UnityEngine;

using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleButton : Toggle {
	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) {
		base.OnPointerClick(eventData);
		
		// override the color such that the toggle state of the button is obvious
		// by its color. 
		if (isOn) {
			image.color = this.colors.pressedColor; 

		} else {
			image.color = this.colors.normalColor;           
		}
	}
}