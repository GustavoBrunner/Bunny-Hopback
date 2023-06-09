using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour
{
    private static LightsManager _instance;

    public static LightsManager instance { get => _instance; }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }

    
}
