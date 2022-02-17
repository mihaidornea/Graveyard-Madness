using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {
    public Transform target;

    private NavMeshAgent agent;
    private Rigidbody[] rbs;
    private Animator animator;
    private static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");

    private AudioSource _audioSource;
    private AudioClip hurtAudio;

    public float delayBetweenAttacks = 1f;
    private float currentDelta = 0f;

    private PlayerStats _playerStats;

    void Start() {
        _playerStats = PlayerStats.instance;
        rbs = GetComponentsInChildren<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<XROrigin>().transform;
        _audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        DisactivateRagdoll();
    }

    void Update() {
        agent.SetDestination(target.position);
        animator.SetFloat(MovementSpeed,
            Mathf.Max(Mathf.Abs(agent.velocity.normalized.x), Mathf.Abs(agent.velocity.normalized.z)));
        if (Vector3.Distance(target.position, transform.position) < 1.5f && currentDelta >= delayBetweenAttacks) {
            Attack();
        } else {
            currentDelta += Time.deltaTime;
        }
    }

    void ActivateRagdoll() {
        foreach (var item in rbs) {
            item.isKinematic = false;
        }
    }

    void DisactivateRagdoll() {
        foreach (var item in rbs) {
            item.isKinematic = true;
        }
    }

    public void Death() {
        ActivateRagdoll();
        Spawner.instance.UnitDead();
        _audioSource.PlayOneShot(hurtAudio);
        agent.enabled = false;
        GetComponent<Animator>().enabled = false;
        Destroy(gameObject, 7);
        Destroy(this);
    }

    public void PlayHurtSound() {
        _audioSource.PlayOneShot(hurtAudio);
    }

    private void Attack() {
        animator.SetTrigger("Attack");
        currentDelta = 0f;
        StartCoroutine(DealDamage());
    }

    IEnumerator DealDamage() {
        yield return new WaitForSeconds(1);
        _playerStats.TakeDamage();
    }
}