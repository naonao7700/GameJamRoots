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
    }

    public void DoUpdate( float deltaTime )
    {
        time -= deltaTime;
        SetText();
    }

    public void SetText( )
    {
        text.text = "Time:" + time.ToString();
    }

}
