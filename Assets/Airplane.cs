using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
	public float thrust   = 10f;
	public float maxSpeed = 20f;

	Rigidbody rb;
	
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude < maxSpeed) {
			rb.AddRelativeForce(new Vector3(0, 0, thrust));
		}
    }
}
