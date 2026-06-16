using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class BasketChecker : MonoBehaviour
{
    public string correctItemTag = "CorrectEPI";
    public string wrongItemTag = "WrongEPI";
    public int requiredCount = 3;
    public UnityEvent onAllItemsCollected;

    private HashSet<GameObject> correctItemsInside = new HashSet<GameObject>();

    private struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    private Dictionary<GameObject, TransformData> initialStates = new Dictionary<GameObject, TransformData>();

    private void Start()
    {
        StoreInitialStates(correctItemTag);
        StoreInitialStates(wrongItemTag);
    }

    private void StoreInitialStates(string tag)
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject item in items)
        {
            GameObject rootObj = item.transform.root.gameObject;

            if (!initialStates.ContainsKey(rootObj))
            {
                TransformData data = new TransformData
                {
                    position = rootObj.transform.position,
                    rotation = rootObj.transform.rotation
                };
                initialStates.Add(rootObj, data);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(correctItemTag))
        {
            correctItemsInside.Add(other.transform.root.gameObject);
            CheckWinCondition();
        }
        else if (other.CompareTag(wrongItemTag))
        {
            Debug.Log("Wrong EPI detected! Soft resetting scenario...");
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
        correctItemsInside.Clear();

        foreach (var kvp in initialStates)
        {
            GameObject item = kvp.Key;
            TransformData startData = kvp.Value;

            if (item != null)
            {
                item.transform.position = startData.position;
                item.transform.rotation = startData.rotation;

 
                Rigidbody rb = item.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }
    }
}