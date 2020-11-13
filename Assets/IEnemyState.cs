using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState {
    void UpdateState();
    void ToAlertState();
    void ToIdleState();

    void OnTriggerEnter(Collider other);
    void OnTriggerStay(Collider other);
    void OnTriggerExit(Collider other);
}

