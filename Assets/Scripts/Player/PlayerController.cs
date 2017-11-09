using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Player number for game. Private field is only changed in Debug.
    [SerializeField]
    [Tooltip("Player numbers start from 1.")]
    private int _mPlayerNumber = 0;
    public int PlayerNumber {
        get {
            return _mPlayerNumber;
        }
    }

    // Reference to players home cauldron
    public CauldronController playerCauldron;

    // Left face button inventory slot. Publicly accessible. Privately assigned.
    [SerializeField]
    private InteractiveObject _leftInventorySlot;
    public InteractiveObject leftInventorySlot { get { return _leftInventorySlot; } }

    // Up face button inventory slot. Publicly accessible. Privately assigned.
    [SerializeField]
    private InteractiveObject _upInventorySlot;
    public InteractiveObject upInventorySlot { get { return _upInventorySlot; } }

    // Right face button inventory slot. Publicly accessible. Privately assigned.
    [SerializeField]
    private InteractiveObject _rightInventorySlot;
    public InteractiveObject rightInventorySlot { get { return _rightInventorySlot; } }

    // Down face button inventory slot. Publicly accessible. Privately assigned.
    [SerializeField]
    private InteractiveObject _downInventorySlot;
    public InteractiveObject downInventorySlot { get { return _downInventorySlot; } }

	// Inventory slots start as empty.
	void Start () {
        _leftInventorySlot = null;
        _upInventorySlot = null;
        _rightInventorySlot = null;
        _downInventorySlot = null;
	}

    private bool collisionHappening = false;

    // Check if player is pressing a button and drop object if so.
    void Update() {

        if (!collisionHappening) {
            if(Input.GetButtonDown("LeftFace" + _mPlayerNumber) && _leftInventorySlot != null) {
                _leftInventorySlot.SpawnObject(transform.position);
                _leftInventorySlot = null;
            } else if (Input.GetButtonDown("UpFace" + _mPlayerNumber) && _upInventorySlot != null) {
                _upInventorySlot.SpawnObject(transform.position);
                _upInventorySlot = null;
            } else if (Input.GetButtonDown("RightFace" + _mPlayerNumber) && _rightInventorySlot != null) {
                _rightInventorySlot.SpawnObject(transform.position);
                _rightInventorySlot = null;
            } else if (Input.GetButtonDown("DownFace" + _mPlayerNumber) && _downInventorySlot != null) {
                _downInventorySlot.SpawnObject(transform.position);
                _downInventorySlot = null;
            }
        }
    }

    // On trigger stay (used instead of enter since buttons are also checked).
    void OnTriggerStay(Collider other) {
        // Check to see if collided with cauldron.
        CauldronController cauldron = other.GetComponent<CauldronController>();
        bool cauldronCollision = CauldronCheckAndRun(cauldron);

        // Check to see if collided with ingredient
        IngredientInformation ingredient = other.GetComponent<IngredientInformation>();
        bool ingredientCollision = IngredientCheckAndRun(ingredient);
        if (ingredientCollision) {
            Destroy(other.gameObject);
        }

        collisionHappening = cauldronCollision || ingredientCollision;
    }

    /// <summary>
    /// Function for checking if the CauldronCrafting component is null and assumes a none null value means a cauldron.
    /// </summary>
    /// <param name="cauldron">CauldronCrafting component of cauldron.</param>
    /// <returns>Returns whether the CauldronCrafting component is not null.</returns>
    private bool CauldronCheckAndRun(CauldronController cauldron) {
        // Does the collided object contain a CauldronCrafting component? I.e. is it a cauldron.
        if (cauldron != null && cauldron == playerCauldron) {
            if (Input.GetButtonDown("LeftFace" + _mPlayerNumber)) {
                // Give left inventory slot to cauldron (even if empty)
                _leftInventorySlot = cauldron.AddIngredient(_leftInventorySlot);
            } else if (Input.GetButtonDown("UpFace" + _mPlayerNumber)) {
                // Give up inventory slot to cauldron (even if empty)
                _upInventorySlot = cauldron.AddIngredient(_upInventorySlot);
            } else if (Input.GetButtonDown("RightFace" + _mPlayerNumber)) {
                // Give right inventory slot to cauldron (even if empty)
                _rightInventorySlot = cauldron.AddIngredient(_rightInventorySlot);
            } else if (Input.GetButtonDown("DownFace" + _mPlayerNumber)) {
                // Give down inventory slot to cauldron (even if empty)
                _downInventorySlot = cauldron.AddIngredient(_downInventorySlot);
            }
            // Collision is with a cauldron.
            return true;
        }
        // Collision is not with a cauldron.
        return false;
    }

    /// <summary>
    /// Function for checking if the IngredientInformation component is null and assumes a none null value means an ingredient.
    /// </summary>
    /// <param name="ingredient">IngredientInformation component.</param>
    /// <returns>Returns whether the IngredientInformation component is not null.</returns>
    private bool IngredientCheckAndRun(IngredientInformation ingredient) {
        // Does the collided object contain an INgredientInformation component? I.e. is it an ingredient
        if (ingredient != null) {
            if (Input.GetButtonDown("LeftFace" + _mPlayerNumber)) {
                // Is the left inventory slot occuppied?
                switch (_leftInventorySlot == null)
                {
                    // Pick up ingredient.
                    case true:
                        _leftInventorySlot = ingredient.mIngredient;
                        return true;
                    // Swap ingredient with inventory slot.
                    case false:
                        InteractiveObject intermediary = _leftInventorySlot;
                        _leftInventorySlot = ingredient.mIngredient;
                        intermediary.SpawnObject(transform.position);
                        return true;
                }
            } else if (Input.GetButtonDown("UpFace" + _mPlayerNumber)) {
                // Is the up inventory slot occuppied?
                switch (_upInventorySlot == null)
                {
                    // Pick up ingredient.
                    case true:
                        _upInventorySlot = ingredient.mIngredient;
                        return true;
                    // Swap ingredient with inventory slot.
                    case false:
                        InteractiveObject intermediary = _upInventorySlot;
                        _upInventorySlot = ingredient.mIngredient;
                        intermediary.SpawnObject(transform.position);
                        return true;
                }
            } else if (Input.GetButtonDown("RightFace" + _mPlayerNumber)) {
                // Is the right inventory slot occuppied?
                switch (_rightInventorySlot == null)
                {
                    // Pick up ingredient.
                    case true:
                        _rightInventorySlot = ingredient.mIngredient;
                        return true;
                    // Swap ingredient with inventory slot.
                    case false:
                        InteractiveObject intermediary = _rightInventorySlot;
                        _rightInventorySlot = ingredient.mIngredient;
                        intermediary.SpawnObject(transform.position);
                        return true;
                }
            } else if (Input.GetButtonDown("DownFace" + _mPlayerNumber)) {
                // Is the down inventory slot occuppied?
                switch (_downInventorySlot == null)
                {
                    // Pick up ingredient.
                    case true:
                        _downInventorySlot = ingredient.mIngredient;
                        return true;
                    // Swap ingredient with inventory slot.
                    case false:
                        InteractiveObject intermediary = _downInventorySlot;
                        _downInventorySlot = ingredient.mIngredient;
                        intermediary.SpawnObject(transform.position);
                        return true;
                }
            }
            return false;
        }
        // Collision is not with ingredient.
        return false;
    }
}
