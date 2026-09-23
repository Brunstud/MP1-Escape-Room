using UnityEngine;

public class LockManager : MonoBehaviour
{
    public int locksCompleted = 0;
    public GameObject winMessage;

    public void CompleteLock()
    {
        locksCompleted++;

        Debug.Log("Lock completed: " + locksCompleted);

        if (locksCompleted == 1)
        {
            Debug.Log("LOCK 1 CALIBRATED");
        }

        if (locksCompleted == 2)
        {
            Debug.Log("LOCK 2 CALIBRATED");
        }

        if (locksCompleted == 3)
        {
            Debug.Log("ALL LOCKS COMPLETED!");
            if (winMessage != null)
            {
                winMessage.SetActive(true);
            }
        }
    }
}