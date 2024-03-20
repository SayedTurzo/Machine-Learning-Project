using UnityEngine;
using System.Collections;

public class CoroutineExample : MonoBehaviour
{
    // Coroutine to print a message after waiting for a few seconds
    IEnumerator PrintMessage()
    {
        Debug.Log("Coroutine started");
        
        // Wait for 2 seconds
        yield return new WaitForSeconds(2);
        
        Debug.Log("Coroutine resumed after 2 seconds");
        StartCoroutine(PrintMessage());
    }

    void Start()
    {
        // Start the coroutine when the object is enabled
        StartCoroutine(PrintMessage());
    }
}