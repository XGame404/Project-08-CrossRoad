using UnityEngine;

public class LandLogic : MonoBehaviour
{
    [SerializeField] private GameObject RoadPlatform;
    [SerializeField] private GameObject PlantPlatform;
    [SerializeField] private Transform lastDefaultPlatform;
    private GameObject newPlatform;

    private Vector3 currentPlatformPosition;
    private Vector3 newPlatformPosition;

    private float spawnCooldown = 0.1f;
    private float nextSpawnTime = 0f;

    [SerializeField] private float platformGap = 20f;

    void Start()
    {
        currentPlatformPosition = lastDefaultPlatform.position;
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float distanceX = player.transform.position.x - currentPlatformPosition.x;

            if (Mathf.Abs(distanceX) <= 80 && Time.time >= nextSpawnTime)
            {
                nextSpawnTime = Time.time + spawnCooldown;

                newPlatformPosition = currentPlatformPosition;

                newPlatformPosition.x += platformGap;

                if (Random.Range(0, 8) != 5)
                {
                    newPlatform = Instantiate(RoadPlatform, newPlatformPosition, Quaternion.identity);
                }
                else 
                {
                    newPlatform = Instantiate(PlantPlatform, newPlatformPosition, Quaternion.identity);

                }

                if (newPlatform != null)
                {
                    currentPlatformPosition = newPlatformPosition;
                }
            }
        }
    }

}
