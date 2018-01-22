using UnityEngine;
using System.Collections;

public class ChaseState : IEnemyAI
{

    EnemyStates enemy;

    public ChaseState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {
        Watch();
        Chase();
    }
    // Funkcja odpowiedzialna za 'widzenie' przeciwnika
    // Gdy przeciwnik zauważy gracza ustawiamy go jako nasz cel 'gonitwy
    // Gdy przeciwnik zgubi gracza z oczu przechodzimy do stanu zaalarmowania
    void Watch()
    {
        if(!enemy.EnemySpotted())
        {
            ToAlertState();
        }
    }
    // Funkcja odpowiedzialna za gonienie przeciwnika
    // Jeśli przeciwnik jest wystarczająco blisko przechodzi do stanu atakowania
    void Chase()
    {
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.isStopped = false;
        if(enemy.navMeshAgent.remainingDistance <= enemy.attackRange && enemy.onlyMelee == true)
        {
            enemy.navMeshAgent.isStopped = true;
            ToAttackState();
        } else if (enemy.navMeshAgent.remainingDistance <= enemy.shootRange && enemy.onlyMelee == false)
        {
            enemy.navMeshAgent.isStopped = true;
            ToAttackState();
        }
    }

    public void OnTriggerEnter(Collider enemy)
    {

    }

    public void ToPatrolState()
    {
        Debug.Log("Nie powinienem móc tego zrobić!");
    }

    public void ToAttackState()
    {
        Debug.Log("Zaczynam atakować gracza!");
        enemy.currentState = enemy.attackState;
    }

    public void ToAlertState()
    {
        Debug.Log("Zgubiłem gracza z oczu!");
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        Debug.Log("Już go gonie!");
    }

}