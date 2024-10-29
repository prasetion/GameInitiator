using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameInitiator : MonoBehaviour
{
    private Queue<IInitialize> initializationQueue = new Queue<IInitialize>();

    void Start()
    {
        List<IInitialize> initializers = GetOrderedComponents<IInitialize>();
        foreach (var initializer in initializers)
        {
            AddInitializationStep(initializer);
        }

        StartInitialization().Forget();
    }

    public void AddInitializationStep(IInitialize step)
    {
        initializationQueue.Enqueue(step);
    }

    public async UniTask StartInitialization()
    {
        while (initializationQueue.Count > 0)
        {
            IInitialize initializer = initializationQueue.Dequeue();
            await initializer.Initialize();
        }

        Debug.Log("All initialization steps completed.");
    }

    public List<T> GetOrderedComponents<T>() where T : class
    {
        List<T> orderedComponents = new List<T>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            T component = child.GetComponent<T>();

            if (component != null)
            {
                orderedComponents.Add(component);
            }
        }

        return orderedComponents;
    }
}
