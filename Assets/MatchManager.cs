using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour {

    public List<PlayerController> allPlayers;
    public Text winText;
	
    // poop

	// Update is called once per frame
	void Update () {
		if (allPlayers[0] == null) {
            winText.text = "Game Over! Player 2 wins!";
            winText.enabled = true;
        }
        else if (allPlayers[1] == null)
        {
            winText.text = "Game Over! Player 1 wins!";
            winText.enabled = true;
        }

        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
