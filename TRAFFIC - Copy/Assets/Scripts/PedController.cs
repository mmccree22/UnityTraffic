using UnityEngine;
using UnityEngine.AI;

public class PedController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public GameObject PATH;
    private Transform[] PathPoints;

    public float minDistance = 5;

    public int index = 0;

    public CarScript carScript; // Reference to the CarScript

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        PathPoints = new Transform[PATH.transform.childCount];
        for (int i = 0; i < PathPoints.Length; i++)
        {
            PathPoints[i] = PATH.transform.GetChild(i);
        }
    }

    void Update()
    {
        CheckIfCanCross(); // Call the new method instead of Roam()
    }

    void CheckIfCanCross()
    {
        // Check if the car has reached the trigger point
        if (carScript.HasReachedTrigger)
        {
            // Perform the crossing action
            PerformCrossing();
        }
    }

    void PerformCrossing()
    {
        // Update the pedestrian's animation or perform any other actions for crossing the road
        animator.SetFloat("vertical", 1);
    }

    void Roam()
    {
        if (carScript.HasReachedTrigger)
        {
            // If the car has reached the trigger point, the pedestrian will roam
            if (Vector3.Distance(transform.position, PathPoints[index].position) < minDistance)
            {
                if (index + 1 != PathPoints.Length)
                {
                    index += 1;
                }
                else
                {
                    index = 0;
                }
            }

            agent.SetDestination(PathPoints[index].position);
            animator.SetFloat("vertical", 1); // Update the animation based on agent's movement
        }
        else
        {
            // If the car hasn't reached the trigger point, the pedestrian won't move
            agent.SetDestination(transform.position);
            animator.SetFloat("vertical", 0);
        }
    }
}
