using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
	[Header("Plane Stats")]
	[Tooltip("Throttle position")]
	public float throttleIncrement = 0.1f;
	[Tooltip("Maximum thrust")]
	public float thrustMax= 200f;
	[Tooltip("Plane responsivness")]
	public float responsivness = 1f;
	
	private float throttle;
	private float roll;
	private float pitch;
	private float yaw;
	
	Rigidbody rb;
	
	private float responseModifier {
			get {
				return (rb.mass / 10f) * responsivness;
			}
	}
	
	private void Awake() {
		rb = GetComponent<Rigidbody>();
	}
	
	private void HandleInputs() {
		// Set rotation from axis inputs
		roll = Input.GetAxis("Roll");
		pitch = Input.GetAxis("Pitch");
		yaw = Input.GetAxis("Yaw");
		
		// Set throttle behaviour
		if(Input.GetKey(KeyCode.Space)) 
			throttle += throttleIncrement;
		else if(Input.GetKey(KeyCode.LeftControl))
			throttle -= throttleIncrement;
		
		throttle = Mathf.Clamp(throttle, 0f, 100f);
	}
	
	private void Update() {
		HandleInputs();
	}
	
	private void FixedUpdate() {
		// Apply forces to the plane
		rb.AddForce(transform.forward  * thrustMax * throttle);
		rb.AddTorque(transform.up      * yaw   * responseModifier);
		rb.AddTorque(transform.right   * pitch * responseModifier);
		rb.AddTorque(-transform.forward * roll  * responseModifier);
		
	}
	
	
	
}
