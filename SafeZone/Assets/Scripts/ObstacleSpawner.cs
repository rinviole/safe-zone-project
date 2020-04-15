using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public static Vector2 spawnTimeMinMax; 
    private float nextSpawnTime;
    float timeBetweenSpawns;
    int obstacleRandomIndex;
    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime && !GameOver.instance.GameIsOver && GameManager.instance.canSpawn)
        {
            timeBetweenSpawns = Mathf.Lerp(spawnTimeMinMax.x, spawnTimeMinMax.y, Difficulty.GetDifficultyPercent());
            nextSpawnTime = Time.time + timeBetweenSpawns;
            Spawner();
        }
    }

    void Spawner()
    {
        int randomSpawnPosIndex = Random.Range(0, 4);

        //Probability for monster 1 to 20.
        obstacleRandomIndex = Random.Range(0, 20);

        int obstacleIndex;
        if (obstacleRandomIndex >= 1)
            obstacleIndex = 0;
        else
            obstacleIndex = 1;

        Instantiate(obstaclePrefab[obstacleIndex], ScreenUtility.GetRandomPos()[randomSpawnPosIndex], Quaternion.identity);
    }
    
}

