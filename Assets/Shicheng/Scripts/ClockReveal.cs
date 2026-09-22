using UnityEngine;
using System.Collections;

public class ClockReveal : MonoBehaviour
{
    public GameObject clockObject;
    public GameObject cabinetObject;

    public float clockRotateAngle = 45f;
    public float clockRotateTime = 0.5f;

    public Vector3 cabinetMoveOffset = new Vector3(0f, 0f, -1.8f);
    public float cabinetMoveTime = 1.5f;

    private bool opened = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InteractClock()
    {
        if (opened)
            return;

        opened = true;

        StartCoroutine(RotateClock());
    }

    private IEnumerator RotateClock()
    {
        Quaternion startRotation = clockObject.transform.localRotation;
        Quaternion endRotation =
            startRotation * Quaternion.Euler(0f, clockRotateAngle, 0f);

        float time = 0f;

        while (time < clockRotateTime)
        {
            time += Time.deltaTime;

            float t = time / clockRotateTime;
            t = t * t * (3f - 2f * t);

            clockObject.transform.localRotation =
                Quaternion.Slerp(startRotation, endRotation, t);

            yield return null;
        }

        clockObject.transform.localRotation = endRotation;

        StartCoroutine(MoveCabinet());
    }

    private IEnumerator MoveCabinet()
    {
        Vector3 startPosition = cabinetObject.transform.localPosition;
        Vector3 endPosition = startPosition + cabinetMoveOffset;

        float time = 0f;

        while (time < cabinetMoveTime)
        {
            time += Time.deltaTime;

            float t = time / cabinetMoveTime;
            t = t * t * (3f - 2f * t);

            cabinetObject.transform.localPosition =
                Vector3.Lerp(startPosition, endPosition, t);

            yield return null;
        }

        cabinetObject.transform.localPosition = endPosition;
    }
}