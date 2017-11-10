using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General class for objects players can store in inventory.
/// </summary>
public abstract class InteractiveObject {

    // Object name is publicly accessible but privately assigned.
    private string _itemID;
    public string itemID {
        get {
            return _itemID;
        }
    }

    // Public sprite for UI.
    public Sprite uiSprite;
    public Color uiSpriteColor;

    /// <summary>
    /// Constructer.
    /// </summary>
    /// <param name="name"></param>
    public InteractiveObject(string id) {
        _itemID = id;
        SpriteRenderer renderer = Resources.Load<GameObject>("Items/ui" + _itemID).GetComponent<SpriteRenderer>();
        uiSprite = renderer.sprite;
        uiSpriteColor = renderer.color;
    }

    /// <summary>
    /// For players to spawn instances of objects in the scene.
    /// </summary>
    /// <param name="position"></param>
    /// <returns>Whether instantiation was successful.</returns>
    public virtual GameObject SpawnObject(Vector3 position) {
        return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + _itemID), position, Quaternion.identity, GameObject.Find("Map").transform);
    }
}
