using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionInformation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}

public class Potion : InteractiveObject {
    public enum PotionType { bomb, wall };

    private PotionType _mPotionType;
    public PotionType mPotionType {
        get {
            return _mPotionType;
        }
    }

    public Potion(PotionType potionType) : base(potionType.ToString()+"potion") {
        _mPotionType = potionType;
    }
}
