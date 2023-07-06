using UnityEngine;

public class PedController1 : MonoBehaviour
{
    public Animator animator;
    public GameObject PATH;
    private Transform[] PathPoints;
    public float minDistance = 5;
    public int index = 0;
    public CarScript carScript; // Reference to the CarScript

    private bool canCross = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        PathPoints = new Transform[PATH.transform.childCount];
        for (int i = 0; i < PathPoints.Length; i++)
        {
            PathPoints[i] = PATH.transform.GetChild(i);
        }
    }

    void Update()
    {
        if (carScript.HasReachedTrigger && !canCross)
        {
            canCross = true;
            PerformCrossing();
        }
    }

    void PerformCrossing()
    {
        animator.SetFloat("vertical", 1);

        // Wait until the car has reached the trigger point
        StartCoroutine(WaitForCarToReachTrigger());
    }

    System.Collections.IEnumerator WaitForCarToReachTrigger()
    {
        // Wait until the car has reached the trigger point
        while (!carScript.HasReachedTrigger)
        {
            yield return null;
        }

        // Move the pedestrian to the next path point
        StartCoroutine(MoveToNextPoint());
    }

    System.Collections.IEnumerator MoveToNextPoint()
    {
        Vector3 targetPosition = PathPoints[index].position;

        while (Vector3.Distance(transform.position, targetPosition) > minDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 1f * Time.deltaTime);
            yield return null;
        }

        // Increment index and wrap around if necessary
        index = (index + 1) % PathPoints.Length;

        // Continue moving to the next point if there are more points available
        if (index != 0)
        {
            targetPosition = PathPoints[index].position;
            StartCoroutine(MoveToNextPoint());
        }
        else
        {
            // Pedestrian has crossed the road, set animation to idle
            animator.SetFloat("vertical", 0);
        }
    }
}
