using UnityEngine;

public class OrbitingProjectile : MonoBehaviour
{
    public Vector3 velocity;

    private Transform attractor;
    private float gravity;
    private bool useGravity;

    public void Initialize(
        Vector3 initialVelocity,
        Transform attractorObject,
        float gravityStrength,
        bool gravityEnabled)
    {
        velocity = initialVelocity;
        attractor = attractorObject;
        gravity = gravityStrength;
        useGravity = gravityEnabled;
    }

    void Update()
    {
        if (useGravity && attractor != null)
        {
            Vector3 offset =
                transform.position - attractor.position;

            float distance = offset.magnitude;

            if (distance > 0.01f)
            {
                Vector3 acceleration =
                    -gravity * offset /
                    (distance * distance * distance);

                velocity += acceleration * Time.deltaTime;
            }
        }

        transform.position += velocity * Time.deltaTime;
    }
}