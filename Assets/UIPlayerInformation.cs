using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInformation : MonoBehaviour {

    public PlayerInformation player;

    public Image leftButton;
    public Image upButton;
    public Image rightButton;
    public Image downButton;
	
	// Update is called once per frame
	void Update () {
        leftButton.sprite = player.leftInventorySlot.uiSprite;
        upButton.sprite = player.upInventorySlot.uiSprite;
        rightButton.sprite = player.rightInventorySlot.uiSprite;
        downButton.sprite = player.downInventorySlot.uiSprite;
    }
}
