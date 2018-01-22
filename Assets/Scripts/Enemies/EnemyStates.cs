using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyStates : MonoBehaviour
{
    public Transform[] waypoints;
    public int patrolRange;
    public int attackRange;
    public int shootRange;
    public Transform vision;
    public float stayAlertTime;
    public float viewAngle;

    public GameObject missile;
    public float missileDamage;
    public float missileSpeed;

    public bool onlyMelee = false;
    public float meleeDamage;
    public float attackDelay;

    public LayerMask raycastMask;

    [HideInInspector]
    public AlertState alertState;
    [HideInInspector]
    public AttackState attackState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public PatrolState patrolState;
    [HideInInspector]
    public IEnemyAI currentState;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Transform chaseTarget;
    [HideInInspector]
    public Vector3 lastKnownPosition;
    void Awake()
    {
        // Tworzymy instancje każdego ze stanu
        // I przekazujemy do nich obiekt EnemyStates
        alertState = new AlertState(this);
        attackState = new AttackState(this);
        chaseState = new ChaseState(this);
        patrolState = new PatrolState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        // Przypisujemy startowy stan
        currentState = patrolState;
    }

    void Update()
    {
        // Co klatke gry wykonujemy akcje aktualnego stanu
        currentState.UpdateActions();
    }

    void OnTriggerEnter(Collider otherObj)
    {
        // Po wejściu w interakcje z innym obiektem
        // Wywolaj funkcje OnTriggerEnter zgodna z aktualnym stanem
        currentState.OnTriggerEnter(otherObj);
    }
    // Funkcja odpowiedzialna za wychwytywanie strzałów bohatera
    // Pozycje, z której strzelal bohater ustawia jako ostatnią znaną pozycje jego pobytu
    void HiddenShot(Vector3 shotPosition)
    {
        Debug.Log("Ktoś strzelił");
        lastKnownPosition = shotPosition;
        currentState = alertState;
    }

    public bool EnemySpotted()
    {
        Vector3 direction = GameObject.FindWithTag("Player").transform.position - transform.position;
        float angle = Vector3.Angle(direction, vision.forward);

        if(angle < viewAngle * 0.5f)
        {
            RaycastHit hit;
            if(Physics.Raycast(vision.transform.position, direction.normalized, out hit, patrolRange, raycastMask))
            {
                if(hit.collider.CompareTag("Player"))
                {
                    chaseTarget = hit.transform;
                    lastKnownPosition = hit.transform.position;
                    return true;
                }
            }
        }
        return false;
    }
}