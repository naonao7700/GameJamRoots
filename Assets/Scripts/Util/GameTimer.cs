using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : SingletonMonoBehaviour<GameTimer>
{
    [SerializeField] private Text text;

    public float time;

    public void SetTime( float time )
    {
        this.time = time;
        SetText();
    }

    public void DoUpdate( float deltaTime )
    {
        time -= deltaTime;
        if (time < 0.0f) time = 0.0f;
        SetText();
    }

    public void SetText( )
    {
        text.text = "Time:" + time.ToString();
    }

    public bool IsEnd()
    {
        return time <= 0.0f;
    }

}
