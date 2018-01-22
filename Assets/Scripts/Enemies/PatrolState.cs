using UnityEngine;
using System.Collections;

public class PatrolState : IEnemyAI
{

    EnemyStates enemy;
    int nextWayPoint = 0;

    public PatrolState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {
        Watch();
        Patrol();
    }
    // Funkcja odpowiadająca za 'widzenie' przeciwnika
    // Gdy przeciwnik zauwazy bohatera przechodzi do stanu gonitwy
    void Watch()
    {
        if(enemy.EnemySpotted())
        {
            Debug.Log("Zauwazylem wroga!");
            ToChaseState();
        }
    }
    // Funkcja odpowiadająca za patrolowanie wzdłuż wyznaczonych punktów
    void Patrol()
    {
        enemy.navMeshAgent.destination = enemy.waypoints[nextWayPoint].position;
        enemy.navMeshAgent.isStopped = false;
        if(enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance
            && !enemy.navMeshAgent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % enemy.waypoints.Length;
        }
    }

    public void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.CompareTag("Player"))
        {
            ToAlertState();
        }
    }

    public void ToPatrolState()
    {
        Debug.Log("I'm already patrolling!");
    }

    public void ToAttackState()
    {
        enemy.currentState = enemy.attackState;
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

}