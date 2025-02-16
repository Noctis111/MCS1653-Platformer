using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 1.8f;
    public float tolerance = 0.05f;

    private Vector3 currentTarget;
    private int nextTarget;

    // Improvements to consider:
    // - Player needs to stick with platform moving horizontally
    // - Ease in and out--slower closer to target
    // - Make handle an array of any number of waypoints, not just 2

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = waypoints[1].position;
        nextTarget = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        if (Vector3.Distance(currentTarget, transform.position) < tolerance)
        {
            SwitchTargets();
        }
    }

    private void SwitchTargets()
    {
        currentTarget = waypoints[nextTarget].position;

        if (nextTarget == 0)
        {            
            nextTarget = 1;
        }
        else
        {
            nextTarget = 0;
        }
    }
}
