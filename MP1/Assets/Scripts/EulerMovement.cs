using UnityEngine;

public class EulerMovement : MonoBehaviour
{
    public Vector3 velocity;

    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}