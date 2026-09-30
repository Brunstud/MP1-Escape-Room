using UnityEngine;

public class CometOrbit : MonoBehaviour
{
    public Transform planet;
    public float gravity = 2.0f;

    private Vector3 velocity;

    void Start()
    {
        Vector3 offset = transform.position - planet.position;
        float distance = offset.magnitude;

        Vector3 tangent = new Vector3(-offset.z, 0f, offset.x).normalized;

        float orbitSpeed = Mathf.Sqrt(gravity / distance);

        velocity = tangent * orbitSpeed;
    }

    void Update()
    {
        Vector3 offset = transform.position - planet.position;

        float distance = offset.magnitude;

        if (distance == 0f)
            return;

        Vector3 acceleration =
            -gravity * offset / (distance * distance * distance);

        velocity += acceleration * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;
    }
}