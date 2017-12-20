using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlokAtackTarget {

    private GameObject _target_blok = null;
    public GameObject target_blok {
        get {
            return _target_blok;
        }
    }

    private Transform _nav_target = null;
    public Transform nav_target {
        get {
            if (_nav_target == null) {
                _nav_target = target_blok.GetComponent<BlockTargets>().GetRandomTarget();
            }

            if (_nav_target == null) {
                Debug.LogError("There is not target script on the block");
            }

            return _nav_target;
        }
    }

    public Vector3 hitpoint = Vector3.zero;

    public IDamageable<float> damageable 
    {
        get {
            var found = target_blok.GetComponents(typeof(IDamageable<float>));

            if (found.Length < 1) {
                return null;
            }

            return (IDamageable<float>)found[0];
        }
    }

    public BlokAtackTarget (GameObject tar, Vector3 hit) 
    {
        _target_blok = tar;
        hitpoint = hit;
    }

    public BlokAtackTarget() 
    {
        Erase();
    }

    public void Erase() 
    {
        _target_blok = null;
        _nav_target = null;
        hitpoint = Vector3.zero;
    }

    public void Assign (GameObject hitgo, Vector3? hit = null)
    {
        hitpoint = hit.GetValueOrDefault();
        _target_blok = hitgo;

        // Erase if invalid
        if (damageable == null || nav_target == null) {
            Erase();
        }
    }

    public bool IsValid() 
    {
        if (target_blok == null || damageable == null) {
            return false;
        }
        return true;
    }
}
