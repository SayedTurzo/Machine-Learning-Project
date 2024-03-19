using UnityEngine;
using UnityEngine.UI;

namespace _MapSystem.Scripts
{
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
}
