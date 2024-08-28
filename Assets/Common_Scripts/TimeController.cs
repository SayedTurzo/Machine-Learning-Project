using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float timeScaleIncrement = 0.1f;
    public float maxTimeScale = 3.0f;
    public float minTimeScale = 0.1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            IncreaseTimeScale();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DecreaseTimeScale();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTimeScale();
        }
    }
    
    void OnGUI()
    {
        // Calculate the position for the label in the top-right corner
        float x = Screen.width - 110; // 100 width + 10 padding
        float y = 10; // 10 padding from the top
        GUI.Label(new Rect(x, y, 200, 200), "Time Scale: " + Time.timeScale.ToString("F2"));
    }

    void IncreaseTimeScale()
    {
        if (Time.timeScale < maxTimeScale)
        {
            Time.timeScale += timeScaleIncrement;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0, maxTimeScale);
            Debug.Log("Time Scale Increased: " + Time.timeScale);
        }
    }

    void DecreaseTimeScale()
    {
        if (Time.timeScale > minTimeScale)
        {
            Time.timeScale -= timeScaleIncrement;
            Time.timeScale = Mathf.Clamp(Time.timeScale, minTimeScale, maxTimeScale);
            Debug.Log("Time Scale Decreased: " + Time.timeScale);
        }
    }

    void ResetTimeScale()
    {
        Time.timeScale = 1.0f;
        Debug.Log("Time Scale Reset: " + Time.timeScale);
    }
}
