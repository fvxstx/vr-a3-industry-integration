using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BasketChecker : MonoBehaviour
{
    public string correctItemTag = "CorrectEPI";
    public string wrongItemTag = "WrongEPI";
    public int requiredCount = 3;
    public UnityEvent onAllItemsCollected;

    private HashSet<GameObject> correctItemsInside = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(correctItemTag))
        {
            correctItemsInside.Add(other.transform.root.gameObject);
            CheckWinCondition();
        }

        else if (other.CompareTag(wrongItemTag))
        {
            Debug.Log("Wrong EPI detected! Restarting scenario...");
            RestartScenario();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(correctItemTag))
        {
            correctItemsInside.Remove(other.transform.root.gameObject);
        }
    }

    private void CheckWinCondition()
    {
        if (correctItemsInside.Count >= requiredCount)
        {
            Debug.Log("All correct EPIs successfully collected!");

            onAllItemsCollected.Invoke();
        }
    }

    private void RestartScenario()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
