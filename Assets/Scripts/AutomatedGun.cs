using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedGun : Gun {

    public bool IsTrigger { get; set; } = false;
    public float delayBetweenShots = 2f;

    private float currentDelta = 0f;
    
    void Update()
    {
        if (IsTrigger && currentDelta >= delayBetweenShots) {
            Fire();
        } else if (currentDelta < delayBetweenShots) {
            currentDelta += Time.deltaTime;
        }
    }

    public override void Fire() {
        base.Fire();
        currentDelta = 0f;
    }
}
