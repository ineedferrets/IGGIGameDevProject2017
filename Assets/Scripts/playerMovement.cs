using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	public float speed = 1;
	public int playerNumber = 1;
	private float boundX;
	private float boundY;
	
	void Start () {
		GameObject map = GameObject.Find("Map");
		MapGenerator mapGen = map.GetComponent<MapGenerator>();
		boundX = (mapGen.getX() / 2) - 0.5f;
		boundY = (mapGen.getY() / 2) - 0.5f;
		print("X Boundary " + boundX +", Y Boundary " + boundY);
		
	}
	// Update is called once per frame
	void Update () {
        UpdatePlayer();
	}
	
	void UpdatePlayer(){
        float hori = Input.GetAxis ("Horizontal"+playerNumber);
        float vert = Input.GetAxis ("Vertical"+playerNumber);
		Vector3 newPosition = transform.position;
		Vector3 movement = new Vector3(hori * speed, vert * speed, 0);
		transform.Translate(movement * Time.deltaTime);
		if (transform.position.x >= boundX) {
			Vector3 temp = new Vector3(boundX, transform.position.y, 0);
			transform.position = temp;
		}
		if (transform.position.x <= -boundX) {
			Vector3 temp = new Vector3(-boundX, transform.position.y, 0);
			transform.position = temp;
		}
		if (transform.position.y >= boundY) {
			Vector3 temp = new Vector3(transform.position.x, boundY, 0);
			transform.position = temp;
		}
		if (transform.position.y <= -boundY) {
			Vector3 temp = new Vector3(transform.position.x, -boundY, 0);
			transform.position = temp;
		}
		
	}
}
