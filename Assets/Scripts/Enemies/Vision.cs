using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
 Skrypt odpowiada za to, aby oczy przeciwnika cały czas podążąły za obiektem gracza
     */
public class Vision : MonoBehaviour
{

    Vector3 destination;

    void Update()
    {
        destination = transform.parent.GetComponent<EnemyStates>().navMeshAgent.destination;
        transform.LookAt(destination);
    }
}