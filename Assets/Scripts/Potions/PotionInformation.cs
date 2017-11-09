using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Potion behaviour and information.
/// </summary>
public class PotionInformation : MonoBehaviour {

    public Potion potion;
    public float timeUntilEffect;

    private void Start() {

        Invoke("PlacePotion", timeUntilEffect);
    }

    private void PlacePotion() {
        potion.PlacePotion();
    }
}

/// <summary>
/// Potion class inherits from interactive object. Contains all possible Potion types.
/// </summary>
public class Potion : InteractiveObject {
    /// <summary>
    /// Enum type of potion.
    /// </summary>
    public enum Type { bomb, wall };
    // Publicly accessible potion type. Privately assigned.
    private Type _enumType;
	
    public Type enumType {
        get {
            return _enumType;
        }
    }

    public Potion(Type potionType) : base(potionType.ToString()+"potion") {
        _enumType = potionType;
    }
	
	public void PlacePotion() { // when potion is placed
		// TODO: put potion in space that witch is currently standing.
		
		if (_enumType == Type.bomb) {
            BombEffect();
		}
		if (_enumType == Type.wall) {
            WallEffect();
		}
	}
	
	public void BombEffect() {
		// TODO:
		// If any adjacent tiles to the bomb are obstacles, make them flat tiles
		// If any witch within 3 (?) spaces of the potion, make them die
	}
	
	public void WallEffect() {
		// TODO: Change tile that potion is on from a movement tile to an Obstacle
	}
	
}
