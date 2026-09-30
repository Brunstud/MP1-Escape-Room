using UnityEngine;
using TMPro;

public class AIONScoreboard : MonoBehaviour
{
    public TextMeshProUGUI scoreboardText;
    public LockManager lockManager;

    void Update()
    {
        int locks = lockManager.locksCompleted;

        scoreboardText.text =
            "AION TIME TRAVEL SYSTEM\n\n" +
            "LOCKS: " + locks + " / 3\n\n" +
            GetStatus(locks);
    }

    string GetStatus(int locks)
    {
        if (locks == 3)
        {
            return "STATUS: UNLOCKED - READY FOR TIME TRAVEL";
        }

        return "STATUS: LOCKED";
    }
}