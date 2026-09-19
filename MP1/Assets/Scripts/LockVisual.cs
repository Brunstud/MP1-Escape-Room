using UnityEngine;
using System.Collections;

public class LockVisual : MonoBehaviour
{
    public GameObject visualObject;

    public void ActivateVisual()
    {
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        visualObject.SetActive(true);

        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        visualObject.transform.localScale = startScale;

        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime;

            float easedTime = time * time * (3f - 2f * time);

            visualObject.transform.localScale =
                Vector3.Lerp(startScale, endScale, easedTime);

            yield return null;
        }
    }
}