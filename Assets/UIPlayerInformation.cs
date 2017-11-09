using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInformation : MonoBehaviour {

    public PlayerController player;

    public Image leftButton;
    public Image upButton;
    public Image rightButton;
    public Image downButton;
	
	// Update is called once per frame
	void Update () {
        if (player.leftInventorySlot != null) {
            leftButton.sprite = player.leftInventorySlot.uiSprite;
        } else {
            leftButton.sprite = null;
        }

        if (player.upInventorySlot != null) {
            upButton.sprite = player.upInventorySlot.uiSprite;
        } else {
            upButton.sprite = null;
        }


        if (player.rightInventorySlot != null) {
            rightButton.sprite = player.rightInventorySlot.uiSprite;
        } else {
            rightButton.sprite = null;
        }


        if (player.downInventorySlot != null) {
            downButton.sprite = player.downInventorySlot.uiSprite;
        } else {
            downButton.sprite = null;
        }

    }
}
