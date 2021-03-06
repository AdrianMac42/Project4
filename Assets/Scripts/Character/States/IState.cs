﻿using UnityEngine;
using System.Collections;

public interface IState
{
    void UpdateState();

    void OnTriggerEnter(Collider other);

    void ToIdleState();

    void ToPatrolState();

    void ToAlertState();

    void ToChaseState();

    void ToHealState();

    void ToFleeState();

    void ToWorkingState();
}