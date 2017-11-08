using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInformation : MonoBehaviour {

    // Item Ingredient Type is set in editor.
    [SerializeField]
    private Ingredient.Type _itemIngredient;
    public Ingredient.Type itemIngredient {
        get {
            return _itemIngredient;
        }
    }

    /// <summary>
    /// The ingredient object holding.
    /// </summary>
    public Ingredient mIngredient { get; private set; }

    /// <summary>
    /// On scene start.
    /// </summary>
    private void Awake() {
        mIngredient = new Ingredient(_itemIngredient);
    }
}

/// <summary>
/// Ingredient class inherits from interactive object. Contains all possible ingredient types.
/// </summary>
public class Ingredient : InteractiveObject {
    /// <summary>
    /// Enum type of ingredient.
    /// </summary>
    public enum Type { red, blue };

    // Publicly accessible ingredient type. Privately assigned.
    private Type _enumType;
    public Type enumType {
        get {
            return _enumType;
        }
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="ingredientType"></param>
    public Ingredient(Type ingredientType) : base(ingredientType.ToString()+"ingredient") { _enumType = ingredientType; }
}