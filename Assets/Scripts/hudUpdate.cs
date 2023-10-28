using UnityEngine;
using UnityEngine.UIElements;

public class hudUpdater : MonoBehaviour
{
    public Label labelElement;

    public void Start()
    {
		labelElement = GetComponent<UIDocument>().rootVisualElement.Q<Label>("hud");
    }
	
	public void Update() {
		labelElement.text =  "Throttle: " + Airplane.throttle.ToString("F0") + "%\n" + "Airspeed: " + (Airplane.rb.velocity.magnitude*3).ToString("F0") + "kn\n" + "Altitude: " + (Airplane.altitude*4).ToString("F0") +  " ft";
	}
}
