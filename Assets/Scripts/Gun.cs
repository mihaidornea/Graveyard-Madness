using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float speed = 30;
    public GameObject bullet;
    public Transform muzzle;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public bool IsTeleportingActive { get; set; } = false;
    
    public virtual void Fire() {
        if (IsTeleportingActive) 
            return;
        var spawnedBullet = Instantiate(bullet, muzzle.position, muzzle.rotation);
        spawnedBullet.transform.Rotate(Vector3.left, -130f);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * muzzle.forward;
        audioSource.PlayOneShot(audioClip);
        Destroy(spawnedBullet, 2);
    }
}
