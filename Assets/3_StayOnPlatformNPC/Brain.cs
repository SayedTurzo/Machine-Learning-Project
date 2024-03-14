using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace _3_StayOnPlatformNPC
{
    public class Brain : MonoBehaviour
    {
        public int DNALength = 2;
        public float timeALive;
        public float timeWalking;
        public DNA dna;
        public GameObject eye;
        private bool alive = true;
        private bool seeGround = true;
        public float speed=1f;
        
        public GameObject ethanPrefab;
        private GameObject ethan;

        private void OnDestroy()
        {
            Destroy(ethan);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag.Equals("dead"))
            {
                alive = false;
                timeALive = 0;
                timeWalking = 0;
            }
        }
        
        public void Init()
        {
            //Initialize DNA

            dna = new DNA(DNALength, 3);
            timeALive = 0;
            alive = true;
            ethan = Instantiate(ethanPrefab, this.transform.position, this.transform.rotation);
            ethan.GetComponent<AICharacterControl>().target = this.transform;
        }

        private void Update()
        {
            if (!alive) return;
            
            Debug.DrawRay(eye.transform.position,eye.transform.forward*10,Color.red,10);
            seeGround = false;
            RaycastHit hit;
            if (Physics.Raycast(eye.transform.position,eye.transform.forward*10,out hit))
            {
                if (hit.collider.gameObject.tag.Equals("platform"))
                {
                    seeGround = true;
                }
            }

            timeALive = PopulationManager.elapsed;

            float turn = 0;
            float move = 0;
            if (seeGround)
            {
                if (dna.GetGene(0) == 0) {move = 1; timeWalking += 1; }
                else if (dna.GetGene(0) == 1) turn = -90;
                else if (dna.GetGene(0) == 2) turn = 90;
            }
            else
            {
                if (dna.GetGene(1) == 0) {move = 1; timeWalking += 1;}
                else if (dna.GetGene(1) == 1) turn = -90;
                else if (dna.GetGene(1) == 2) turn = 90;
            }

            this.transform.Translate(0, 0, move * speed * Time.deltaTime);
            this.transform.Rotate(0,turn,0);
        }
    }
}
