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
}
