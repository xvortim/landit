using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	[Tooltip("Starting speed")]
	public float start = 30f;
	
	public bool flapsCon = false;
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
	[SerializeField] Transform lailleron;
	[SerializeField] Transform railleron;
	[SerializeField] Transform flaps;
	[SerializeField] Transform elevator;
	[SerializeField] Transform rudder;
	
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
		
		if(Input.GetKey(KeyCode.M))
			SceneManager.LoadSceneAsync(0);
		
		if(Input.GetKey(KeyCode.R))
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			
	}
	
	public void HandleCommands() {
		// Animations
		propeller.Rotate(Vector3.forward * throttle);
		// LeftRoll
		if(Input.GetKey(KeyCode.A)) {
			if(lailleron.transform.localRotation != Quaternion.Euler(30, 0, 0)) {
				lailleron.transform.Rotate(1f, 0, 0);
				railleron.transform.Rotate(-1f, 0, 0);
			}
		} else {
			while(lailleron.transform.localRotation.x > 0) {
				lailleron.transform.Rotate(-1f, 0, 0);
				railleron.transform.Rotate(1f, 0, 0);
			}
		}
		// RightRoll
		if(Input.GetKey(KeyCode.D)) {
			if(railleron.transform.localRotation != Quaternion.Euler(30, 0, 0)) {
				railleron.transform.Rotate(1f, 0, 0);
				lailleron.transform.Rotate(-1f, 0, 0);
			}
		} else {
			while(railleron.transform.localRotation.x > 0) {
				railleron.transform.Rotate(-1f, 0, 0);
				lailleron.transform.Rotate(1f, 0, 0);
			}
		}
		
		// PitchUp
		if(Input.GetKey(KeyCode.S)) {
			if(elevator.transform.localRotation != Quaternion.Euler(30, 0, 0))
				elevator.transform.Rotate(1f, 0, 0);
		} else {
			while(elevator.transform.localRotation.x > 0) {
				elevator.transform.Rotate(-1f, 0, 0);
			}
		}
		// PitchDown
		if(Input.GetKey(KeyCode.W)) {
			if(elevator.transform.localRotation != Quaternion.Euler(-30, 0, 0))
				elevator.transform.Rotate(-1f, 0, 0);
		} else {
			while(elevator.transform.localRotation.x < 0) {
				elevator.transform.Rotate(1f, 0, 0);
			}
		}
		
		// YawRight
		if(Input.GetKey(KeyCode.Q)) {
			if(rudder.transform.localRotation != Quaternion.Euler(0, 30, 0))
				rudder.transform.Rotate(0, 1f, 0);
		} else {
			while(rudder.transform.localRotation.y > 0) {
				rudder.transform.Rotate(0, -1f, 0);
			}
		}
		// YawLeft
		if(Input.GetKey(KeyCode.E)) {
			if(rudder.transform.localRotation != Quaternion.Euler(0, -30, 0))
				rudder.transform.Rotate(0, -1f, 0);
		} else {
			while(rudder.transform.localRotation.y < 0) {
				rudder.transform.Rotate(0, 1f, 0);
			}
		}
		
		// FlapsDown
		if(Input.GetKey(KeyCode.F)) {
			if(!flapsCon) {
				flaps.transform.Rotate(-15f, 0, 0);
				flapsCon = true;
			} 
		}

		// FlapsUp	
		if(Input.GetKey(KeyCode.G)) {
			if(flapsCon) {
				flaps.transform.Rotate(15f, 0, 0);
				flapsCon = false;
			} 
		}
	}
		
	//---//
	
	public void Awake() {
		rb = GetComponent<Rigidbody>();
		engineSound = GetComponent<AudioSource>();
	}
	
	public void Start() {
		throttle = start*3;
		rb.velocity = new Vector3(0.0f, 0.0f, start);
	}
	
	public void Update() {
		HandleInputs();
		HandleCommands();
		
		// Sound
		engineSound.volume = throttle * 0.01f;
	}
	
	public void FixedUpdate() {
		// Apply forces to the plane
		rb.AddForce(transform.forward  * thrustMax * throttle);
		rb.AddTorque(transform.up      * yaw   * responseModifier);
		rb.AddTorque(transform.right   * pitch * responseModifier);
		rb.AddTorque(-transform.forward * roll  * responseModifier);
		
		// Flaps out and stall speed
		if(!flapsCon) {
			rb.AddForce(Vector3.up * rb.velocity.magnitude * lift);
			
			if(rb.velocity.magnitude < 15f && altitude > 10) {
				rb.AddForce(Vector3.up * rb.velocity.magnitude * lift * -6f);
				Debug.Log("STALLED!");
			}
		} else {
			rb.AddForce(Vector3.up * rb.velocity.magnitude * lift * 1.3f);
			rb.AddForce(transform.forward * thrustMax * throttle * -0.3f);
			
			if(rb.velocity.magnitude < 10f && altitude > 10) {
				rb.AddForce(Vector3.up * rb.velocity.magnitude * lift * -6f);
				Debug.Log("STALLED!");
			}
		};
		
		// Too high angle of attack
		if(rb.transform.localRotation.x < -0.20f) {
			rb.AddForce(transform.forward * thrustMax * throttle * rb.transform.localRotation.x);
		} else if(rb.transform.localRotation.x > 0.20f) {
			rb.AddForce(transform.forward * thrustMax * throttle * -rb.transform.localRotation.x);
		}
	}
}
