using System.Collections;
using System.Collections.Generic;
using UMGS;
using UnityEngine;
using UnityEngine.Serialization;

public class MapManager : SingletonPersistent<MapManager>
{
    public GameObject obstaclesPanel;
    private GameObject pointObject;
    private Vector3 position;

    public GameObject grid;
    public GameObject obstaclesHolder;

    public GameObject mapsGallery;
    public GameObject galleryMapButton;

    public void HandleClick(GameObject pointObject,Vector3 position)
    {
        Debug.Log("Clicked object name: "+pointObject.name+"\n"+"Clicked object position: " + position);
        obstaclesPanel.SetActive(true);

        this.pointObject = pointObject;
        this.position = position;
    }

    public void SpawnObstacle(GameObject objectToSpawn,bool isNone)
    {
        Destroy(pointObject);
        GameObject newObj = Instantiate(objectToSpawn, position, Quaternion.identity);
        if (isNone)
        {
            newObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
            newObj.transform.SetParent(grid.transform);
        }
        else
        {
            newObj.GetComponent<SpriteRenderer>().sortingOrder = 2;
            newObj.transform.SetParent(obstaclesHolder.transform);
        }
        ClickableObject clickableObj = newObj.AddComponent<ClickableObject>();
        clickableObj.OnClick += (clickableObj,position) => HandleClick(clickableObj,position);
        obstaclesPanel.SetActive(false);
    }

    public void ShowLevelEditor()
    {
        grid.SetActive(true);
    }

    public void hideLevelEditor()
    {
        grid.SetActive(false);
    }
}
