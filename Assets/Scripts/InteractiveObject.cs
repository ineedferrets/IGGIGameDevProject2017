using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General class for objects players can store in inventory.
/// </summary>
public abstract class InteractiveObject {

    // Object name is publicly accessible but privately assigned.
    private string _name;
    public string name {
        get {
            return _name;
        }
    }

    // Public sprite for UI.
    public Sprite uiSprite;

    /// <summary>
    /// Constructer.
    /// </summary>
    /// <param name="name"></param>
    public InteractiveObject(string name) {
        _name = name;
        uiSprite = Resources.Load<Sprite>("Items/ui" + _name);
    }

    /// <summary>
    /// For players to spawn instances of objects in the scene.
    /// </summary>
    /// <param name="position"></param>
    /// <returns>Whether instantiation was successful.</returns>
    public bool SpawnObject(Vector3 position) {
        return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + name), position, Quaternion.identity, GameObject.Find("Map").transform) != null;
    }
}
