using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public GameObject particlePrefab;
    public GameObject soundPrefab;
    public Transform spawnPoint;

    public float shootSpeed = 5f;

    public void SpawnObject()
    {
        GameObject spawnedObject =
            Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);

        ShooterProjectile projectile =
            spawnedObject.GetComponent<ShooterProjectile>();

        projectile.velocity =
            spawnPoint.forward * shootSpeed;

        Instantiate(
            particlePrefab,
            spawnedObject.transform.position,
            Quaternion.identity
        );

        Instantiate(
            soundPrefab,
            spawnedObject.transform.position,
            Quaternion.identity
        );
    }
}