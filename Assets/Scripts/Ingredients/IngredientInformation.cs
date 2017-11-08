using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInformation : MonoBehaviour {

    [SerializeField]
    private Ingredient.IngredientType _itemIngredient;
    public Ingredient.IngredientType itemIngredient {
        get {
            return _itemIngredient;
        }
    }

    public Ingredient mIngredient { get; private set; }

    private void Awake() {
        if (itemIngredient == Ingredient.IngredientType.red)
            mIngredient = new RedIngredient();
        else if (itemIngredient == Ingredient.IngredientType.blue)
            mIngredient = new BlueIngredient();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if(other.GetComponent<PlayerInformation>().GiveItem(mIngredient))
            {
                Destroy(gameObject);
            }
        }
    }
}


public abstract class Ingredient : InteractiveObject {
    public enum IngredientType { red, blue };

    private IngredientType _mIngredient;
    public IngredientType mIngredient {
        get {
            return _mIngredient;
        }
    }

    public Ingredient(IngredientType ingredientType) : base(ingredientType.ToString()+"ingredient") { _mIngredient = ingredientType; }
}

public class RedIngredient : Ingredient {
    public RedIngredient() : base (IngredientType.red) { }
}

public class BlueIngredient : Ingredient {
    public BlueIngredient() : base (IngredientType.blue) { }
}