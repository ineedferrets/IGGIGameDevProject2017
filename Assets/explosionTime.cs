using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionTime : MonoBehaviour {

	private Transform[] obstacle;
	// Use this for initialization
	void Start () {
		Invoke("Explosion", 5f);
	}
	
	void Explosion () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		
	}
}
