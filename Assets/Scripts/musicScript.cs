using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicScript : MonoBehaviour
{
	public AudioSource musicSound;
	
    void Update()
    {
        if(menuScript.soundOn)
			musicSound.volume = 1f;
		else
			musicSound.volume = 0f;
    }
}
