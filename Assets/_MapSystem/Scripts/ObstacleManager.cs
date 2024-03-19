using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace _MapSystem.Scripts
{
    public class ObstacleManager : MonoBehaviour
    {
        // Data structure to hold information about spawned obstacles
        [System.Serializable]
        public class ObstacleData
        {
            public string name;
            public Vector3 position;
            public Quaternion rotation;
            public string spriteName; // Additional data for SpriteRenderer
            // Add any other relevant data fields here
        }

        // List to hold spawned obstacles' data
        public List<ObstacleData> spawnedObstaclesData = new List<ObstacleData>();
    
        // List to hold JSON data strings
        private List<string> jsonDataList = new List<string>();

        // Key to save/load jsonDataList using PlayerPrefs
        private string jsonDataListKey = "ObstacleDataList";

        // private void Start()
        // {
        //     LoadJsonDataList();
        // }

        public void InitMaps()
        {
            foreach (Transform child in MapManager.Instance.mapsGallery.transform)
            {
                // Destroy the child GameObject
                Destroy(child.gameObject);
            }
        
            LoadJsonDataList();
            Debug.Log(jsonDataList.Count);
            if (MapManager.Instance.galleryMapButton == null)
            {
                Debug.LogError("Obstacle prefab is not assigned!");
                return;
            }

            if (jsonDataList == null)
            {
                Debug.LogWarning("No obstacle data available to spawn!");
                return;
            }

            // foreach (string jsonData in jsonDataList)
            // {
            //     // Instantiate the obstacle prefab using the data from each ObstacleData object
            //     GameObject newMap = Instantiate(MapManager.Instance.galleryMapButton, MapManager.Instance.mapsGallery.transform, true);
            //     newMap.GetComponent<Button>().onClick.AddListener(() => LoadObstacles(jsonDataList.IndexOf(jsonData)));
            // }
        
            for (int i = 0; i < jsonDataList.Count; i++)
            {
                int index = i; // Capture the index in a local variable to avoid closure issues
                // Instantiate the obstacle prefab using the data from each ObstacleData object
                GameObject newMap = Instantiate(MapManager.Instance.galleryMapButton, MapManager.Instance.mapsGallery.transform, true);
                newMap.GetComponent<Button>().onClick.AddListener(() => LoadObstacles(index));
                newMap.GetComponent<Button>().onClick.AddListener(ClosePanel);
            }
        }

        private void ClosePanel()
        {
            MapManager.Instance.mapsGallery.SetActive(false);
        }

        // Spawn a new obstacle
        public void SpawnObstacle(GameObject obstacle)
        {
            SpriteRenderer spriteRenderer = obstacle.GetComponent<SpriteRenderer>();
            string spriteName = (spriteRenderer != null) ? spriteRenderer.sprite.name : ""; // Get sprite name if SpriteRenderer exists
            // Record obstacle data
            spawnedObstaclesData.Add(new ObstacleData
            {
                name = obstacle.name,
                position = obstacle.transform.position,
                rotation = obstacle.transform.rotation,
                spriteName = spriteName // Additional data for SpriteRenderer
            });
        }

        // Save spawned obstacles' data
        public void SaveObstacles()
        {
            // Clear the existing list
            spawnedObstaclesData.Clear();

            // Populate the list with data from children
            foreach (Transform child in transform)
            {
                SpawnObstacle(child.gameObject);
            }

            // Serialize the list of ObstacleData objects
            string jsonData = JsonUtility.ToJson(new ObstacleDataWrapper { obstacles = spawnedObstaclesData });

            // Save the JSON data to the file
            File.WriteAllText(Application.persistentDataPath + "/obstaclesData.json", jsonData);

            // Add the JSON data to the list
            jsonDataList.Add(jsonData);

            // Save jsonDataList using PlayerPrefs
            SaveJsonDataList();
        
            // Debug log to indicate successful saving
            Debug.Log("Obstacles data saved successfully.");
            MapManager.Instance.hideLevelEditor();
        }

        // Load spawned obstacles' data and recreate obstacles
        public void LoadObstacles(int index)
        {
            // Load jsonDataList from PlayerPrefs
            LoadJsonDataList();

            if (index >= 0 && index < jsonDataList.Count)
            {
                string jsonData = jsonDataList[index];

                // Deserialize the JSON data into a ObstacleDataWrapper object
                ObstacleDataWrapper wrapper = JsonUtility.FromJson<ObstacleDataWrapper>(jsonData);

                // Clear existing obstacles
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }

                // Recreate obstacles...
                foreach (ObstacleData obstacleData in wrapper.obstacles)
                {
                    GameObject newObstacle = new GameObject(obstacleData.name);
                    newObstacle.transform.position = obstacleData.position;
                    newObstacle.transform.rotation = obstacleData.rotation;

                    // Add SpriteRenderer component if sprite name exists
                    if (!string.IsNullOrEmpty(obstacleData.spriteName))
                    {
                        SpriteRenderer spriteRenderer = newObstacle.AddComponent<SpriteRenderer>();
                        // Load sprite by name
                        Sprite sprite = Resources.Load<Sprite>(obstacleData.spriteName);
                        if (sprite != null)
                        {
                            spriteRenderer.sprite = sprite;
                        }
                        else
                        {
                            Debug.LogWarning("Sprite not found with name: " + obstacleData.spriteName);
                        }
                    }

                    newObstacle.AddComponent<CircleCollider2D>();
                    ClickableObject clickableObj = newObstacle.AddComponent<ClickableObject>();
                    clickableObj.OnClick += (clickableObj,position) => MapManager.Instance.HandleClick(clickableObj,position);
                    newObstacle.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    newObstacle.transform.SetParent(transform);
                }

                // Debug log to indicate successful loading
                Debug.Log("Obstacles data loaded successfully.");
            }
            else
            {
                // Debug log to indicate invalid index
                Debug.LogWarning("Invalid index provided for loading obstacles.");
            }
        }
    
        // Save jsonDataList using PlayerPrefs
        private void SaveJsonDataList()
        {
            string jsonDataListString = JsonUtility.ToJson(new StringListWrapper { dataList = jsonDataList });
            PlayerPrefs.SetString(jsonDataListKey, jsonDataListString);
            PlayerPrefs.Save();
        }
    
    
        // Load jsonDataList from PlayerPrefs
        private void LoadJsonDataList()
        {
            if (PlayerPrefs.HasKey(jsonDataListKey))
            {
                string jsonDataListString = PlayerPrefs.GetString(jsonDataListKey);
                StringListWrapper wrapper = JsonUtility.FromJson<StringListWrapper>(jsonDataListString);
                jsonDataList = wrapper.dataList;
            }
        }

        // Wrapper class to serialize and deserialize the list of ObstacleData objects
        [System.Serializable]
        private class ObstacleDataWrapper
        {
            public List<ObstacleData> obstacles;
        }
    
        // Wrapper class to serialize and deserialize a list of strings
        [System.Serializable]
        private class StringListWrapper
        {
            public List<string> dataList;
        }
    }
}
