using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableThing : MonoBehaviour, IDamageable<float>{

    public float hitpoints = 200;

    public void Damage (float dmg) {
        hitpoints -= dmg;

        if(Dead()) {
            Destroy();
        }
    }

    public bool Dead () {
        return hitpoints <= 0;
    }


    private void Destroy() {
        Destroy(gameObject);
    }
}
