using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : SingletonMonoBehaviour<GameTimer>
{
    [SerializeField] private Image image;

    public float time;
    public float maxTime;

    public void SetTime( float time )
    {
        this.time = time;
        maxTime = time;
        SetImage();
    }

    public void DoUpdate( float deltaTime )
    {
        time -= deltaTime;
        if (time < 0.0f) time = 0.0f;
        SetImage();
    }

    public void SetImage( )
    {
        float rate = 0;
        if( maxTime != 0.0f )
        {
            rate = time / maxTime;
        }
        image.fillAmount = rate;
    }

    public bool IsEnd()
    {
        return time <= 0.0f;
    }

}
