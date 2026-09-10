using UnityEngine;
using UnityEngine.InputSystem;

public class ChronalOrbSpawner : MonoBehaviour
{
    // Controller button
    public InputActionReference spawnAction;

    // Create Orb & Particle Effect
    public GameObject orbPrefab;
    public GameObject particlePrefab;

    // Where the orb should appear
    public Transform spawnPoint;

    // The object that pulls the orb
    public Transform attractor;

    // Strength of the attraction
    public float gravity = 8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnAction.action.Enable();
    }


    // Update is called once per frame
    void Update()
    {
        // Check spawn button
        if (spawnAction.action.WasPressedThisFrame())
        {
            SpawnOrb();
        }
    }


    void SpawnOrb()
    {
        // Create new orb
        GameObject newOrb = Instantiate(
            orbPrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        // Create particle effect
        GameObject newParticles = Instantiate(
            particlePrefab,
            spawnPoint.position,
            Quaternion.identity
        );
        Destroy(newParticles, 2f);

        // Call ChronalOrb script
        ChronalOrb orbScript =
            newOrb.GetComponent<ChronalOrb>();

        orbScript.Initialize(
            attractor,
            spawnPoint.forward,
            gravity
        );
    }
}