using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerInit : MonoBehaviour, IInitialize
{
    public async UniTask Initialize()
    {
        // throw new System.NotImplementedException();
        Debug.Log("Initializing PlayerInit...");
        await UniTask.Delay(100);
        Debug.Log("PlayerInit Initialization Completed.");
    }

}
