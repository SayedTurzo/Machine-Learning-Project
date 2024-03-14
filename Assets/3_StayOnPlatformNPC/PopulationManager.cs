using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3_StayOnPlatformNPC
{
    public class PopulationManager : MonoBehaviour
    {
        public GameObject botPrefab;
        public int populationSize = 50;
        private List<GameObject> population = new List<GameObject>();
        public static float elapsed = 0;
        public float trialTime = 5;
        private int generation = 1;

        

        
        private GUIStyle _guiStyle = new GUIStyle();
        private void OnGUI()
        {
            _guiStyle.fontSize = 25;
            _guiStyle.normal.textColor = Color.white;
            GUI.BeginGroup(new Rect(10,10,250,150));
            GUI.Box(new Rect(0,0,140,140),"Stats",_guiStyle);
            GUI.Label(new Rect(10,25,200,30), "Generation: "+ generation,_guiStyle);
            GUI.Label(new Rect(10,50,200,30), $"Time: {elapsed:0.00}", _guiStyle);
            GUI.Label(new Rect(10,75,200,30), "Population: "+ population.Count,_guiStyle);
            GUI.EndGroup();
        }
        
        private void Start()
        {
            for (int i = 0; i < populationSize; i++)
            {
                var position = this.transform.position;
                Vector3 startingPosition = new Vector3(position.x + Random.Range(-2, 2), position.y, position.z + Random.Range(-2, 2));
                GameObject go = Instantiate(botPrefab, startingPosition, this.transform.rotation);
                go.GetComponent<Brain>().Init();
                population.Add(go);
            }
        }
        
        GameObject Breed(GameObject parent1,GameObject parent2)
        {
            var position = this.transform.position;
            Vector3 startingPosition = new Vector3(position.x + Random.Range(-2, 2), position.y, position.z + Random.Range(-2, 2));
            GameObject offspring = Instantiate(botPrefab, startingPosition, this.transform.rotation);
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
            List<GameObject> sortedLIst = population.OrderBy(o => (o.GetComponent<Brain>().timeWalking+o.GetComponent<Brain>().timeALive)).ToList();
        
            population.Clear();

            for (int i= (int) (sortedLIst.Count/2f)-1;i<sortedLIst.Count-1;i++)
            {
                population.Add(Breed(sortedLIst[i],sortedLIst[i+1]));
                population.Add(Breed(sortedLIst[i+1],sortedLIst[i]));
            }

            foreach (var t in sortedLIst)
            {
                Destroy(t);
            }

            generation++;
        }
        
        private void Update()
        {
            elapsed += Time.deltaTime;
            if (elapsed>=trialTime)
            {
                BreedNewPopulation();
                elapsed = 0;
            }
        }
    }
}
