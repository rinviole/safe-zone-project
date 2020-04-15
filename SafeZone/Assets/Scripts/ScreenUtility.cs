using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenUtility
{
    public static Vector2 GetScreenSize()
    {
        return new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    }

    public static Vector2 PlayerScreenSize() //GetScreen Size + player scale. GetScreenSize also used in obstacle spawner.
    {
        Transform playerObject = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 playerWidthHeight = new Vector2(playerObject.localScale.x, playerObject.localScale.y);
        return (GetScreenSize()) - playerWidthHeight / 2;
    }

    public static Vector2[] GetRandomPos()
    {
        Vector2[] randomPos = { new Vector2(-GetScreenSize().x, Random.Range(-GetScreenSize().y, GetScreenSize().y)),
                                new Vector2(GetScreenSize().x, Random.Range(-GetScreenSize().y, GetScreenSize().y)),
                                new Vector2(Random.Range(-GetScreenSize().x, GetScreenSize().x), GetScreenSize().y),
                                new Vector2(Random.Range(-GetScreenSize().x, GetScreenSize().x), -GetScreenSize().y)};
        return randomPos;
    }
}
