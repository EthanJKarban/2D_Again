using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.Rendering;

public class CoroutineTest : MonoBehaviour
{


    Coroutine currentRoutine;


    private void Start()
    {
        currentRoutine = StartCoroutine(Test());

        bool playerIsAlive = true;
        while (playerIsAlive)
        {
            playerIsAlive = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopCoroutine(currentRoutine);
        }
    }

    IEnumerator Test()
    {
        while (true)
        {
            Debug.Log("Hello");


            yield return new WaitForSeconds(1);

            Debug.Log("World");

            
        }
    }





}
