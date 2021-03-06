﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Player number for game. Private field is only changed in Debug.
    [SerializeField]
    [Tooltip("Player numbers start from 1.")]
    private int _mPlayerNumber = 0;
	private bool playerExploded;
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
		playerExploded = false;
	}

    private bool collidingWithInteractiveObject = false;

    // Check if player is pressing a button and drop object if so.
    void Update() {
        if (!collidingWithInteractiveObject) {
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
        collidingWithInteractiveObject = false;
    }

    // On trigger stay (used instead of enter since buttons are also checked).
    void OnTriggerStay(Collider other) {
        if (other.isTrigger)
        {
            // Check to see if collided with cauldron.
            CauldronController cauldron = other.GetComponent<CauldronController>();
            bool cauldronCollision = false;
            if (cauldron != null)
            {
                cauldronCollision = CauldronCheckAndRun(cauldron);
            }

            Debug.Log("Colliding with my cauldron: " + cauldronCollision);

            // Check to see if collided with ingredient
            IngredientInformation ingredient = other.GetComponent<IngredientInformation>();
            bool ingredientCollision = ingredient != null;
            if (!cauldronCollision && ingredientCollision)
            {
                IngredientCheckAndRun(ingredient);
            }

            Debug.Log("Colliding with ingredient: " + ingredientCollision);

            collidingWithInteractiveObject = cauldronCollision || ingredientCollision;

            // EXPLOSION
            if (other.gameObject.tag == "Explosion" && playerExploded == false)
            {
                print("Oh dear i'm dead");
                playerExploded = true;
                PlayerDeath();
                playerExploded = false;
            }
        }
    }

    private void PlayerDeath() {
        transform.position = playerCauldron.transform.position + new Vector3(0f, 0f, -0.2f);
    }

    /// <summary>
    /// Function for checking if the CauldronCrafting component is null and assumes a none null value means a cauldron.
    /// </summary>
    /// <param name="cauldron">CauldronCrafting component of cauldron.</param>
    /// <returns>Returns whether the CauldronCrafting component is owned by the player.</returns>
    private bool CauldronCheckAndRun(CauldronController cauldron) {
        // Does the collided object contain a CauldronCrafting component? I.e. is it a cauldron.
        if (cauldron == playerCauldron) {
            Debug.Log("I Have Collided with my cauldron!");
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
        if (Input.GetButtonDown("LeftFace" + _mPlayerNumber)) {
            // Is the left inventory slot occuppied?
            switch (_leftInventorySlot == null)
            {
                // Pick up ingredient.
                case true:
                    Debug.Log("Slot is empty");
                    _leftInventorySlot = ingredient.mIngredient;
                    Destroy(ingredient.gameObject);
                    return true;
                // Swap ingredient with inventory slot.
                case false:
                    Debug.Log("Slot is not empty");
                    InteractiveObject intermediary = _leftInventorySlot;
                    _leftInventorySlot = ingredient.mIngredient;
                    Destroy(ingredient.gameObject);
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
                    Destroy(ingredient.gameObject);
                    return true;
                // Swap ingredient with inventory slot.
                case false:
                    InteractiveObject intermediary = _upInventorySlot;
                    _upInventorySlot = ingredient.mIngredient;
                    Destroy(ingredient.gameObject);
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
                    Destroy(ingredient.gameObject);
                    return true;
                // Swap ingredient with inventory slot.
                case false:
                    InteractiveObject intermediary = _rightInventorySlot;
                    _rightInventorySlot = ingredient.mIngredient;
                    Destroy(ingredient.gameObject);
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
                    Destroy(ingredient.gameObject);
                    return true;
                // Swap ingredient with inventory slot.
                case false:
                    InteractiveObject intermediary = _downInventorySlot;
                    _downInventorySlot = ingredient.mIngredient;
                    Destroy(ingredient.gameObject);
                    intermediary.SpawnObject(transform.position);
                    return true;
            }
        }
        return false;
    }
}
