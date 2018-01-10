using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
 Skrypt odpowiada za to, aby oczy przeciwnika cały czas podążąły za obiektem gracza
     */
public class Vision : MonoBehaviour
{
    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.LookAt(player);
    }
}