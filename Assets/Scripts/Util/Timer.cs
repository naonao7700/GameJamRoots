using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Timer
{
    [SerializeField] private float count;
    [SerializeField] private float time;

    public void DoUpdate( float deltaTime )
    {
        count += deltaTime;
        if (count > time) count = time;
    }

    public bool IsEnd() { return count >= time; }
    public float GetRate()
    {
        if (time <= 0.0f) return 1.0f;
        return count / time; 
    }
    public void Reset()
    {
        count = 0.0f;
    }
    public void Reset( float time )
    {
        this.time = time;
        count = 0;
    }
}
