using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallBuild : MonoBehaviour {

	public Transform obstaclePrefab; 
	private bool obstaclePlaced = false;
	
	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Build" && obstaclePlaced == false) {
			obstaclePlaced = true;
			Transform newObstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity) as Transform; 
		}
	}
}
