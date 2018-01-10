using UnityEngine;
using System.Collections;

/*
 Skrypt zmienia rotację obiektu, do którego jest przypisany tak
 aby był odwrócony zawsze w stronę głównej kamery (kamery gracza)
     */

public class FaceCamera : MonoBehaviour
{
    Vector3 cameraDirection;

    void Update()
    {
        cameraDirection = Camera.main.transform.forward;
        cameraDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(cameraDirection);
    }
}