using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectSpawnerAdh : MonoBehaviour
{
    public InputActionReference action;

    public GameObject objectPrefab;
    public Transform spawnPoint;

    [Header("Visual Feedback (Particle Burst)")]
    [Tooltip("Optional: External particle effect prefab to instantiate at the spawn location.")]
    public GameObject particleEffectPrefab;

    public Transform attractor;

    public float shootSpeed = 3f;
    public float gravity = 2f;

    public bool perfectOrbit = true;

    void Start()
    {
        if (action != null && action.action != null)
        {
            action.action.Enable();
            action.action.performed += SpawnFromController;
        }
    }

    void OnDestroy()
    {
        if (action != null && action.action != null)
        {
            action.action.performed -= SpawnFromController;
        }
    }

    void Update()
    {
        // Laptop testing
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
        {
            SpawnObject();
        }
    }

    void SpawnFromController(InputAction.CallbackContext ctx)
    {
        SpawnObject();
    }

    void SpawnObject()
    {
        if (objectPrefab == null || spawnPoint == null)
            return;

        // 1. Instantiate the spawned object
        GameObject spawnedObject = Instantiate(
            objectPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        // 2. Co-locate Particle Burst Feedback
        // Option A: Instantiate external particle prefab at the spawn point
        if (particleEffectPrefab != null)
        {
            Instantiate(particleEffectPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        // Option B: Trigger child Particle System components attached to the spawned object
        ParticleSystem[] particles = spawnedObject.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }

        // 3. Configure Orbit / Physics Velocity
        Vector3 initialVelocity;

        if (perfectOrbit && attractor != null)
        {
            initialVelocity = CalculateOrbitVelocity(spawnedObject.transform.position);
        }
        else
        {
            // Object Shooter: velocity points where controller points
            initialVelocity = spawnPoint.forward * shootSpeed;
        }

        OrbitingProjectile projectile = spawnedObject.GetComponent<OrbitingProjectile>();
        if (projectile != null)
        {
            projectile.Initialize(
                initialVelocity,
                attractor,
                gravity,
                perfectOrbit
            );
        }

        // 4. Co-locate Spatial Sound
        AudioSource audioSource = spawnedObject.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    Vector3 CalculateOrbitVelocity(Vector3 spawnPosition)
    {
        Vector3 offset = spawnPosition - attractor.position;
        float distance = offset.magnitude;

        Vector3 radialDirection = offset.normalized;
        Vector3 controllerDirection = spawnPoint.forward.normalized;

        Vector3 tangentDirection = controllerDirection - Vector3.Dot(controllerDirection, radialDirection) * radialDirection;

        if (tangentDirection.sqrMagnitude < 0.001f)
        {
            tangentDirection = Vector3.Cross(radialDirection, Vector3.up);

            if (tangentDirection.sqrMagnitude < 0.001f)
            {
                tangentDirection = Vector3.Cross(radialDirection, Vector3.right);
            }
        }

        tangentDirection.Normalize();
        float orbitSpeed = Mathf.Sqrt(gravity / distance);

        return tangentDirection * orbitSpeed;
    }
}