using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CauldronController : MonoBehaviour, IDestructable {

    public float maxHealth = 100;

    private float _health;
    public float health {
        get {
            return _health;
        }
    }

    /// <summary>
    /// Ingredient pool within the cauldron.
    /// </summary>
    public List<Ingredient> ingredientQueue;

    public PlayerController player;

    private List<Potion> craftablePotions;

    /// <summary>
    /// Initialisation by setting queue and the relative player
    /// </summary>
    void Start() {
        _health = maxHealth;
        ingredientQueue = new List<Ingredient>(2);

        craftablePotions = new List<Potion>();
        foreach (Potion.Type potType in Enum.GetValues(typeof(Potion.Type)))
        {
            craftablePotions.Add(new Potion(potType));
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Explosion") {
            Destroy(player.gameObject);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Transfers an ingredient to the cauldron.
    /// </summary>
    /// <param name="item">The object transferred to the cauldron.</param>
    /// <returns>Depending on the type of item given, returns either a potion, null or the ingredient in the cauldron.</returns>
    public InteractiveObject AddIngredient(InteractiveObject item) {
        // Has the player given a null whilst there is an ingredient in the cauldron? Then give back the ingredient in the potion.
        if (item == null && ingredientQueue[0] != null) {
            InteractiveObject returnObject;
            returnObject = ingredientQueue[0];
            ingredientQueue.Clear();
            return returnObject;
        }

        // Cast item as ingredient.
        Ingredient ingredient = item as Ingredient;

        // Has the player given something that isn't an ingredient (which also includes null)? Return item.
        if (ingredient == null) {
            return item;
        }

        // Add ingredient to recipe queue and check whether there are enough ingredients to make a potion.
        ingredientQueue.Add(ingredient);
        Debug.Log(ingredientQueue.Count);
        return RecipeCheck();
    }

    /// <summary>
    /// Checks whether the ingredientQueue has enough ingredients to make something with.
    /// </summary>
    /// <returns>Potion that is made when ingredients are mixed. Returns null otherwise.</returns>
    private InteractiveObject RecipeCheck() {
        // Has cauldron got two ingredients?
        if (ingredientQueue.Count == 2) {
            InteractiveObject returnObject = null;

            string craftingID = "";
            for (int i = 0; i < ingredientQueue.Count; i++) {
                craftingID += ingredientQueue[i].itemID;
                if (i != ingredientQueue.Count - 1) {
                    craftingID += " ";
                }
            }

            foreach (Potion potion in craftablePotions) {
                if (potion.craftingIDs.Contains(craftingID)) {
                    returnObject = potion;
                    ingredientQueue.Clear();
                    break;
                }
            }

            if (returnObject == null) {
                returnObject = ingredientQueue[ingredientQueue.Count - 1];
                ingredientQueue.Remove(returnObject as Ingredient);
            }

            return returnObject;
        } else {
            // Return a null.
            return null;
        }

    }

    public bool ApplyDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0) {
            return true;
        } else {
            return false;
        }
    }
}