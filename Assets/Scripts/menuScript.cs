using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class menuScript : MonoBehaviour
{
	public static bool mobileOn;
	public static bool soundOn;
	
	public void OnEnable() {
		VisualElement root = GetComponent<UIDocument>().rootVisualElement;
		Button fly    = root.Q<Button>("fly");
	    Button land   = root.Q<Button>("land");
		Button exit   = root.Q<Button>("exit");
		
		fly.clicked    += () => SceneManager.LoadSceneAsync(1);
		land.clicked   += () => SceneManager.LoadSceneAsync(2);
		exit.clicked   += () => Application.Quit();
	}
	
	public void Start() {
		hudUpdater.buttonRoll   = 0f;
		hudUpdater.buttonPitch  = 0f;
		hudUpdater.buttonYaw    = 0f;
 	}
	
	public void Update() {
		VisualElement root = GetComponent<UIDocument>().rootVisualElement;
		Toggle mobile = root.Q<Toggle>("mobile");
		Toggle sound  = root.Q<Toggle>("sound");
		
		mobileOn = mobile.value;
	    soundOn  = sound.value;	
	}
}
