using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathOptions : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(DeathOptionss());
    }

    IEnumerator DeathOptionss()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene("Main", LoadSceneMode.Single);
            }
            yield return null;
        }
    }
}
