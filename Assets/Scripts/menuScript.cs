using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class menuScript : MonoBehaviour
{
	public void OnEnable() {
		VisualElement root = GetComponent<UIDocument>().rootVisualElement;
		
		Button fly    = root.Q<Button>("fly");
		Button land   = root.Q<Button>("land");
		Button exit   = root.Q<Button>("exit");
		Button mobile = root.Q<Button>("mobile");
		
		fly.clicked    += () => PlayGameFly();
		land.clicked   += () => PlayGameLand();
		exit.clicked   += () => Application.Quit();
		mobile.clicked += () => MobileMode();
	}
	
	public void PlayGameFly() {
		SceneManager.LoadSceneAsync(1);
	}
	
	public void PlayGameLand() {
		SceneManager.LoadSceneAsync(2);
	}
	
	public void MobileMode() {
		hudUpdater.mobileOn = !hudUpdater.mobileOn;
		var style = GetComponent<UIDocument>().rootVisualElement.Q<Button>("mobile")?.style;
		
		if(hudUpdater.mobileOn)
			style.backgroundColor = new StyleColor(Color.green);
		else
			style.backgroundColor = new StyleColor(Color.red);
	}

}
