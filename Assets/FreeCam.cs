using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCam : MonoBehaviour
{
    void Update(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal > 0){
            this.gameObject.transform.Translate(new Vector3(0.05f,0,0),Space.Self);
        }else if(horizontal < 0){
            this.gameObject.transform.Translate(new Vector3(-0.05f,0,0),Space.Self);
        }

        if(vertical > 0){
            this.gameObject.transform.Translate(new Vector3(0,0,0.05f),Space.Self);
        }else if(vertical < 0){
            this.gameObject.transform.Translate(new Vector3(0,0,-0.05f),Space.Self);
        }

        if(Input.GetKey(KeyCode.Q)){
            this.gameObject.transform.Rotate(new Vector3(0,-0.25f,0),Space.Self);
        }else if(Input.GetKey(KeyCode.E)){
            this.gameObject.transform.Rotate(new Vector3(0,0.25f,0),Space.Self);
        }

        // float new_val = this.gameObject.transform.rotation.x + Input.mouseScrollDelta.y * 0.1f;
        this.transform.Rotate(new Vector3(Input.mouseScrollDelta.y * 3,0,0));
    }
}
