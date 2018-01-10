using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Sprite deadBody;
    public int maxHealth;
    float health;

    EnemyStates es;
    NavMeshAgent nma;
    SpriteRenderer sr;
    BoxCollider bc;

    private void Start()
    {
        health = maxHealth;
        es = GetComponent<EnemyStates>();
        nma = GetComponent<NavMeshAgent>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            es.enabled = false;
            nma.enabled = false;
            sr.sprite = deadBody;
            bc.center = new Vector3(0, -0.8f, 0);
            bc.size = new Vector3(1.05f, 0.43f, 0.2f);
        }
    }

    void AddDamage(float damage)
    {
        health -= damage;
    }
}