using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class LockManager : MonoBehaviour
{
    public int locksCompleted = 0;
    public GameObject winMessage;

    public string nextSceneName = "02_PrincipalOffice";
    public float transitionDelay = 2f;
    public XRBaseInteractor leftInteractor;
    public XRBaseInteractor rightInteractor;

    private bool transitioning = false;

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

        if (locksCompleted >= 3 && !transitioning)
        {
            transitioning = true;
            Debug.Log("ALL LOCKS COMPLETED!");
            if (winMessage != null)
            {
                winMessage.SetActive(true);
            }

            StartCoroutine(GoToNextRoom());
        }
    }

    private IEnumerator GoToNextRoom()
    {
        yield return new WaitForSeconds(transitionDelay);
        CrossSceneCarryManager.Instance.CaptureHeldObjects(leftInteractor, rightInteractor);
        SceneManager.LoadScene(nextSceneName);
    }
}
