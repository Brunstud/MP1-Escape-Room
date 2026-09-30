using UnityEngine;

public class ShooterProjectile : MonoBehaviour
{
    public Vector3 velocity;

    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}