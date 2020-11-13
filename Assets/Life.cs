using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{
    public int life;
    public Text lifeText;

    public void Hit(int damage){
        life -= damage;
        lifeText.text = "HEALTH: " + life.ToString();
        if(life <= 0){
            Debug.Log("You are dead");
            SceneManager.LoadScene("Main",LoadSceneMode.Single);
        }
    }
}
