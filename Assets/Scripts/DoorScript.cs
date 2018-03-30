using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public bool slide, rotate;
    public float speed;
    public KeyCode openningKey;
    public Vector3 endPosition;
    Vector3 startPosition;
    GameObject doors;
    bool isOpen = false;
    Animator anim;

    private void Awake()
    {
        doors = transform.Find("Doors").gameObject;
        startPosition = doors.transform.position;
        anim = doors.GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(openningKey))
            {
                if(slide)
                {
                    StartCoroutine(SlideDoors());
                } else if (rotate)
                {
                    if(!isOpen)
                    {
                        isOpen = !isOpen;
                        anim.SetBool("isOpened", true);
                    } else
                    {
                        isOpen = !isOpen;
                        anim.SetBool("isOpened", false);
                    }
                }
            }
        }
    }

    IEnumerator SlideDoors()
    {
        Vector3 current = doors.transform.position;
        Vector3 destination = isOpen ? startPosition : endPosition;
        isOpen = !isOpen;
        float t = 0f;
        while ( t < 1 )
        {
            t += Time.deltaTime * speed;
            doors.transform.position = Vector3.Lerp(current, destination, t);
            yield return null;
        } 
    }

}
