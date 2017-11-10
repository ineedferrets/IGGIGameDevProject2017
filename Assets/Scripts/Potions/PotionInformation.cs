using System;
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
    
    public enum Type { bomb, wall };

    private Type _potType;
    public Type potType { get { return _potType; } }

    private List<string> _craftingIDs;
    public List<string> craftingIDs { get { return _craftingIDs; } }

    public Potion(Type potionType) : base(potionType.ToString()+"potion") {
        switch (potionType)
        {
            case Type.bomb:
                _craftingIDs = new List<string>() { "redingredient redingredient", "blueingredient blueingredient" };
                break;
            case Type.wall:
                _craftingIDs = new List<string>() { "redingredient blueingredient", "blueingredient redingredient" };
                break;
            default:
                break;
        }
    }

    public override GameObject SpawnObject(Vector3 position)
    {
        position += new Vector3(0, 0, 3);
        return base.SpawnObject(position);
    }
}
