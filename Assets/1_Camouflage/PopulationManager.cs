using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _1_Camouflage;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Video;
using Random = UnityEngine.Random;

public class PopulationManager : MonoBehaviour
{
    public GameObject personPrefab;
    public int populationSize = 10;
    private List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    public int trialTime = 10;
    private int generation = 1;

    public float minX, maxX, minY, maxY , minSize , maxSize;

    private GUIStyle _guiStyle = new GUIStyle();
    private void OnGUI()
    {
        _guiStyle.fontSize = 50;
        _guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10,10,100,20), "Generation: "+ generation,_guiStyle);
        GUI.Label(new Rect(10,60,100,20), "Trial time: "+ (int)elapsed, _guiStyle);
    }

    private void Start()
    {
        for (int i = 0;i< populationSize;i++)
        {
            Vector3 pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            GameObject go = Instantiate(personPrefab, pos, quaternion.identity);
            go.GetComponent<_1_Camouflage.DNA>().r = Random.Range(0.0f, 1.0f);
            go.GetComponent<_1_Camouflage.DNA>().g = Random.Range(0.0f, 1.0f);
            go.GetComponent<_1_Camouflage.DNA>().b = Random.Range(0.0f, 1.0f);
            go.GetComponent<_1_Camouflage.DNA>().scale = Random.Range(minSize, maxSize);
            population.Add(go);
        }
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed>trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }

    private void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();
        //get rid o unfit people
        List<GameObject> sortedLIst = population.OrderBy(o => o.GetComponent<_1_Camouflage.DNA>().timeToDie).ToList();
        
        population.Clear();

        for (int i= (int) (sortedLIst.Count/2f)-1;i<sortedLIst.Count-1;i++)
        {
            population.Add(Breed(sortedLIst[i],sortedLIst[i+1]));
            population.Add(Breed(sortedLIst[i+1],sortedLIst[i]));
        }

        for (int i=0;i<sortedLIst.Count;i++)
        {
            Destroy(sortedLIst[i]);
        }

        generation++;
    }

    private GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        GameObject offspring = Instantiate(personPrefab, pos, quaternion.identity);
        _1_Camouflage.DNA dna1 = parent1.GetComponent<_1_Camouflage.DNA>();
        _1_Camouflage.DNA dna2 = parent2.GetComponent<_1_Camouflage.DNA>();

        //swap parent DNA
        if (Random.Range(0,1000) >  5)
        {
            Debug.Log("First is on");
            offspring.GetComponent<_1_Camouflage.DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
            offspring.GetComponent<_1_Camouflage.DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
            offspring.GetComponent<_1_Camouflage.DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
            offspring.GetComponent<_1_Camouflage.DNA>().scale = Random.Range(0, 10) < 5 ? dna1.scale : dna2.scale;
        }
        else
        {
            Debug.Log("second is on");
            offspring.GetComponent<_1_Camouflage.DNA>().r = Random.Range(0.1f,1.0f);
            offspring.GetComponent<_1_Camouflage.DNA>().g = Random.Range(0.1f,1.0f);
            offspring.GetComponent<_1_Camouflage.DNA>().b = Random.Range(0.1f,1.0f);
            offspring.GetComponent<_1_Camouflage.DNA>().scale = Random.Range(minSize,maxSize);
        }

        return offspring;
    }
}
