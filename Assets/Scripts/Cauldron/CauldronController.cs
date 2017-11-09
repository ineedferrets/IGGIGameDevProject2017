using System.Collections;
using System.Collections.Generic;
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

    /// <summary>
    /// Initialisation by setting queue and the relative player
    /// </summary>
    void Start() {
        _health = maxHealth;
        ingredientQueue = new List<Ingredient>(2);
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
        Debug.Log(ingredientQueue[0] + " " + ingredientQueue[1]);
        return RecipeCheck();
    }

    /// <summary>
    /// Checks whether the ingredientQueue has enough ingredients to make something with.
    /// </summary>
    /// <returns>Potion that is made when ingredients are mixed. Returns null otherwise.</returns>
    private InteractiveObject RecipeCheck() {
        // Has cauldron got two ingredients?
        if (ingredientQueue[0] != null && ingredientQueue[1] != null) {
            // Return potion that is made by these two ingredients and clear cauldron.
            Potion returnPotion = Recipes.IngredientToPotion[new IngredientCombination(ingredientQueue[0].enumType, ingredientQueue[1].enumType)];
            ingredientQueue.Clear();
            return returnPotion;
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

/// <summary>
/// Static class containing all recipes.
/// </summary>
public static class Recipes
{
    /// <summary>
    /// Dictionary that maps ingredient combinations to a potion.
    /// </summary>
    public static Dictionary<IngredientCombination, Potion> IngredientToPotion
        = new Dictionary<IngredientCombination, Potion>() {
            // 2 x Same Colour = Bomb
            { new IngredientCombination(Ingredient.Type.red, Ingredient.Type.red), new Potion(Potion.Type.bomb) },
            { new IngredientCombination(Ingredient.Type.blue, Ingredient.Type.blue), new Potion(Potion.Type.bomb) },

            // Different colours = wall
            { new IngredientCombination(Ingredient.Type.red, Ingredient.Type.blue), new Potion(Potion.Type.wall) },
            { new IngredientCombination(Ingredient.Type.blue, Ingredient.Type.red), new Potion(Potion.Type.wall) }
        };
}

/// <summary>
/// Wrapper that contains two ingredients.
/// </summary>
public class IngredientCombination {
    public Ingredient.Type mIngredient1;
    public Ingredient.Type mIngredient2;

    public IngredientCombination (Ingredient.Type ingredient1, Ingredient.Type ingredient2) {
        mIngredient1 = ingredient1;
        mIngredient2 = ingredient2;
    }
}