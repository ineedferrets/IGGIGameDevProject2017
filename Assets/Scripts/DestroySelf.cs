using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour {

    public float Delay = 3f; // Delay in seconds before destroying the explosion particle
	
    void Start() {
        Destroy(gameObject, Delay);
    }
	
}
