using UnityEngine;
using System.Collections;

public class AlertState : IEnemyAI
{

    EnemyStates enemy;
    float timer = 0;

    public AlertState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {
        Search();
        Watch();
        // Rozglądaj się tylko wtedy, kiedy doszedłeś do ostatnio znanego miejsca
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
            LookAround();
    }
    // Funkcja odpowiedzialna za 'widzenie' przeciwnika
    // Gdy przeciwnik zauważy bohatera ustawia jego pozycje jako nowy cel
    // I przechodzi do stanu 'gonitwy'
    void Watch()
    {
        RaycastHit hit;
        if (Physics.Raycast(enemy.transform.position, enemy.vision.forward, out hit, enemy.patrolRange)
            && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            enemy.navMeshAgent.destination = hit.transform.position;
            ToChaseState();
        }
    }
    // Funkcja odpowiedzialna za 'rozgladanie sie' przeciwnika
    // Gdy przeciwnik dojdzie do ostatnio znanego miejsca pobytu przeciwnika
    // Spedza tam X czasu, a potem wraca do stanu patrolowania
    void LookAround()
    {
        timer += Time.deltaTime;
        if(timer >= enemy.stayAlertTime)
        {
            timer = 0;
            ToPatrolState();
        }
    }
    // Funkcja ustawia ostatnie znane miejsce bohatera jako cel
    void Search()
    {
        enemy.navMeshAgent.destination = enemy.lastKnownPosition;
        enemy.navMeshAgent.isStopped = false;
    }

    public void OnTriggerEnter(Collider enemy)
    {

    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    public void ToAttackState()
    {
        Debug.Log("Nie powinienem móc tego zrobić!");
    }

    public void ToAlertState()
    {
        Debug.Log("Już jestem w trybie zaalarmowania!");
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }
}