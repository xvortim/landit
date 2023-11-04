using UnityEngine;
using UnityEngine.UIElements;

public class hudUpdater : MonoBehaviour
{
    public Label labelElement;
	public Label helpElement;
	public Label compassElement;

    public void Start()
    {
		labelElement   = GetComponent<UIDocument>().rootVisualElement.Q<Label>("hud");
		helpElement    = GetComponent<UIDocument>().rootVisualElement.Q<Label>("help");
		compassElement = GetComponent<UIDocument>().rootVisualElement.Q<Label>("compass");
    }
	
	public void Update() {
		labelElement.text =  "Throttle: " + Airplane.throttle.ToString("F0") + "%\n" + "Airspeed: " + (Airplane.rb.velocity.magnitude*3).ToString("F0") 
										  + "kn\n" + "Altitude: " + (Airplane.altitude*4).ToString("F0") +  "ft\n";
		if(Input.GetKeyDown(KeyCode.H))
			helpElement.text =  "Throttle: SPACE\\LSHIFT\nPitch: S\\W \nRoll: A\\D \nYaw: Q\\E \nFlaps: F\\G \nCamera POV: num1 -> num4\nR to restart, M to menu\nPress X to remove help" ;
		else if(Input.GetKeyDown(KeyCode.X))
			helpElement.text =  "" ;

		compassElement.text = (Airplane.rb.transform.eulerAngles.y).ToString("F0") + '°';
	}
}

