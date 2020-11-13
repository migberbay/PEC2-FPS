using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float TTL;

    private void FixedUpdate() {
        TTL -= Time.deltaTime;
        if(TTL <= 0){
            Destroy(this.gameObject);
        }
    }
}
