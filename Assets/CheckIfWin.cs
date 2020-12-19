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
            Player.SetActive(false);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine("CheckIfWon");
    }
}
