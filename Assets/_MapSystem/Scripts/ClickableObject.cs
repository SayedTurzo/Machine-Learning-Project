using UnityEngine;

namespace _MapSystem.Scripts
{
    public class ClickableObject : MonoBehaviour
    {
        public event System.Action<GameObject,Vector3> OnClick;

        void OnMouseDown()
        {
            Debug.Log("Clicked on obstacle");
            OnClick?.Invoke(gameObject,transform.position);
        }
    }
}