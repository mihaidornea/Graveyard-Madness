using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour {

    public bool fatal = false;
    public GameObject limbPrefab;
    
    public void Hit() {
        var childLimb = transform.GetChild(0).GetComponentInChildren<Limb>();
        if (childLimb) {
            childLimb.Hit();
        }
        transform.localScale = Vector3.zero;
        var spawnedLimb = Instantiate(limbPrefab, transform.parent);
        spawnedLimb.transform.parent = null;

        if (fatal)
            GetComponentInParent<Zombie>().Death();
        
        Destroy(spawnedLimb, 10);
        Destroy(this);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Weapon")) {
            GetComponentInParent<Zombie>().PlayHurtSound();
            Hit();
        }
    }
}
