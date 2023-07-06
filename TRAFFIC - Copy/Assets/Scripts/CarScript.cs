using UnityEngine;
using UnityEngine.Events;

public class CarScript : MonoBehaviour
{
    public UnityEvent OnCarReachedTrigger;

    private bool hasReachedTrigger = false;

    public bool HasReachedTrigger
    {
        get { return hasReachedTrigger; }
        set { hasReachedTrigger = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerPoint"))
        {
            hasReachedTrigger = true;
            OnCarReachedTrigger.Invoke();
        }
    }
}
