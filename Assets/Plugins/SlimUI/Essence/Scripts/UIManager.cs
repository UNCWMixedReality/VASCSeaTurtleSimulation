using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour {

	public enum Difficulties {
		easy, medium, hard, brutal
	};

	public enum LoadingPosition {
		center, topRight, topLeft, bottomLeft, bottomCenter, bottomRight
	};

	public enum LoadingStyle {
		style1, style2, style3, style4
	};
	
	Difficulties difficulty;

	[Header("Windows")]
	public GameObject mainMenu;
	public GameObject titleScreen;
	public GameObject welcomeScreen;

	[Header("Title & Welcome Screen")]
	public bool usingWelcomeScreen = false;
	public bool usingTitleScreen = false;
	[TextArea]
	public string disclaimer = "If you are using a spashscreen, make srue to DISABLE Main Menu by default. The game object will activate by script.";
	[Tooltip("The time to wait before you can leave the title screen.")]
	public float titleScreenWait;
	bool canPressKey = false;
	[Tooltip("Title animation.")]
	public Animator titleScreenTitle;
	[Tooltip("Key prompt animation.")]
	public Animator titleScreenPrompt;

	[Header("Loading Screen Elements")]
	[Tooltip("The parent game object holding loading icon animations.")]
	public RectTransform loadingIcon;
	float loadX, loadY;
	private string loadSceneName; // scene name is defined when the load game data is retrieved
	[Tooltip("The position on the screen of the loading icon.")]
	public LoadingPosition animationPosition;
	[Range(0.7f, 1.4f)]
	[Tooltip("The size of the loading icon in the load screen.")]
	public float loadIconScaleFactor = 1.0f;
	public LoadingStyle loadAnimation;
	public GameObject[] loadAnimationStyles;
	[Tooltip("The X position of the loading icon. Value is mirrored for both top and bottom of the screen.")]
	public int loadIconxPos = 853;
	[Tooltip("The Y position of the loading icon. Value is mirrored for both sides of the screen.")]
	public int loadIconyPos = 431;
	[Tooltip("The Text being used for the load message.")]
	public TMP_Text loadScreenDisplay;
	public string loadingScreenText = "LOADING";
	[Tooltip("The Text being used for the tips.")]
	public TMP_Text tipText;
	[Tooltip("The tips that will be cycled in the loading screen.")]
	public string[] tips;
	[Tooltip("How fast the tips alternate (in seconds).")]
	public int tipCycleSpeed = 4;
	int styleLength = 0;
	[Tooltip("Name of scene in build settings that loads on new game.")]
	public string newSceneName;
	[Tooltip("Name of scene in build settings that loads when loading previous save.")]
	public string savedSceneName;

	[Header("Data")]
	[Tooltip("The slider that represents current experience towards next rank.")]
	public Slider experienceBar;
	[Tooltip("The Text displaying the rank.")]
	public TMP_Text rankText;
	public static int rank = 1;

	[Header("Multiplayer")]
	public bool isReady = false;
	public bool friendsAreReady = false; // FOR DEMO PURPOSES, EVERYONE IS ALWAYS READY
	public TMP_Text readyTextButton;
	public TMP_Text readyTextDisplay;
	public Button findMatchButton;

	void Start(){
		if(animationPosition == LoadingPosition.center){
			loadX = 0;
			loadY = 0;
		}else if(animationPosition == LoadingPosition.topRight){
			loadX = loadIconxPos;
			loadY = loadIconyPos;
		}else if(animationPosition == LoadingPosition.topLeft){
			loadX = -loadIconxPos;
			loadY = loadIconyPos;
		}else if(animationPosition == LoadingPosition.bottomRight){
			loadX = loadIconxPos;
			loadY = -loadIconyPos;
		}else if(animationPosition == LoadingPosition.bottomCenter){
			loadX = 0;
			loadY = -loadIconyPos;
		}else if(animationPosition == LoadingPosition.bottomLeft){
			loadX = -loadIconxPos;
			loadY = -loadIconyPos;
		}

		if(usingTitleScreen){
			StartCoroutine(TitleScreenDelay());
		}else if (!usingTitleScreen){
			titleScreen.SetActive(false);
		}

		loadingIcon.anchoredPosition = new Vector2(loadX, loadY);
		loadingIcon.localScale = new Vector3(loadIconScaleFactor,loadIconScaleFactor,loadIconScaleFactor);

		loadScreenDisplay.text = loadingScreenText;
		styleLength = loadAnimationStyles.Length;

		LoadingStyle styleChosen = loadAnimation;
		int toInteger = (int)styleChosen;
		loadAnimationStyles[toInteger].SetActive(true);

		// Start leveling up automatically //////////////// FOR DEMONSTRATION ONLY
		rankText.text = "" + rank;
		InvokeRepeating("TestLevelingUp", 0f, 0.1f);
	}

	// Called when loading new game scene
	public void LoadNewLevel (){
		if(newSceneName != ""){
			StartCoroutine(LoadAsynchronously(newSceneName));
		}
	}

	public void AssignDifficulty(int dif){
		if(dif == 0){
			difficulty = Difficulties.easy;
		}else if(dif == 1){
			difficulty = Difficulties.medium;
		}else if(dif == 2){
			difficulty = Difficulties.hard;
		}else if(dif == 3){
			difficulty = Difficulties.brutal;
		}

		PlayerPrefs.SetString("difficulty",difficulty.ToString());
	}

	// Add the save code in this function! It doesn't work until you assign it to your scene.
	public void LoadSavedLevel (){
		if(savedSceneName != ""){
			StartCoroutine(LoadAsynchronously(savedSceneName)); // temporarily uses New Scene Name. Change this to 'loadSceneName' when you program the save data
		}
	}

	public void ReadyUpMultiplayer(){
		if(isReady){
			isReady = false;
			readyTextButton.text = "READY";
			readyTextDisplay.text = "NOT READY";
			readyTextDisplay.color = Color.red;
			if(friendsAreReady){
				findMatchButton.interactable = false;
			}
		}else if(!isReady){
			isReady = true;
			readyTextButton.text = "NOT READY";
			readyTextDisplay.text = "READY";
			readyTextDisplay.color = Color.green;
			if(friendsAreReady){
				findMatchButton.interactable = true;
			}
		}
	}

	IEnumerator LoadAsynchronously (string sceneName){ // scene name is just the name of the current scene being loaded
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
		operation.allowSceneActivation = false;

		while (!operation.isDone){

			//float progress = Mathf.Clamp01(operation.progress / .9f);
			if(operation.progress >= 0.9f){
				loadScreenDisplay.text = "PRESS ANY KEY TO CONTINUE";

				if(Input.anyKey){
					operation.allowSceneActivation = true;
				}
			}
			
			yield return null;
		}
	}

	void Update(){
		if(usingTitleScreen){
			if(Input.anyKeyDown && canPressKey){
				StartCoroutine(TitleScreenClose());
			}
		}
	}

	IEnumerator TitleScreenDelay(){
		yield return new WaitForSeconds(titleScreenWait);
		canPressKey = true;
	}

	IEnumerator TitleScreenClose(){
		usingTitleScreen = false;
		titleScreenTitle.SetTrigger("Fade");
		titleScreenPrompt.SetTrigger("Fade");
		yield return new WaitForSeconds(1f);
		mainMenu.SetActive(true);
		if(usingWelcomeScreen){
			welcomeScreen.SetActive(true);
		}
	}

	public void StartTipCycle(){
		InvokeRepeating("ChangeTip", tipCycleSpeed, tipCycleSpeed);
	}

	void ChangeTip(){
		tipText.text = tips[Random.Range(0,tips.Length)];
	}

	void TestLevelingUp()
    {
        experienceBar.value += 1;

		if(experienceBar.value == 100){
			experienceBar.value = 0;
			rank += 1;
			rankText.text = "" + rank;
		}
	}
}
