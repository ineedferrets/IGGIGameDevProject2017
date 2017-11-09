using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionTime : MonoBehaviour {

	public Transform explosionPrefab;
	
	// Use this for initialization
	void Start () {
		Invoke("Explosion", 1f);
	}
	
	void Explosion () {
		Vector3 temp = new Vector3(0f,0f,-0.2f);
		Vector3 position = transform.position + temp;
		Instantiate(explosionPrefab, transform.position + temp, explosionPrefab.transform.rotation);
	}
	
}
