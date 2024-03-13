using UnityEngine;

namespace _1_Camouflage
{
    public class DNA : MonoBehaviour
    {
        public float r;
        public float g;
        public float b;

        public float scale;

        private bool dead = false;
        public float timeToDie = 0;
        private SpriteRenderer sRenderer;
        private Collider2D sCollider;
        private Transform sTransform;

        private void OnMouseDown()
        {
            dead = true;
            timeToDie = PopulationManager.elapsed;
            Debug.Log("Dead At: "+ timeToDie);
            sRenderer.enabled = false;
            sCollider.enabled = false;
        }

        private void Start()
        {
            sRenderer = GetComponent<SpriteRenderer>();
            sCollider = GetComponent<Collider2D>();
            sTransform = GetComponent<Transform>();
            sRenderer.color = new Color(r, g, b);
            sTransform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
