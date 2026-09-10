using UnityEngine;

public class PerfectOrbit : MonoBehaviour
{
    public Transform attractor;
    public float gravity = 2f;

    public Vector3 velocity;

    void Start()
    {
        Vector3 direction =
            attractor.position - transform.position;

        float distance =
            direction.magnitude;

        Vector3 radialDirection =
            direction.normalized;

        Vector3 tangent =
            Vector3.Cross(radialDirection, Vector3.up).normalized;

        float orbitalSpeed =
            Mathf.Sqrt(gravity / distance);

        velocity =
            tangent * orbitalSpeed;
    }

    void Update()
    {
        Vector3 direction =
            attractor.position - transform.position;

        float distance =
            direction.magnitude;

        Vector3 acceleration =
            direction.normalized * gravity / (distance * distance);

        velocity += acceleration * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;
    }
}