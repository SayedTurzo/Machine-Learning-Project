using UnityEngine;

namespace _MapSystem.Scripts
{
    public class GridSpawner : MonoBehaviour
    {
        public GameObject objectToSpawn; // The object to spawn
        public Vector3 startingPosition; // Starting position for spawning
        public float gridWidth = 10f; // Width of the grid area
        public float gridHeight = 10f; // Height of the grid area
        public int rows = 5; // Number of rows in the grid
        public int columns = 5; // Number of columns in the grid

        void OnDrawGizmosSelected()
        {
            // Calculate the starting position based on gridHeight
            Vector3 adjustedStartingPosition = startingPosition + new Vector3(0f, gridHeight, 0f);

            // Draw wireframe cube for the grid area
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(adjustedStartingPosition + new Vector3(gridWidth / 2f, -gridHeight / 2f, 0f), new Vector3(gridWidth, gridHeight, 0f));

            // Calculate row and column spacing
            float rowSpacing = gridHeight / (float)(rows - 1);
            float columnSpacing = gridWidth / (float)(columns - 1);

            // Draw grid lines for rows
            for (int i = 0; i < rows; i++)
            {
                Vector3 rowStart = adjustedStartingPosition + new Vector3(0f, -i * rowSpacing, 0f);
                Vector3 rowEnd = rowStart + new Vector3(gridWidth, 0f, 0f);
                Gizmos.DrawLine(rowStart, rowEnd);
            }

            // Draw grid lines for columns
            for (int i = 0; i < columns; i++)
            {
                Vector3 columnStart = adjustedStartingPosition + new Vector3(i * columnSpacing, 0f, 0f);
                Vector3 columnEnd = columnStart + new Vector3(0f, -gridHeight, 0f);
                Gizmos.DrawLine(columnStart, columnEnd);
            }
        }

        void Awake()
        {
            SpawnGrid();
        }

        void SpawnGrid()
        {
            // Calculate row and column spacing
            float rowSpacing = gridHeight / (float)(rows - 1);
            float columnSpacing = gridWidth / (float)(columns - 1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    // Calculate the x, y, and z positions for the current grid cell
                    float xPosition = startingPosition.x + col * columnSpacing;
                    float yPosition = startingPosition.y + row * rowSpacing;
                    float zPosition = startingPosition.z;

                    // Spawn the object at the calculated position
                    Vector3 spawnPosition = new Vector3(xPosition, yPosition, zPosition);
                    GameObject newObj = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
                    newObj.transform.SetParent(this.gameObject.transform);
                
                    // Attach a ClickableObject component to the instantiated object
                    ClickableObject clickableObj = newObj.AddComponent<ClickableObject>();
                    clickableObj.OnClick += (clickableObj,position) => MapManager.Instance.HandleClick(clickableObj,position);
                }
            }
        }
    }
}
