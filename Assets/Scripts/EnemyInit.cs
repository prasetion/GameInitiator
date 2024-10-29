using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyInit : MonoBehaviour, IInitialize
{
    public async UniTask Initialize()
    {
        Debug.Log("Initializing EnemyInit...");
        await UniTask.Delay(1000);
        Debug.Log("EnemyInit Initialization Completed.");
    }
}
