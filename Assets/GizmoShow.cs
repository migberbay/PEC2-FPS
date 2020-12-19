using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoShow : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < gameObject.transform.childCount; i++)
            Gizmos.DrawSphere(gameObject.transform.GetChild(i).position, 1); 
    }
}
