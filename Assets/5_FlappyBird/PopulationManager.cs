using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _5_FlappyBird
{
    public class PopulationManager : MonoBehaviour
    {
        public GameObject botPrefab;
        public GameObject startingPosition;
        public int populationSize = 50;
        private List<GameObject> population = new List<GameObject>();
        public static float elapsedTime = 0;
        public float trailTime = 5;
        private int generation = 1;
    
        private GUIStyle _guiStyle = new GUIStyle();
        private void OnGUI()
        {
            _guiStyle.fontSize = 25;
            _guiStyle.normal.textColor = Color.white;
            GUI.BeginGroup(new Rect(10,10,250,150));
            GUI.Box(new Rect(0,0,140,140),"Stats",_guiStyle);
            GUI.Label(new Rect(10,25,200,30), "Generation: "+ generation,_guiStyle);
            GUI.Label(new Rect(10,50,200,30), $"Time: {elapsedTime:0.00}", _guiStyle);
            GUI.Label(new Rect(10,75,200,30), "Population: "+ population.Count,_guiStyle);
            GUI.EndGroup();
        }
    
        private void Start()
        {
            for (int i = 0; i < populationSize; i++)
            {
                GameObject b = Instantiate(botPrefab, startingPosition.transform.position, this.transform.rotation);
                b.GetComponent<Brain>().Init();
                population.Add(b);
            }
        }
    
        GameObject Breed(GameObject parent1,GameObject parent2)
        {
            GameObject offspring = Instantiate(botPrefab, startingPosition.transform.position, this.transform.rotation);
            Brain b = offspring.GetComponent<Brain>();
            if (Random.Range(0,100)==1)
            {
                b.Init();
                b.dna.Mutate();
            }
            else
            {
                b.Init();
                b.dna.Combine(parent1.GetComponent<Brain>().dna,parent2.GetComponent<Brain>().dna);
            }

            return offspring;
        }
    
        void BreedNewPopulation()
        {
            List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Brain>().distanceTravelled).ToList();
            population.Clear();
            for (int i = (int) (3*sortedList.Count /4.0f)-1; i < sortedList.Count-1; i++)
            {
                population.Add(Breed(sortedList[i],sortedList[i+1]));
                population.Add(Breed(sortedList[i+1],sortedList[i]));
                population.Add(Breed(sortedList[i],sortedList[i+1]));
                population.Add(Breed(sortedList[i+1], sortedList[i]));
            }

            for (int i = 0; i < sortedList.Count; i++)
            {
                Destroy(sortedList[i]);
            }

            generation++;
        }
    
        private void Update()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime>=trailTime)
            {
                BreedNewPopulation();
                elapsedTime = 0;
            }
        }
    }
}
