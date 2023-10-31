using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class menuScript : MonoBehaviour
{
	private void OnEnable() {
		VisualElement root = GetComponent<UIDocument>().rootVisualElement;
		
		Button fly  = root.Q<Button>("fly");
		Button land = root.Q<Button>("land");
		Button exit = root.Q<Button>("exit");
		
		fly.clicked += () => PlayGameFly();
		land.clicked+= () => PlayGameLand();
		exit.clicked+= () => Application.Quit();
	}
	
	public void PlayGameFly() {
		SceneManager.LoadSceneAsync(1);
	}
	
	public void PlayGameLand() {
		SceneManager.LoadSceneAsync(2);
	}

}
