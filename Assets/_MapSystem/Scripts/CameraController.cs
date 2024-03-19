using UnityEngine;
using UnityEngine.UI;

namespace _MapSystem.Scripts
{
    public class CameraController : MonoBehaviour
    {
        public Slider slider; // Reference to the UI Slider
        public float minY = 0f; // Minimum y position
        public float maxY = 38f; // Maximum y position
        public float sensitivity = 0.1f; // Touch or mouse sensitivity

        private Vector2 lastInputPosition;

        void Start()
        {
            // Subscribe to the slider's OnValueChanged event
            slider.onValueChanged.AddListener(OnSliderValueChanged);

            // Set initial slider value based on the camera's initial position
            UpdateSliderValue();
        }

        void Update()
        {
            if (Input.touchSupported && Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    lastInputPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 delta = touch.position - lastInputPosition;
                    float deltaY = delta.y * sensitivity;

                    // Invert deltaY to match touch direction
                    deltaY *= -1;

                    MoveCamera(deltaY);

                    lastInputPosition = touch.position;
                }
            }
            else if (Input.mousePresent)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    lastInputPosition = Input.mousePosition;
                }
                else if (Input.GetMouseButton(0))
                {
                    Vector2 delta = (Vector2)Input.mousePosition - lastInputPosition;
                    float deltaY = delta.y * sensitivity;

                    MoveCamera(deltaY);

                    lastInputPosition = Input.mousePosition;
                }
            }
        }

        void MoveCamera(float deltaY)
        {
            // Invert deltaY to reverse camera movement
            //deltaY *= -1;

            // Update camera position within range
            Vector3 newPosition = transform.position + new Vector3(0f, deltaY, 0f);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
            transform.position = newPosition;

            // Update slider value based on camera's new position
            UpdateSliderValue();
        }

        void UpdateSliderValue()
        {
            // Invert camera's Y-position to reverse slider value
            float invertedY = maxY - (transform.position.y - minY);
        
            // Map inverted camera's Y-position to slider value within range
            float sliderValue = Mathf.InverseLerp(minY, maxY, invertedY);
            slider.value = sliderValue;
        }

        void OnSliderValueChanged(float value)
        {
            // Invert the slider value to reverse the movement
            value = 1f - value;

            // Map inverted slider value to camera's Y-position within range
            float newY = Mathf.Lerp(minY, maxY, value);
            Vector3 newPosition = transform.position;
            newPosition.y = newY;
            transform.position = newPosition;
        }
    }
}
