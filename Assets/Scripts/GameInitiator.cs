using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameInitiator : MonoBehaviour
{
    // Queue untuk menyimpan semua inisialisasi
    private Queue<IInitialize> initializationQueue = new Queue<IInitialize>();

    // Start is called before the first frame update
    void Start()
    {
        List<IInitialize> initializers = GetOrderedComponents<IInitialize>();
        foreach (var initializer in initializers)
        {
            AddInitializationStep(initializer);
        }

        // Mulai proses inisialisasi
        StartInitialization().Forget();
    }

    // Fungsi untuk menambahkan inisialisasi ke dalam queue
    public void AddInitializationStep(IInitialize step)
    {
        initializationQueue.Enqueue(step);
    }

    // Memproses queue satu per satu menggunakan UniTask
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

        // Looping melalui setiap child dalam urutan transform
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            T component = child.GetComponent<T>();

            // Jika child memiliki komponen T, tambahkan ke list
            if (component != null)
            {
                orderedComponents.Add(component);
            }
        }

        return orderedComponents;
    }
}
