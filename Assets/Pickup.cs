using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("To Restore")]
    public bool health;
    public bool bullets, shield;

    [Header("Ammounts")]
    public int hp;
    public int bullts, shld;

    public void Restore(GameObject player)
    {
        Life l = player.GetComponent<Life>();
        Inventory inv = player.GetComponent<Inventory>();
        Shooting sht = player.GetComponent<Shooting>();
        bool used = false; 

        if (health && l.life < 100)
        {
            l.life += hp;
            if(l.life > 100)
            {
                l.life = 100;     
            }
            l.lifeText.text = "HEALTH: " + l.life.ToString();
            used = true;
        }

        if (shield && l.shield < 50)
        {
            l.shield += shld;
            if (l.shield > 50)
            {
                l.shield = 50;
            }
            l.shieldText.text = "SHIELD: " + l.shield.ToString();
            used = true;
        }

        if (bullets)
        {
            inv.bullets += bullts;
            sht.bulletText.text = "BULLETS: " + inv.bullets.ToString();
            used = true;
        }

        if (used)
        {
            Destroy(this.gameObject);
        }
    }
}
