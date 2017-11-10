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
            leftButton.color = player.leftInventorySlot.uiSpriteColor;
            leftButton.enabled = true;
        } else {
            leftButton.sprite = null;
            leftButton.enabled = false;
        }

        if (player.upInventorySlot != null) {
            upButton.sprite = player.upInventorySlot.uiSprite;
            upButton.color = player.upInventorySlot.uiSpriteColor;
            upButton.enabled = true;
        } else {
            upButton.sprite = null;
            upButton.enabled = false;
        }


        if (player.rightInventorySlot != null) {
            rightButton.sprite = player.rightInventorySlot.uiSprite;
            rightButton.color = player.rightInventorySlot.uiSpriteColor;
            rightButton.enabled = true;
        } else {
            rightButton.sprite = null;
            rightButton.enabled = false;
        }


        if (player.downInventorySlot != null) {
            downButton.sprite = player.downInventorySlot.uiSprite;
            downButton.color = player.downInventorySlot.uiSpriteColor;
            downButton.enabled = true;
        } else {
            downButton.sprite = null;
            downButton.enabled = false;
        }

    }
}
