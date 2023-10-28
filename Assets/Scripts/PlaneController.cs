using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
	[Header("Plane Stats")]
	[Tooltip("Throttle position")]
	public float throttleIncrement = 0.5f;
	[Tooltip("Maximum thrust")]
	public float thrustMax= 300f;
	[Tooltip("Plane responsivness")]
	public float responsivness = 1f;
	[Tooltip("Plane lift")]
	public float lift = 180f;
	
	public static float throttle;
	public static float altitude;
	private float roll;
	private float pitch;
	private float yaw;
	
	public static Rigidbody rb;
	public AudioSource engineSound;
	
	public float responseModifier {
			get {
				return (rb.mass / 10f) * responsivness;
			}
	}
	
	
	[SerializeField] Transform propeller;
	
	//---//
	
	public void HandleInputs() {
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
		altitude = rb.transform.position.y;
	}
		
	//---//
	
	public void Awake() {
		rb = GetComponent<Rigidbody>();
		engineSound = GetComponent<AudioSource>();
	}
	
	public void Start() {
		throttle = 100f;
		rb.velocity = new Vector3(0.0f, 0.0f, 20f);
	}
	
	public void Update() {
		HandleInputs();
		propeller.Rotate(Vector3.forward * throttle);
		engineSound.volume = throttle * 0.01f;
	}
	
	public void FixedUpdate() {
		// Apply forces to the plane
		rb.AddForce(transform.forward  * thrustMax * throttle);
		rb.AddTorque(transform.up      * yaw   * responseModifier);
		rb.AddTorque(transform.right   * pitch * responseModifier);
		rb.AddTorque(-transform.forward * roll  * responseModifier);
		
		rb.AddForce(Vector3.up * rb.velocity.magnitude * lift);
	}
}
