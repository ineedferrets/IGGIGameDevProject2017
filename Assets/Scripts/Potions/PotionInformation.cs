using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Potion behaviour and information.
/// </summary>
public class PotionInformation : MonoBehaviour {

}

/// <summary>
/// Potion class inherits from interactive object. Contains all possible Potion types.
/// </summary>
public class Potion : InteractiveObject {
    /// <summary>
    /// Enum type of potion.
    /// </summary>
    public enum Type { bomb, wall };
	private Player currentPlayer;
    // Publicly accessible potion type. Privately assigned.
    private Type _enumType;
	
    public Type enumType {
        get {
            return _enumType;
        }
    }

    public Potion(Type potionType, Player player) : base(potionType.ToString()+"potion") {
        _enumType = potionType;
		currentPlayer = player
    }
	
	public void PlacePotion() { // when potion is placed
		// TODO: put potion in space that witch is currently standing. Or in square next to witch in direction witch is facing. Which is better?
		
		if (_enumType = Type.bomb) {
			Invoke("BombEffect", 3f); // Invoke does the named method after the number of seconds indicated by the float.
		}
		if (_enumType = Type.wall) {
			Invoke("WallEffect", 3f); // Invoke does the named method after the number of seconds indicated by the float.
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
