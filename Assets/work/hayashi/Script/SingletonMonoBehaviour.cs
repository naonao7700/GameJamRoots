using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;
    
    public static T Instance 
    { 
        get
        {
            Type type = typeof(T);
            instance = (T)FindObjectOfType(type);
            if(instance == null)
            {
                Debug.LogError("インスタンスが存在しません");
            }
            return instance;
        }
    }

    private void Awake()
    {
        Check();
    }

    bool Check()
    {
        if (instance == null)
        {
            instance = this as T;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }
        Destroy(this);
        return false;
    }
}
