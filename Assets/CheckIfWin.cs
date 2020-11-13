using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class CheckIfWin : MonoBehaviour
{
    public GameObject WinPanel, Player;


    void Start(){
        StartCoroutine("CheckIfWon");
    }

    IEnumerator CheckIfWon(){
        if(transform.childCount == 0){
            WinPanel.SetActive(true);
            FirstPersonController controller = Player.GetComponent<FirstPersonController>();
            controller.m_MouseLook.lockCursor = false;

            Player.GetComponent<Shooting>().enabled = false;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine("CheckIfWon");
    }

    public void Restart(){
        SceneManager.LoadScene("Main",LoadSceneMode.Single);
    }
}
