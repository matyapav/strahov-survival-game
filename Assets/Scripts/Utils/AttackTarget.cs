using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget
{

    private bool _is_blok = false;
    private GameObject _target_gameObject = null;
    private Transform _nav_target = null;
    public Vector3 hitpoint = Vector3.zero;

    public bool is_blok
    {
        get
        {
            return _is_blok;
        }
    }

    public GameObject target_gameObject
    {
        get
        {
            return _target_gameObject;
        }
    }

    public Transform nav_target
    {
        get
        {
            if (is_blok)
            {
                if (_nav_target == null)
                {
                    _nav_target = target_gameObject.GetComponent<BlockTargets>().GetRandomTarget();
                }

                if (_nav_target == null)
                {
                    Debug.LogError("There is not target script on the block");
                }

                return _nav_target;
            }
            else
            {
                return _target_gameObject.transform;
            }

        }
    }

    public IDamageable<float> damageable
    {
        get
        {
            var found = target_gameObject.GetComponents(typeof(IDamageable<float>));

            if (found.Length < 1)
            {
                return null;
            }
            else if (found.Length > 1)
            {
                Debug.LogError("There are multiple damageables on the GameObject " + target_gameObject.name);
            }

            return (IDamageable<float>)found[0];
        }
    }

    public AttackTarget()
    {
        Erase();
    }

    public void Erase()
    {
        _is_blok = false;
        _target_gameObject = null;
        _nav_target = null;
        hitpoint = Vector3.zero;
    }

    public void Assign(GameObject hitgo, Vector3? hit = null)
    {
        hitpoint = hit.GetValueOrDefault();
        _target_gameObject = hitgo;

        // Get if it is blok
        if (hitgo.tag == "Blok")
        {
            _is_blok = true;
        }

        // Erase if invalid
        if (damageable == null || nav_target == null)
        {
            Erase();
        }
    }
}
