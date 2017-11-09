using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructable {
    /// <summary>
    /// Applys damage to the object and returns whether the object has been destroyed.
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    bool ApplyDamage(float damage);
}
