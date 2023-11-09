using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class menuScript : MonoBehaviour
{
	// Check local
	public static bool mobileOn;
	public static bool soundOn;
	
	// Check WebGL
	[DllImport("__Internal")]
	private static extern bool IsMobile();

	public bool isMobile()
	{
		#if !UNITY_EDITOR && UNITY_WEBGL
			return IsMobile();
		#endif
			return false;
	}
	
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
		VisualElement root = GetComponent<UIDocument>().rootVisualElement;
		Toggle mobile = root.Q<Toggle>("mobile");
		
		if(isMobile() || (Application.platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform)) {
			mobile.value = true;
		} else {
			mobile.value = false;
		}
	}
		
	public void Update() {
		VisualElement root = GetComponent<UIDocument>().rootVisualElement;
		Toggle mobile = root.Q<Toggle>("mobile");
		Toggle sound  = root.Q<Toggle>("sound");
		
		mobileOn = mobile.value;
	    soundOn  = sound.value;	
	}
}
