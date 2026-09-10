using UnityEngine;

public class ChronalOrb : MonoBehaviour
{
    public Transform attractor;
    public Vector3 velocity;
    public float gravity = 8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (attractor == null) return;

        Vector3 offset = transform.position - attractor.position;
        float distance = offset.magnitude;
        if (distance < 0.1f) return;

        // Calculate the acceleration toward the attractor
        Vector3 acceleration = -gravity * offset / Mathf.Pow(distance, 3f);

        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }


    // called when new orb created
    public void Initialize(Transform newAttractor, Vector3 controllerForward, float newGravity)
    {
        attractor = newAttractor;
        gravity = newGravity;

        Vector3 offset = transform.position - attractor.position;

        float distance = offset.magnitude;
        if (distance < 0.1f) distance = 0.1f;

        // Get the direction pointing away from the attractor
        Vector3 radialDirection = offset.normalized;

        // Get a sideways direction based on where
        // the controller is pointing
        Vector3 tangentDirection = controllerForward - Vector3.Dot(controllerForward, radialDirection) * radialDirection;

        // If the direction is too small,
        // make another sideways direction
        if (tangentDirection.sqrMagnitude < 0.001f)
        {
            tangentDirection = Vector3.Cross(radialDirection, Vector3.up);

            // Try another direction if it is still too small
            if (tangentDirection.sqrMagnitude < 0.001f)
            {
                tangentDirection = Vector3.Cross(radialDirection, Vector3.right);
            }
        }

        // normalize
        tangentDirection.Normalize();

        // Calculate the orbit speed
        float orbitalSpeed = Mathf.Sqrt(gravity / distance);
        velocity = tangentDirection * orbitalSpeed;
    }
}