using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoSelected : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 2.5f);
    }
}
