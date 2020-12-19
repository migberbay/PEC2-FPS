using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfDone : MonoBehaviour
{
    public GameObject CheckMark, Player;

    void Start()
    {
        if(CheckMark == null )
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(CheckIfAllDead());
        }
    }

    IEnumerator CheckIfAllDead()
    {
        if (transform.childCount == 0)
        {
            CheckMark.SetActive(true);
            Player.GetComponent<Inventory>().keys++;
            Destroy(gameObject);
            yield return 0;
        }
        else
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(CheckIfAllDead());
        }
    }
}
