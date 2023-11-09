using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
	
	private float roll;
	private float pitch;
	private float yaw;
	
	// Passed to hudUpdate.cs
	public static float throttle;
	public static float altitude;
	public static Rigidbody rb;
	public static bool flapsCon = false;
	
	// Audio
	public AudioSource engineSound;
	public AudioSource sirenSound;
	
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
	
	//UI Buttons//
	
	//UI buttons (legacy)
	public Canvas uiCanvas;
	
	public Button pitchupButton;   
	public Button pitchdownButton; 
	public Button rollleftButton;  
	public Button rollrightButton; 
	public Button yawleftButton;   
	public Button yawrightButton;
	
	public float buttonPitchUp;
	public float buttonPitchDown;
	public float buttonRollRight;
	public float buttonRollLeft;
	public float buttonYawRight;
	public float buttonYawLeft;
	
	//-Handle UI buttons-//
	
	public void pitchPlaneDownButton(bool statusPitchDown) {
		if(statusPitchDown)
		    buttonPitchDown = 1.0f;
		else
			buttonPitchDown = 0.0f;
	}
	public void pitchPlaneUpButton(bool statusPitchUp) {
		if(statusPitchUp)
			buttonPitchUp = -1.0f;
		else
			buttonPitchUp = 0.0f;
	}

	public void rollPlaneRightButton(bool statusRoll) {
		if(statusRoll)
			buttonRollRight = 1.0f;
		else
			buttonRollRight = 0.0f;
	}
	public void rollPlaneLeftButton(bool statusRoll) {
		if(statusRoll)
			buttonRollLeft = -1.0f;
		else
			buttonRollLeft = 0.0f;
	}
	
	public void yawPlaneRightButton(bool statusYaw) {
		if(statusYaw)
			buttonYawRight = 1.0f;
		else
			buttonYawRight = 0.0f;
	}
	public void yawPlaneLeftButton(bool statusYaw) {
		if(statusYaw)
			buttonYawLeft = -1.0f;
		else
			buttonYawLeft = 0.0f;
	}
	
	public void flapsDown() {
		if(!flapsCon) {
			flaps.transform.Rotate(-15f, 0, 0);
			flapsCon = true;
		} 
	}
	
	public void flapsUp() {
		if(flapsCon) {
			flaps.transform.Rotate(15f, 0, 0);
			flapsCon = false;
		} 
	}
	
	//---//
	
	public void HandleInputs() {
		// Set rotation from axis inputs
		roll  = Input.GetAxis("Roll");
		pitch = Input.GetAxis("Pitch");
		yaw   = Input.GetAxis("Yaw");
		
		if(menuScript.mobileOn) {
			roll  = buttonRollRight + buttonRollLeft; 
			pitch = buttonPitchDown + buttonPitchUp;
			yaw   = buttonYawRight  + buttonYawLeft;
		}
		
		// Set throttle behaviour
		if(menuScript.mobileOn) {
			throttle = hudUpdater.sliderValue;
		} else {
			if(Input.GetKey(KeyCode.Space)) {
				throttle += throttleIncrement;
			} else if(Input.GetKey(KeyCode.LeftShift)) {
				throttle -= throttleIncrement;
			}
		}
		
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
		if(Input.GetKey(KeyCode.A) || (buttonRollLeft!=0f) ) {
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
		if(Input.GetKey(KeyCode.D) || (buttonRollRight!=0f) ) {
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
		if(Input.GetKey(KeyCode.S) || (buttonPitchUp!=0f) ) {
			if(elevator.transform.localRotation != Quaternion.Euler(30, 0, 0))
				elevator.transform.Rotate(1f, 0, 0);
		} else {
			while(elevator.transform.localRotation.x > 0) {
				elevator.transform.Rotate(-1f, 0, 0);
			}
		}
		// PitchDown
		if(Input.GetKey(KeyCode.W) || (buttonPitchDown != 0f) ) {
			if(elevator.transform.localRotation != Quaternion.Euler(-30, 0, 0))
				elevator.transform.Rotate(-1f, 0, 0);
		} else {
			while(elevator.transform.localRotation.x < 0) {
				elevator.transform.Rotate(1f, 0, 0);
			}
		}
		
		// YawLeft
		if(Input.GetKey(KeyCode.Q) || (buttonYawLeft!=0f) ) {
			if(rudder.transform.localRotation != Quaternion.Euler(0, 30, 0))
				rudder.transform.Rotate(0, 1f, 0);
		} else {
			while(rudder.transform.localRotation.y > 0) {
				rudder.transform.Rotate(0, -1f, 0);
			}
		}
		// YawRight
		if(Input.GetKey(KeyCode.E) || (buttonYawRight!=0f) ) {
			if(rudder.transform.localRotation != Quaternion.Euler(0, -30, 0))
				rudder.transform.Rotate(0, -1f, 0);
		} else {
			while(rudder.transform.localRotation.y < 0) {
				rudder.transform.Rotate(0, 1f, 0);
			}
		}
		
		// FlapsDown
		if(Input.GetKey(KeyCode.F)) {
			flapsDown();
		}

		// FlapsUp	
		if(Input.GetKey(KeyCode.G)) {
			flapsUp();			
		}
	}
		
	//---//
	
	public void Awake() {
		rb = GetComponent<Rigidbody>();
	}
	
	public void Start() {
		// Canvas
		if(menuScript.mobileOn)	
			uiCanvas.enabled = true;
		else
			uiCanvas.enabled = false;
		
		// Starting values
		throttle = start*3;
		hudUpdater.sliderValue = throttle;
		rb.velocity = new Vector3(0.0f, 0.0f, start);
	}
	
	public void Update() {
		HandleInputs();
		HandleCommands();
		
		// Sound
		if(menuScript.soundOn)
			engineSound.volume = throttle * 0.01f;
		else
			engineSound.volume = 0f;
		
		if(menuScript.soundOn) {
			if(altitude > 5f) {
				if(!flapsCon)
					sirenSound.volume  = -(rb.velocity.magnitude - 20f) * 0.05f; 
				else
					sirenSound.volume  = -(rb.velocity.magnitude - 15f) * 0.05f; 
			}
		} else {
			sirenSound.volume = 0.00f;
		}
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
			}
		} else {
			rb.AddForce(Vector3.up * rb.velocity.magnitude * lift * 1.3f);
			rb.AddForce(transform.forward * thrustMax * throttle * -0.3f);
			
			if(rb.velocity.magnitude < 10f && altitude > 10) {
				rb.AddForce(Vector3.up * rb.velocity.magnitude * lift * -6f);
			}
		};
		
		// Too high angle of attack
		if(rb.transform.localRotation.x < -0.20f) {
			rb.AddForce(transform.forward * thrustMax * throttle * rb.transform.localRotation.x);
		}
	}
}
