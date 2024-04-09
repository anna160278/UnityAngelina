using UnityEngine;
using System.Collections.Generic;

public class BusMovement : MonoBehaviour
{
    private List<Transform> waypoints = new();
    private float moveSpeed = 10f;
    private float turnSpeed = 5f;
    private int waypointIndex = 0;

    private void Start()
    {
        GameObject points = GameObject.FindGameObjectWithTag("Waypoints");

        foreach (Transform point in points.transform)
        {
            waypoints.Add(point);
        }
    }

    private void Update()
    {
        MoveToWaypoint();
        TurnTowardsWaypoint();
    }

    private void MoveToWaypoint()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            Transform targetWaypoint = waypoints[waypointIndex];
            Vector3 targetPosition = targetWaypoint.position;
            Vector3 movementThisFrame = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.position = movementThisFrame;

            if (transform.position == targetPosition)
            {
                waypointIndex++;
                if (waypointIndex == waypoints.Count) // ≈сли был последний, то обнул€ем индекс
                {
                    waypointIndex = 0;
                }
            }
        }
    }

    private void TurnTowardsWaypoint()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            Vector3 directionToTarget = waypoints[waypointIndex].position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
}
