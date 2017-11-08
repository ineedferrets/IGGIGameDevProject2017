using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject {

    private string _mName;
    public string mName
    {
        get
        {
            return _mName;
        }
    }

    public Sprite uiSprite;

    public InteractiveObject(string name) { _mName = name; uiSprite = Resources.Load("Items/" + _mName) as Sprite; }

}
