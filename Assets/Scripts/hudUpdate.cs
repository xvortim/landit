using UnityEngine;
using UnityEngine.UIElements;

public class hudUpdater : MonoBehaviour
{
    public Label labelElement;
	public Label helpElement;

    public void Start()
    {
		labelElement = GetComponent<UIDocument>().rootVisualElement.Q<Label>("hud");
		helpElement = GetComponent<UIDocument>().rootVisualElement.Q<Label>("help");
    }
	
	public void Update() {
		labelElement.text =  "Throttle: " + Airplane.throttle.ToString("F0") + "%\n" + "Airspeed: " + (Airplane.rb.velocity.magnitude*3).ToString("F0") 
										  + "kn\n" + "Altitude: " + (Airplane.altitude*4).ToString("F0") +  "ft\n" + Airplane.rb.transform.localRotation.x;
		if(Input.GetKeyDown(KeyCode.H))
			helpElement.text =  "Throttle: SPACE\\LCTRL \nPitch: S\\W \nRoll: A\\D \nYaw: Q\\E \nFlaps: F\\G \nCamera POV: num1 -> num4\nPress X to remove help" ;
		else if(Input.GetKeyDown(KeyCode.X))
			helpElement.text =  "" ;		
	}
}
