﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class enemyAI : MonoBehaviour
{
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public float shootingCooldown = 0, timeWaitToShoot = 0;
    [HideInInspector] public GameObject player;

    public NavMeshAgent navMeshAgent;
    public Transform[] patrollingPath;
    public Animator animator;
    public float bulletsPerSecond, initialTimeWaitToShoot;
    public AudioClip[] shotSounds;
    public AudioSource audioSource;
    public ParticleSystem muzzleFlash;
    public GameObject healthPickup, bulletPickup, shieldPickup;

    public int life = 150;
    public TextMeshPro healthText;

    private void Start() {
        idleState = new IdleState(this);
        alertState = new AlertState(this);

        currentState = idleState;

        timeWaitToShoot = initialTimeWaitToShoot;
        shootingCooldown = 1/bulletsPerSecond;
        player = GameObject.FindGameObjectWithTag("Player");
    }


    // we need to tell the states when a native mono class occurs since they don't inherit from MonoBehaviour
    private void Update() {
        currentState.UpdateState();
    }

    private void OnTriggerEnter(Collider other) {
        currentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other) {
        currentState.OnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other) {
        currentState.OnTriggerExit(other);
    }

    IEnumerator HitPlayer(){
        Life l = player.GetComponent<Life>();
        if(l != null){
            l.Hit(5);
        }else{
            Debug.Log("I'm not finding the stupid component.");
        }
        yield return null;
    }

    public void Hit(int damage){
        life -= damage;
        if(life <= 0){
            SpawnPickups();
            Destroy(this.gameObject);
        }
        healthText.text = life.ToString();
    }

    void SpawnPickups()
    {
        int randomHP = Random.Range(0, 100);
        int randomBullts = Random.Range(0, 100);
        int randomShield = Random.Range(0, 100);
        Vector3 pos = transform.position;
        pos.y += 2;
        if (randomHP > 75)
        {
            Instantiate(healthPickup, pos, transform.rotation);
        }
        if (randomBullts > 55)
        {
            Instantiate(bulletPickup, pos, transform.rotation);
        }
        if (randomShield > 75)
        {
            Instantiate(shieldPickup, pos, transform.rotation);
        }
    }
}
