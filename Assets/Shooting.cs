using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public float bulletsPerSecond;
    public AudioClip[] pewpewSounds;
    public AudioSource source;
    public float shootingCooldown = 0, shootingDeviation = 0;
    public ParticleSystem muzzleFlash;
    public GameObject bulletHole;
    public Camera mainCam;
    public Inventory inv;
    public Text bulletText;
    

    public GameObject Ak47UI, SkorpionUI, Ak47Model, SkorpionModel;
    private bool AK47 = true, Skorpion = false;
    private bool canShootSkorpion = true, skorpionCD = false;
    private float scrollCD = 0;

    private void Update() {
        if(Input.GetKey(KeyCode.Mouse0)){
            if (AK47)
            {
                if (shootingCooldown <= 0)
                {
                    if (inv.bullets > 0)
                    {
                        Shoot();
                    }
                    shootingCooldown = 1 / bulletsPerSecond;
                }
                else
                {
                    shootingCooldown -= Time.deltaTime;
                }
            }
            if (Skorpion && canShootSkorpion && inv.bullets > 0)
            {
                Shoot();
                skorpionCD = true;
                canShootSkorpion = false;
            }
        }else{
            shootingCooldown = 0;
            if(shootingDeviation > 0){
                shootingDeviation -= Time.deltaTime/16; // it would take 2 seconds to recover from shooting.
                if(shootingDeviation < 0){shootingDeviation = 0;}
            }
        }

        if(Input.GetKeyUp(KeyCode.Mouse0) && skorpionCD == true){
            StartCoroutine(ShootAgain());
        }


        if (Input.GetKey(KeyCode.Mouse1))
        {
            mainCam.fieldOfView = 30;
        }
        else
        {
            mainCam.fieldOfView = 60;
        }

        if(Input.GetAxisRaw("Mouse ScrollWheel") != 0 && scrollCD <= 0 )
        {
            Debug.Log("scroll");
            ChangeWeapon();
            scrollCD = 1;
        }
        scrollCD -= Time.deltaTime;
    }

    IEnumerator ShootAgain()
    {
        skorpionCD = false;
        yield return new WaitForSeconds(.2f);
        canShootSkorpion = true;
    }

    void ChangeWeapon()
    {
        if (AK47)
        {
            AK47 = false;
            Ak47Model.SetActive(false);
            Ak47UI.transform.Find("Selected").gameObject.SetActive(false);

            Skorpion = true;
            SkorpionModel.SetActive(true);
            SkorpionUI.transform.Find("Selected").gameObject.SetActive(true);
        }
        else if (Skorpion)
        {
            AK47 = true;
            Ak47Model.SetActive(true);
            Ak47UI.transform.Find("Selected").gameObject.SetActive(true);

            Skorpion = false;
            SkorpionModel.SetActive(false);
            SkorpionUI.transform.Find("Selected").gameObject.SetActive(false);
        }

    }

    private void Shoot(){
        source.clip = pewpewSounds[Random.Range(0,pewpewSounds.Length)];
        source.Play();
        muzzleFlash.Play();
        inv.bullets -= 1;
        bulletText.text = "BULLETS: " + inv.bullets.ToString();

        RaycastHit hit;
        Camera camera = this.gameObject.GetComponentInChildren<Camera>();
        Vector3 origin = camera.transform.position;
        Vector3 dir = camera.transform.forward;

        dir.x += Random.Range(-shootingDeviation,shootingDeviation);
        dir.y += Random.Range(-shootingDeviation,shootingDeviation);
        dir.z += Random.Range(-shootingDeviation,shootingDeviation);

        Debug.DrawRay(origin, dir * 100, Color.red,5);
        Physics.Raycast(origin, dir, out hit, 100);

        try{
            if(hit.collider.gameObject.tag == "Enemy"){
                // Debug.Log("Robot is hit");
                if (Skorpion)
                {
                    hit.collider.gameObject.GetComponentInParent<enemyAI>().Hit(Random.Range(18, 25));
                }
                if (AK47)
                {
                    hit.collider.gameObject.GetComponentInParent<enemyAI>().Hit(Random.Range(12, 17));
                }
                
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
}
