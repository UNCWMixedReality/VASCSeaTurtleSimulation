using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Altimit.UI;

public class SpeakableText : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    const string highlightTagStart = "<mark=#FF5C0050>";
    const string tagEnd = "</mark>";

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        cancelButtons = false;
        StartCoroutine(CoHoldClick());
    }

    TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        /*
        gameObject.Hold(
            AUI.UI.Image(AUI.GetSprite("Circle"), AUI.GetMaterial("Orange")).SetSize(AUI.SmallSize).Hold(
                AUI.UI.Image(AUI.GetSprite("Sound")).OnHeld(x=>x.Stretch(10))
            ).OnHeld(x=>x.SetAnchor(TextAnchor.MiddleLeft).SetPosition(Vector2.zero))
        );
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool cancelButtons = false;
    IEnumerator CoHoldClick()
    {
        yield return new WaitForSeconds(.25f);
        if (isPointerOver && isPointerDown)
        {
            cancelButtons = true;
            TextToSpeech.Instance.Speak(textMesh);
        }
    }

    bool isPointerOver = false;
    bool isPointerDown = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        //textMesh.text = highlightTagStart + TextToSpeech.StripHTML(textMesh.text) + tagEnd;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
        //textMesh.text = TextToSpeech.StripHTML(textMesh.text);
        //targetTextMesh.text.Replace(highlightTagStart, null).Replace(tagEnd, null);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        if (cancelButtons)
            return;
        var button = GetComponentInParent<Button>();
        if (button != null)
            button.SendMessage("OnPointerClick", eventData, SendMessageOptions.DontRequireReceiver);
    }
}
