using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class RuntimeListenerAddition : MonoBehaviour
{
    MenuManager InSceneManager;
    XRSimpleInteractable ListenerManager;
    LoginStudent StudentManager;

    protected void OnEnable()
    {
        InSceneManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<MenuManager>();

        StudentManager = GetComponentInChildren<Text>().GetComponent<LoginStudent>();

        ListenerManager = GetComponent<XRSimpleInteractable>();

        ListenerManager.onSelectExited.AddListener(OnSelectExit);

    }

    protected void OnDisable()
    {
        ListenerManager.onSelectExited.RemoveListener(OnSelectExit);

    }


    protected virtual void OnSelectExit(XRBaseInteractor interactor)

    {
        StudentManager.Login();
        InSceneManager.ChangePanel(GetComponentInParent<SceneSelectionContainer>().SceneSelection);
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
