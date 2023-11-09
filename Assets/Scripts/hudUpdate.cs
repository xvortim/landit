using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class hudUpdater : MonoBehaviour
{
	public static float sliderValue;
	public static bool flapsUI = false;
		
	//UI toolkit buttons
	public Label labelElement;
	public Label helpElement;
	public Label compassElement;
		
	public Button flapsElement;
	public Button restartElement;
	public Button menuElement;
	public SliderInt throttleElement;
	
	public void Start()
    {
		VisualElement root = GetComponent<UIDocument>().rootVisualElement;
		labelElement     = root.Q<Label>("hud");
		helpElement      = root.Q<Label>("help");
		compassElement   = root.Q<Label>("compass");
		flapsElement     = root.Q<Button>("flaps");
		throttleElement  = root.Q<SliderInt>("throttle");
	}
	
	public void Update() 
	{
		// HUD
		labelElement.text =  "Throttle: " + Airplane.throttle.ToString("F0") + "%\n" + "Airspeed: " + (Airplane.rb.velocity.magnitude*3).ToString("F0") 
										  + "kn\n" + "Altitude: " + (Airplane.altitude*4).ToString("F0") +  "ft\n";
		if(Input.GetKeyDown(KeyCode.H))
			helpElement.text =  "Throttle: SPACE\\LSHIFT\nPitch: S\\W \nRoll: A\\D \nYaw: Q\\E \nFlaps: F\\G \nCamera POV: num1 -> num4\nR to restart, M to menu\nPress X to remove help" ;
		else if(Input.GetKeyDown(KeyCode.X))
			helpElement.text =  "" ;

		compassElement.text = (Airplane.rb.transform.eulerAngles.y).ToString("F0") + '°';
		
		//Buttons
		if(!menuScript.mobileOn) {
			restartElement.style.display = DisplayStyle.None;
			menuElement.style.display = DisplayStyle.None;
			throttleElement.style.display = DisplayStyle.None;
			flapsElement.style.display    = DisplayStyle.None;
		} else {
			helpElement.text =  "";
		}

		flapsElement.clicked      += () => {Airplane.flapsCon = !Airplane.flapsCon; flapsUI = !flapsUI;};
		sliderValue = throttleElement.value;
	}
	
	public void OnEnable() 
	{
		VisualElement root = GetComponent<UIDocument>().rootVisualElement;
		restartElement   = root.Q<Button>("restart");
		menuElement      = root.Q<Button>("menu");
		
		menuElement.clicked     += () => SceneManager.LoadSceneAsync(0);
		restartElement.clicked  += () => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}