using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    Vector3 startPos, endPos;
    public float height, time_to_end;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos;
        endPos.y += height;
        StartCoroutine(ElevatorRun());
    }

    IEnumerator ElevatorRun()
    {
        //Debug.Log("Elevator run starts");
        float t = 0;
        while(transform.position.y < endPos.y)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, t/ time_to_end);
            yield return null;
        }
        //Debug.Log("we get past the first while");
        yield return new WaitForSeconds(2.5f);
        t = 0;
        while (transform.position.y > startPos.y)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(endPos, startPos, t / time_to_end);
            yield return null;
        }
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(ElevatorRun());
    }

}
