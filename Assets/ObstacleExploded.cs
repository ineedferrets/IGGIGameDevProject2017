using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleExploded : MonoBehaviour {

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Explosion") {
			Destroy(gameObject);
		}
	}
	
	
}
