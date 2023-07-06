using UnityEngine;

public class PedestrianScript : MonoBehaviour
{
    private bool hasCrossed = false;

    private void Start()
    {
        // Subscribe to the car's event
        CarScript carScript = FindObjectOfType<CarScript>();
        carScript.OnCarReachedTrigger.AddListener(StartCrossing);
    }

    private void StartCrossing()
    {
        if (!hasCrossed)
        {
            // Perform crossing action
            Debug.Log("Pedestrian is crossing the road.");

            // Set the hasCrossed flag to prevent repeated crossing
            hasCrossed = true;
        }
    }
}
