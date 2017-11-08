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
        if (playerNumber == 1) {
			UpdatePlayer1();
		} 
		if (playerNumber == 2) {
			UpdatePlayer2();
		}
	}
	
	void UpdatePlayer1 (){
        float hori = Input.GetAxis ("Horizontal1");
        float vert = Input.GetAxis ("Vertical1");
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
	
	void UpdatePlayer2 (){
        float hori = Input.GetAxis ("Horizontal2");
        float vert = Input.GetAxis ("Vertical2");
		Vector3 newPosition = transform.position;
		Vector3 movement = new Vector3(hori*speed, vert*speed, 0);
		newPosition += movement;
		transform.position = newPosition;
		
	}
}
