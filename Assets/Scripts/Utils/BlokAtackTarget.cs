using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlokAtackTarget {

    public GameObject target = null;
    public IDamageable<float> damageable = null;
    public Vector3 hitpoint = Vector3.zero;

    public BlokAtackTarget (GameObject tar, IDamageable<float> dam, Vector3 hit) {
        target = tar;
        damageable = dam;
        hitpoint = hit;
    }

    public void Erase() {
        target = null;
        damageable = null;
        hitpoint = Vector3.zero;
    }

    public void Assign(GameObject hitgo, Vector3? hit = null)
    {
        hitpoint = hit.GetValueOrDefault();
        target = hitgo;

        var found = hitgo.GetComponents(typeof(IDamageable<float>));

        if (found.Length < 1) {
            Erase();
            return;
        }

        damageable = (IDamageable<float>)found[0];
    }

    public bool IsValid() {
        if (target == null || damageable == null) {
            return false;
        }
        return true;
    }
}
