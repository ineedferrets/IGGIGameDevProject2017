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
            leftButton.enabled = true;
        } else {
            leftButton.sprite = null;
            leftButton.enabled = false;
        }

        if (player.upInventorySlot != null) {
            upButton.sprite = player.upInventorySlot.uiSprite;
            upButton.enabled = true;
        } else {
            upButton.sprite = null;
            upButton.enabled = false;
        }


        if (player.rightInventorySlot != null) {
            rightButton.sprite = player.rightInventorySlot.uiSprite;
            rightButton.enabled = true;
        } else {
            rightButton.sprite = null;
            rightButton.enabled = false;
        }


        if (player.downInventorySlot != null) {
            downButton.sprite = player.downInventorySlot.uiSprite;
            downButton.enabled = true;
        } else {
            downButton.sprite = null;
            downButton.enabled = false;
        }

    }
}
