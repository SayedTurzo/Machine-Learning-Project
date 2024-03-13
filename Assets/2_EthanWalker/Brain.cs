using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace _2_EthanWalker
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class Brain : MonoBehaviour
    {
        public int DNALength = 1;
        public float timeALive;
        public DNA dna;

        private ThirdPersonCharacter m_Character;
        private Vector3 m_Move;
        private bool m_Jump;
        private bool alive = true;

        public float distanceTravelled;
        private Vector3 startingPosition;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag.Equals("dead"))
            {
                alive = false;
            }
        }

        public void Init()
        {
            //Initialize DNA



            dna = new DNA(DNALength, 6);
            m_Character = GetComponent<ThirdPersonCharacter>();
            timeALive = 0;
            alive = true;
            startingPosition = this.transform.position;
        }

        private void FixedUpdate()
        {
            float h = 0;
            float v = 0;
            bool crouch = false;
            if (dna.GetGene(0) == 0) v = 1;
            else if (dna.GetGene(0) == 1) v = -1;
            else if (dna.GetGene(0) == 2) h = -1;
            else if (dna.GetGene(0) == 3) h = 1;
            else if (dna.GetGene(0) == 4) m_Jump = true;
            else if (dna.GetGene(0) == 5) crouch = true;

            m_Move = v * Vector3.forward + h * Vector3.right;
            m_Character.Move(m_Move,crouch,m_Jump);
            m_Jump = false;
            if (alive)
            {
                timeALive += Time.deltaTime;
                distanceTravelled = Vector3.Distance(this.transform.position, startingPosition);
            }
        }
    }
}
