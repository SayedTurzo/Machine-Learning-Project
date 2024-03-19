using System;
using UnityEngine;
using UnityEngine.XR;

namespace _4_MazeWalker
{
    public class Brain : MonoBehaviour
    {
        public int DNALength = 2;
        public DNA dna;
        public GameObject eye;
        private bool seeWall = true;
        private Vector3 startingPosition;
        public float distanceTravelled = 0;
        private bool alive = true;
        
        public void Init()
        {
            dna = new DNA(DNALength, 360);
            startingPosition = this.transform.position;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("dead"))
            {
                distanceTravelled = 0;
                alive = false;
            }
        }

        private void Update()
        {
            if (!alive)
            {
                return;
            }

            seeWall = false;
            RaycastHit hit;
            Debug.DrawRay(eye.transform.position, eye.transform.forward * 0.5f, Color.red);
            if (Physics.SphereCast(eye.transform.position,0.1f,eye.transform.forward,out hit,0.5f))
            {
                if (hit.collider.gameObject.CompareTag("wall"))
                {
                    seeWall = true;
                }
            }
        }

        private void FixedUpdate()
        {
            if (!alive) return;

            float h = 0;
            float v = dna.GetGene(0);

            if (seeWall)
            {
                h = dna.GetGene(1);
            }
            
            this.transform.Translate(0,0,v*0.0007f);
            this.transform.Rotate(0,h,0);
            distanceTravelled = Vector3.Distance(startingPosition, this.transform.position);
        }
    }
}
