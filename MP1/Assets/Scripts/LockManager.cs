using UnityEngine;

public class LockManager : MonoBehaviour
{
    public int locksCompleted = 0;

    public void CompleteLock()
    {
        locksCompleted++;

        Debug.Log("Lock completed: " + locksCompleted);

        if (locksCompleted >= 3)
        {
            Debug.Log("ALL LOCKS COMPLETED!");
        }
    }
}