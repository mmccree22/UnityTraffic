using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CarAI : MonoBehaviour
{
    public float safeDistance = 2f;
    public float carSpeed = 5f;
    public string[] tags;

    public GameObject currentTrafficRoute;
    public GameObject nextWaypoint;
    public int currentWaypointNumber;

    private NavMeshAgent _carNavmesh;

    private void Start()
    {
        _carNavmesh = this.gameObject.GetComponent<NavMeshAgent>();
        _carNavmesh.speed = carSpeed;
    }

    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, safeDistance);

        if (hit.transform)
        {
            for(int i = 0; i < tags.Length; i++)
            {
                if(hit.transform.tag == tags[i])
                {
                    Stop();
                }
            }
        }
        else
        {
            Move();
        }
    }


    void Stop()
    {
        _carNavmesh.speed = 0;
    }

    void Move()
    {
        if(nextWaypoint == null)
        {
            _carNavmesh.speed = 0;
        }
        else
        {
            _carNavmesh.speed = carSpeed;
        }

        if(currentWaypointNumber > 0)
        {
            if (_carNavmesh.speed == 0)
                _carNavmesh.speed = carSpeed;

            _carNavmesh.SetDestination(currentTrafficRoute.transform.GetChild(currentWaypointNumber - 1).transform.position);

        }
        else
        {
            if (_carNavmesh.speed == 0)
                _carNavmesh.speed = carSpeed; 

            if(nextWaypoint != null)
            _carNavmesh.SetDestination(nextWaypoint.transform.position);
        }

        if(currentWaypointNumber > 0)
        {
            float distance = Vector3.Distance(transform.position, currentTrafficRoute.transform.GetChild(currentWaypointNumber - 1).transform.position);
            if (distance <= 1)
                currentWaypointNumber -= 1;
        }
        else
        {
            float distance = Vector3.Distance(transform.position, nextWaypoint.transform.position);
            if(distance <= 1)
            {
                currentWaypointNumber = 4;
                currentTrafficRoute = nextWaypoint.transform.parent.gameObject;
            }
        }
    }

}
