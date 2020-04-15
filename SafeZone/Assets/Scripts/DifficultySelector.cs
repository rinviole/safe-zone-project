using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelector : MonoBehaviour
{
   public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    private void Awake()
    {
        //Resets the values to prevent constant value adding to itself.
        ResetDifficultyValues();
    }

    public void SelectZoneDifficulty(float zoneChangeAmount)
    {
        GameManager.zoneChangeAmount = zoneChangeAmount;
        ChangeScene();
    }
    public void SelectMinSpawnDifficulty(float spawnTimeMin)
    {
        ObstacleSpawner.spawnTimeMinMax.x = spawnTimeMin;
    }
    public void SelectMaxSpawnDifficulty(float spawnTimeMax)
    {
        ObstacleSpawner.spawnTimeMinMax.y = spawnTimeMax;
    }
    public void SelectZoneMaxSize(float zoneSize)
    {
        GameManager.safeZoneMax = zoneSize;
    }

    void ResetDifficultyValues()
    {
        ObstacleSpawner.spawnTimeMinMax = Vector2.zero;
        GameManager.zoneChangeAmount = 0;
        GameManager.safeZoneMax = 0;
    }
}
