using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    enemyAI myEnemy;
    int currentPatrollingPoint = 0;
    float raycooldown = 0f;

    public IdleState(enemyAI enemy){
        myEnemy = enemy;
    }

    // Main functionality when in this state on the Update Function.
    public void UpdateState(){
        Vector3 currPoint = myEnemy.patrollingPath[currentPatrollingPoint].position;
        myEnemy.navMeshAgent.destination = currPoint;
        float distance = Vector3.Distance(currPoint, myEnemy.transform.position);

        if(distance < 3.5f){
            if(currentPatrollingPoint + 1 ==  myEnemy.patrollingPath.Length){
                currentPatrollingPoint = 0;
            }else{
                currentPatrollingPoint ++;
            }
        }
        raycooldown += Time.deltaTime;
    }

    // Funcionalidad que ocurrira en el momento que el jugador entre en el trigger.
    public void ToAlertState(){
        myEnemy.currentState = myEnemy.alertState;
    }
    
    // never swapping to this state.
    public void ToIdleState(){}

    public void OnTriggerEnter(Collider other) {}

    // Since the player will not be in the trigger this are never used.
    public void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player" && raycooldown > 0.1){
            raycooldown = 0;

            RaycastHit hit;
            Vector3 origin = myEnemy.transform.position;
            origin.y +=3;
            Vector3 dir = (other.gameObject.transform.position - origin).normalized;

            Physics.Raycast(origin, dir, out hit, 30);
            Debug.DrawRay(origin, dir * 30, Color.red,5);

            try{
                if(hit.collider.gameObject.tag == "Player"){
                    ToAlertState();
                }
            }catch(System.Exception){
                Debug.Log("No colliders here");
            }           
        }
    }
    public void OnTriggerExit(Collider other) {}
}
