using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("Camera positions")]
	[SerializeField] Transform[] povs;
	[Tooltip("Camera follow speed")]
	[SerializeField] float speed;
	
	private int index = 0;
	private Vector3 target;
	
	private void Update() {
		if(Input.GetKeyDown(KeyCode.Alpha1)) 
			index = 0;
		else if(Input.GetKeyDown(KeyCode.Alpha2)) 
			index = 1;
		else if(Input.GetKeyDown(KeyCode.Alpha3)) 
			index = 2;
		else if(Input.GetKeyDown(KeyCode.Alpha4)) 
			index = 3;
		
		target = povs[index].position;
	}
	
	private void LateUpdate() {
		transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
		transform.forward  = povs[index].forward;
	}
}
