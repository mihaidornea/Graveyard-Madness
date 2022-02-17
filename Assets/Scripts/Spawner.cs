using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {

    #region Singleton

    public static Spawner instance;

    private void Awake() {
        if (instance != null) {
            return;
        }

        instance = this;
    }

    #endregion
    
    public float spawnTime = 1;
    public int maxInstances = 30;
    public GameObject spawnedObject;
    public Transform[] spawnPoints;
    public bool canSpawn = true;
    
    private float timer = 0f;
    private int instances = 0;
    void Update()
    {
        if (timer > spawnTime  && instances < maxInstances && canSpawn) {
            var  randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(spawnedObject, randomPoint.position, randomPoint.rotation);
            instances++;
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    public void UnitDead() {
        instances--;
    }
}
