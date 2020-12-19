using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{
    public float life, shield;
    public Text lifeText, shieldText;
    public GameObject deathPanel;
    public Camera deathCam;

    public void Hit(int damage){
        if (shield == 0)
        {
            life -= damage;
        }

        if (shield > 0)
        {
            shield -= damage * 0.8f;
            life -= damage * 0.2f;
        }

        if (shield < 0)
        {
            life += shield;
            shield = 0;
        }
        
        lifeText.text = "HEALTH: " + life.ToString();
        shieldText.text = "SHIELD: " + shield.ToString();

        if(life <= 0){
            Debug.Log("You are dead");
            gameObject.SetActive(false);
            deathCam.gameObject.SetActive(true);
            deathPanel.SetActive(true);
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pickup")
        {
            other.GetComponent<Pickup>().Restore(gameObject);
        }
    }
}
