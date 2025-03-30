using UnityEngine;

public class CarSpawnLogic : MonoBehaviour
{
    private float spawnDelay;

    [SerializeField] private GameObject carPrefab;

    private float nextTimeToSpawn;

    private void OnEnable()
    {
        spawnDelay = Random.Range(2.5f, 3f);
    }

    void Update()
    {
        if (nextTimeToSpawn <= Time.time)
        {
            SpawnCar();
            nextTimeToSpawn = Time.time + spawnDelay;
        }
    }

    void SpawnCar()
    {
        GameObject Car = Instantiate(carPrefab, transform.position, transform.rotation);
        Car.transform.SetParent(this.gameObject.transform);
    }

}
