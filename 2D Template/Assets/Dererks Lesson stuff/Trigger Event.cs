using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class TriggerEvent : MonoBehaviour
{
    //UnityEvenets allow you to create generic scripts instead of repeating yourself across multiple scripts.
    public UnityEvent onTriggerEnter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Runs each of the functions assigned in the inspector.
        onTriggerEnter.Invoke();
    }


    // For a function to be compatible with UntiyEvenets, it must be public , a void, and take a simple variable type.
    // By simple, I mean string, int, bool, or float. It can't be a Vector or Matrix, or anything complex
    public void Swticheroo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Vanquish(string objectName)
    {
        
    }

}
