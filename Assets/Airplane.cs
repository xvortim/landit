using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
	public float thrust   = 30f;
	public float defaultLift = -0.001f;
	public float defaultMotion = 0.5f;

	Rigidbody rb;
	
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		rb.velocity = transform.forward * thrust;
		transform.Rotate(defaultLift, 0, 0);
		
		// Airplane Controls
		
		if(Input.GetKey(KeyCode.W)) { transform.Rotate(defaultMotion,0,0);     };
			
		if(Input.GetKey(KeyCode.S)) { transform.Rotate(-(defaultMotion),0,0);  };
			
		if(Input.GetKey(KeyCode.A)) { transform.Rotate(0,0,defaultMotion);     };
		
		if(Input.GetKey(KeyCode.D)) { transform.Rotate(0,0,-(defaultMotion));  };
    }
}
