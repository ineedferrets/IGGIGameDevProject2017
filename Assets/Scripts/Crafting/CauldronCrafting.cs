using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronCrafting : MonoBehaviour {

    public List<Ingredient> ingredientQueue;
    public PlayerInformation playerInformationComponent;

    private int _mPlayerNumber;

    /// <summary>
    /// Initialisation by setting queue and the relative player
    /// </summary>
    void Start() {
        ingredientQueue = new List<Ingredient>(2);
        _mPlayerNumber = playerInformationComponent.PlayerNumber;
    }

    /// <summary>
    /// When something triggers the collider (only the couldrons relative player can do anything)
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other) {
        if (other.Equals(playerInformationComponent.gameObject.GetComponent<Collider>())) {
            InteractiveObject returnedItem = null;
            PlayerInformation.FaceButton buttonPressed;

            if (Input.GetButtonDown("LeftFace" + _mPlayerNumber))
            {
                buttonPressed = PlayerInformation.FaceButton.left;
            }
            else if (Input.GetButtonDown("UpFace" + _mPlayerNumber))
            {
                buttonPressed = PlayerInformation.FaceButton.up;
            }
            else if (Input.GetButtonDown("RightFace" + _mPlayerNumber))
            {
                buttonPressed = PlayerInformation.FaceButton.right;
            }
            else if (Input.GetButtonDown("DownFace" + _mPlayerNumber))
            {
                buttonPressed = PlayerInformation.FaceButton.down;
            }
            else
            {
                return;
            }

            returnedItem = CheckAndUseItem(playerInformationComponent.GetItemFromInventory(buttonPressed));

            playerInformationComponent.GiveItem(returnedItem);
        }
    }

    /// <summary>
    /// Checks the item given to them and will return null if 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private InteractiveObject CheckAndUseItem(InteractiveObject item) {
        if (item == null)
        {
            if (ingredientQueue[0] != null)
            {
                return ingredientQueue[0];
            }
            else
            {
                return null;
            }
        }

        Ingredient ingredient = item as Ingredient;
        ingredientQueue.Add(ingredient);

        return RecipeCheck();
    }

    private InteractiveObject RecipeCheck() {
        if (ingredientQueue[0] != null && ingredientQueue[1] != null)
        {
            return Recipes.IngredientToPotion[new IngredientCombination(ingredientQueue[0], ingredientQueue[1])];
        }

        else
            return null;
    }
}

public static class Recipes
{
    public static Dictionary<IngredientCombination, Potion> IngredientToPotion
        = new Dictionary<IngredientCombination, Potion>()
        {
            // 2 x Same Colour = Bomb
            { new IngredientCombination(new RedIngredient(), new RedIngredient()), new Potion(Potion.PotionType.bomb) },
            { new IngredientCombination(new BlueIngredient(), new BlueIngredient()), new Potion(Potion.PotionType.bomb) },

            // Different colours = wall
            { new IngredientCombination(new RedIngredient(), new BlueIngredient()), new Potion(Potion.PotionType.wall) },
            { new IngredientCombination(new BlueIngredient(), new RedIngredient()), new Potion(Potion.PotionType.wall) }
        };
}

public class IngredientCombination
{
    public Ingredient.IngredientType mIngredient1;
    public Ingredient.IngredientType mIngredient2;

    public IngredientCombination (Ingredient ingredient1, Ingredient ingredient2)
    {
        mIngredient1 = ingredient1.mIngredient;
        mIngredient2 = ingredient2.mIngredient;
    }
}