using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacles : MonoBehaviour
{
    public GameObject obstacleToSpawn;
    public Button button;

    public bool none=false;

    private void Start()
    {
        button.onClick.AddListener(SetSpawnInfo);
    }

    private void SetSpawnInfo()
    {
        MapManager.Instance.SpawnObstacle(obstacleToSpawn,none);
    }
}
