using System;
using System.Collections;
using UnityEngine;

public class Platform_Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    
    [Space] [Header("---- WAYPOINTS ----")] 
    private int targetWaypoint;
    [SerializeField] private Vector2[] waypointsArray;

    private void Start()
    {
        targetWaypoint = 0;
        StartCoroutine("Movement");
    }

    IEnumerator Movement()
    {
        while (targetWaypoint < waypointsArray.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypointsArray[targetWaypoint], speed * Time.deltaTime);
            
            if (Vector2.Distance(transform.position, waypointsArray[targetWaypoint]) < 0.5f)
            {
                targetWaypoint = ++targetWaypoint % waypointsArray.Length;
            }
            
            yield return null;
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (!other.GetComponent<PlayerController>()) return;
    //     other.transform.SetParent(transform);
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     if (!other.GetComponent<PlayerController>()) return;
    //     other.transform.parent = null;
    // }
}
