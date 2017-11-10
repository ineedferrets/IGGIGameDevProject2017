using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInformation : MonoBehaviour {

    [SerializeField]
    private Ingredient.Type IngredientType;
    public Ingredient.Type ingredientType { get { return IngredientType; } }

    /// <summary>
    /// The ingredient object holding.
    /// </summary>
    public Ingredient mIngredient;

    /// <summary>
    /// On scene start.
    /// </summary>
    private void Start() {
        mIngredient = new Ingredient(IngredientType);
    }
}

/// <summary>
/// Ingredient class inherits from interactive object. Contains all possible ingredient types.
/// </summary>
public class Ingredient : InteractiveObject {

    public enum Type { red, blue };

    private Type _ingType;
    public Type ingType { get { return _ingType; } }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="ingredientType"></param>
    public Ingredient(Type ingredientType) : base(ingredientType.ToString() +"ingredient") {
        _ingType = ingredientType;
    }
}