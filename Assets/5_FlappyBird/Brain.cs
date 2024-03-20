using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _5_FlappyBird
{
    public class Brain : MonoBehaviour
    {
        private int DNALength = 5;
        public DNA dna;
        public GameObject eyes;
        private bool seeDownWall = false;
        private bool seeUpWall = false;
        private bool seeBottom = false;
        private bool seeTop = false;
        private Vector3 startPosition;
        public float timeAlive = 0;
        public float distanceTravelled = 0;
        public int crash = 0;
        private bool alive = true;
        private Rigidbody2D rb;

        public void Init()
        {
            //initialize dna
            //0 forward
            //1 up wall
            //2 down wall
            //3 normal upward
            dna = new DNA(DNALength, 200);
            this.transform.Translate(Random.Range(-1.5f,1.5f),Random.Range(-1.5f,1.5f),0);
            startPosition = this.transform.position;
            rb = this.GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.CompareTag("dead"))
            {
                alive = false;
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("top") || other.gameObject.CompareTag("bottom") ||
                other.gameObject.CompareTag("topWall") || other.gameObject.CompareTag("downWall"))
            {
                crash++;
            }
        }

        private void Update()
        {
            if(!alive) return;

            seeUpWall = false;
            seeDownWall = false;
            seeTop = false;
            seeBottom = false;
            RaycastHit2D hit = Physics2D.Raycast(eyes.transform.position, eyes.transform.forward, 1.0f);
            
            Debug.DrawRay(eyes.transform.position,eyes.transform.forward*1.0f,Color.red);
            Debug.DrawRay(eyes.transform.position,eyes.transform.up*1.0f,Color.red);
            Debug.DrawRay(eyes.transform.position,-eyes.transform.up*1.0f,Color.red);

            if (hit.collider!=null)
            {
                if (hit.collider.gameObject.CompareTag("topWall"))
                {
                    seeUpWall = true;
                }
                else if (hit.collider.gameObject.CompareTag("downWall"))
                {
                    seeDownWall = true;
                }
            }
            
            hit = Physics2D.Raycast(eyes.transform.position, eyes.transform.up, 1.0f);
            if (hit.collider!=null)
            {
                if (hit.collider.gameObject.CompareTag("top"))
                {
                    seeTop = true;
                }
            }
            
            hit = Physics2D.Raycast(eyes.transform.position, -eyes.transform.up, 1.0f);
            if (hit.collider!=null)
            {
                if (hit.collider.gameObject.CompareTag("bottom"))
                {
                    seeBottom = true;
                }
            }
            //timeAlive = PopulationManager.elapsed;
        }

        private void FixedUpdate()
        {
            if(!alive) return;
            float upforce = 0;
            float forwardForce = 1.0f;

            if (seeUpWall)
            {
                upforce = dna.GetGene(0);
            }
            else if (seeDownWall)
            {
                upforce = dna.GetGene(1);
            }
            else if(seeTop)
            {
                upforce = dna.GetGene(2);
            }
            else if (seeBottom)
            {
                upforce = dna.GetGene(3);
            }
            else
            {
                upforce = dna.GetGene(4);
            }
            rb.AddForce(this.transform.right*forwardForce);
            rb.AddForce(this.transform.up*upforce*.1f);
            distanceTravelled = Vector3.Distance(startPosition, this.transform.position);
        }
    }
}
