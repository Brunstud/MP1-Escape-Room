using UnityEngine;

public class Orbiter : MonoBehaviour
{
    public Transform attractor;
    public float gravity = 2f;

    public Vector3 velocity;

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