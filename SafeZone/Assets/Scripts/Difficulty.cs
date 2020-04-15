using UnityEngine;

public static class Difficulty 
{

    private static float secondsToMaxDifficulty = 60f;

    public static float GetDifficultyPercent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }

}
