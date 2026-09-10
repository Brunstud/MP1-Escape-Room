using UnityEngine;

public class DoubleIntegrator : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 acceleration;

    void Update()
    {
        velocity += acceleration * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;
    }
}