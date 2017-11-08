using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerInformation : MonoBehaviour {

    [SerializeField]
    private int _mPlayerNumber = 0;
    public int PlayerNumber {
        get {
            return _mPlayerNumber;
        }
    }

    public enum FaceButton { left, up, right, down };

    private InteractiveObject _leftInventorySlot;
    public InteractiveObject leftInventorySlot { get { return _leftInventorySlot; } }

    private InteractiveObject _upInventorySlot;
    public InteractiveObject upInventorySlot { get { return _upInventorySlot; } }

    private InteractiveObject _rightInventorySlot;
    public InteractiveObject rightInventorySlot { get { return _rightInventorySlot; } }

    private InteractiveObject _downInventorySlot;
    public InteractiveObject downInventorySlot { get { return _downInventorySlot; } }

	// Use this for initialization
	void Start () {
        _leftInventorySlot = null;
        _upInventorySlot = null;
        _rightInventorySlot = null;
        _downInventorySlot = null;
	}

    public InteractiveObject GetItemFromInventory(FaceButton pressedButton)
    {
        switch (pressedButton)
        {
            case FaceButton.left:
                return QueryAndResetInventory(_leftInventorySlot);
            case FaceButton.up:
                return QueryAndResetInventory(_upInventorySlot);
            case FaceButton.right:
                return QueryAndResetInventory(_rightInventorySlot);
            case FaceButton.down:
                return QueryAndResetInventory(_downInventorySlot);
            default:
                return new InteractiveObject("");
        }
    }

    public bool GiveItem(InteractiveObject givenItem) {
        System.Array allButtons = Enum.GetValues(typeof(FaceButton));
        foreach (FaceButton button in allButtons)
        {
            if (GiveItemToButton(givenItem, button))
            {
                return true;
            }
        }

        return false;
    }

    public bool GiveItemToButton(InteractiveObject givenItem, FaceButton button) {
        if (IsInventoryEmpty(button))
        {
            switch (button)
            {
                case FaceButton.left:
                    _leftInventorySlot = givenItem;
                    break;
                case FaceButton.up:
                    _upInventorySlot = givenItem;
                    break;
                case FaceButton.right:
                    _rightInventorySlot = givenItem;
                    break;
                case FaceButton.down:
                    _downInventorySlot = givenItem;
                    break;
                default:
                    return false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private InteractiveObject QueryAndResetInventory(InteractiveObject queriedButton)
    {
        InteractiveObject returnObject;
        returnObject = queriedButton;
        queriedButton = new InteractiveObject("");
        return queriedButton;
    }

    private bool IsInventoryEmpty(FaceButton button) {
        switch (button)
        {
            case FaceButton.left:
                return _leftInventorySlot == null;
            case FaceButton.up:
                return _upInventorySlot.mName == null;
            case FaceButton.right:
                return _rightInventorySlot == null;
            case FaceButton.down:
                return _downInventorySlot == null;
            default:
                throw new System.Exception("That is not a viable button");
        }
    }
}
