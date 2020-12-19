using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool is_unlocked = false;
    private bool is_open = false;
    private Transform player;

    public Transform rightDoor, leftDoor;
    Vector3 initialPosR, initialPosL, finalPosL, finalPosR;

    IEnumerator PlayerDetection()
    {
        float d = Vector3.Distance(player.position, gameObject.transform.position);
        if (is_unlocked)
        {
            Open = d < 7.5 ? true : false;
        }
        else
        {
            Inventory inv = player.GetComponent<Inventory>();
            if(inv.keys > 0)
            {
                inv.keys--;
                is_unlocked = true;
            }
        }

        yield return new WaitForSeconds(1);
        StartCoroutine(PlayerDetection());
        yield return 0;
    }

    private void Start()
    {
        initialPosR = rightDoor.position;
        initialPosL = leftDoor.position;
        finalPosL = initialPosL + (rightDoor.right * 2);
        finalPosR = initialPosR + (leftDoor.right * -2);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(PlayerDetection());
    }

    public bool Open
    {
        get { return is_open; }
        set
        {
            if(is_open != value)
            {
                is_open = value;
                StartCoroutine(ActivateDoors(.75f, is_open));
            }
        }
    }

    IEnumerator ActivateDoors(float time, bool open)
    {
        float t = Time.deltaTime;
        while (t < time)
        {
            t += Time.deltaTime;
            float rt = t / time;

            if (open)
            {
                leftDoor.position = Vector3.Lerp(initialPosL, finalPosL, rt);
                rightDoor.position = Vector3.Lerp(initialPosR, finalPosR, rt);
            }
            else
            {
                leftDoor.position = Vector3.Lerp(finalPosL, initialPosL, rt);
                rightDoor.position = Vector3.Lerp(finalPosR, initialPosR, rt);
            }

            yield return null;
        }
        yield return 0;
    }
}
