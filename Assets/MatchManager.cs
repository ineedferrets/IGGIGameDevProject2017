using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {

    public List<PlayerController> allPlayers;
    public Text winText;
	
	// Update is called once per frame
	void Update () {
		if (allPlayers.Count == 1) {
            winText.text = "Game Over! " + allPlayers[0].name + " wins!";
            winText.enabled = true;
        }
	}
}
