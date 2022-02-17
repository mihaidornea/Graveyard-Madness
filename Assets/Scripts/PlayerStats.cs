using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
    
    #region Singleton

    public static PlayerStats instance;

    private void Awake() {
        if (instance != null)
            return;
        instance = this;
    }

    #endregion

    public int maxHealth = 100;
    public int onAttackDecrement = 20;
    public AudioClip onHurt;
    public float healAfterSeconds = 3f;
    public Image healthBar; 
    
    private AudioSource _audioSource;
    private int currentHealth;
    private float currentDelta = 0f;
    private float betweenHealDelay = 0.5f;
    private float currentHealDelta = 0f;
    

    void Start() {
        _audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    void Update() {
        if (currentDelta >= healAfterSeconds) {
            if (currentHealDelta >= betweenHealDelay) 
                Heal();
            else
                currentHealDelta += Time.deltaTime;
        } else {
            currentDelta += Time.deltaTime;
        }

        healthBar.fillAmount = (float)currentHealth / maxHealth;
        if (currentHealth <= 0) {
            Die();
        }
    }

    public void TakeDamage() {
        if (currentHealth > 0) {
            currentHealth -= onAttackDecrement;
            _audioSource.PlayOneShot(onHurt);
            currentDelta = 0f;
        }
    }

    private void Heal() {
        if (currentHealth < maxHealth) {
            currentHealth += onAttackDecrement;
            currentHealDelta = 0f;
        }
    }

    private void Die() {
        SceneManager.LoadScene(0);
    }
}