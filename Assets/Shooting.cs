using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float bulletsPerSecond;
    public AudioClip[] pewpewSounds;
    public AudioSource source;
    public float shootingCooldown = 0, shootingDeviation = 0;
    public ParticleSystem muzzleFlash;
    public GameObject bulletHole;

    private void Update() {
        if(Input.GetKey(KeyCode.Mouse0)){
            if(shootingCooldown <= 0){
                Shoot();
                shootingCooldown = 1/bulletsPerSecond;
            }else{
                shootingCooldown -= Time.deltaTime;
            }
        }else{
            shootingCooldown = 0;
            if(shootingDeviation > 0){
                shootingDeviation -= Time.deltaTime/16; // it would take 2 seconds to recover from shooting.
                if(shootingDeviation < 0){shootingDeviation = 0;}
            }
        }
    }

    private void Shoot(){
        source.clip = pewpewSounds[Random.Range(0,pewpewSounds.Length)];
        source.Play();
        muzzleFlash.Play();

        RaycastHit hit;
        Camera camera = this.gameObject.GetComponentInChildren<Camera>();
        Vector3 origin = camera.transform.position;
        Vector3 dir = camera.transform.forward;

        dir.x += Random.Range(-shootingDeviation,shootingDeviation);
        dir.y += Random.Range(-shootingDeviation,shootingDeviation);
        dir.y += Random.Range(-shootingDeviation,shootingDeviation);

        Debug.DrawRay(origin, dir * 100, Color.red,5);
        Physics.Raycast(origin, dir, out hit, 100);

        try{
            if(hit.collider.gameObject.tag == "Enemy"){
                // Debug.Log("Robot is hit");
                hit.collider.gameObject.GetComponentInParent<enemyAI>().Hit(Random.Range(12,17));
                // StartCoroutine("HitRobot",hit.collider.gameObject);
            }else{
                Instantiate(bulletHole,hit.point + hit.normal*0.01f, Quaternion.FromToRotation(Vector3.forward,-hit.normal));
            }
        }catch(System.Exception){
            Debug.Log("No colliders here");
        }
        if(shootingDeviation < 0.125f){
            shootingDeviation += 0.0125f;
        }
    }

    IEnumerator HitRobot(GameObject robot){
        robot.GetComponentInParent<enemyAI>().Hit(Random.Range(12,17));
        yield return null;
    }
}
