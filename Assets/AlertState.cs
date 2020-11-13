using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState
{
    // a reference to the gameobject running an instance of this state this is necessary since
    // we cannot call this.gameObject from this script because is not attached to a gameObject
    enemyAI myEnemy;

    public AlertState(enemyAI enemy){
        myEnemy = enemy;
    }

    // Functionality on alert state every frame.
    public void UpdateState(){
        
    }

    // we are on this state so this is never called.
    public void ToAlertState(){}

    public void ToIdleState(){
        myEnemy.currentState = myEnemy.idleState;
        myEnemy.timeWaitToShoot = myEnemy.initialTimeWaitToShoot;
        myEnemy.shootingCooldown = 1/myEnemy.bulletsPerSecond;
    }

    // Player is already inside the trigger.
    public void OnTriggerEnter(Collider other) {}

    public void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player"){
            myEnemy.transform.LookAt(other.transform.position);
            float playerdist = Vector3.Distance(myEnemy.transform.position, other.transform.position);
            
            if(playerdist > 12){
                myEnemy.navMeshAgent.destination = other.transform.position;
                myEnemy.animator.enabled = true;
            }else{
                myEnemy.animator.enabled = false;
                myEnemy.navMeshAgent.destination = myEnemy.transform.position;
            }
            
    
            if(myEnemy.timeWaitToShoot < 0){
                myEnemy.shootingCooldown -= Time.deltaTime;
                if(myEnemy.shootingCooldown < 0){
                    Shoot(other.gameObject);
                    myEnemy.shootingCooldown = 1/myEnemy.bulletsPerSecond;
                }   
            }else{
                myEnemy.timeWaitToShoot -= Time.deltaTime;
            }
        }
    }

    public void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            ToIdleState();
        }
    }

    private void Shoot(GameObject who){
        myEnemy.audioSource.clip = myEnemy.shotSounds[Random.Range(0, myEnemy.shotSounds.Length)];
        myEnemy.audioSource.Play();
        myEnemy.muzzleFlash.Play();

        RaycastHit hit;
        Vector3 origin = myEnemy.transform.position;
        origin.y += 2;
        Vector3 dir = (who.transform.position - origin).normalized;
        dir.x += Random.Range(-0.15f,0.15f);
        dir.y += Random.Range(-0.15f,0.15f);
        dir.y += Random.Range(-0.15f,0.15f);
        Debug.DrawRay(origin, dir * 100, Color.red,5);

        Physics.Raycast(origin, dir, out hit, 100);
        try{
            if(hit.collider.gameObject.tag == "Player"){
                Debug.Log("Player is hit");
                myEnemy.StartCoroutine("HitPlayer");
            }
        }catch(System.Exception){
            Debug.Log("No colliders here");
        } 
    }
}
