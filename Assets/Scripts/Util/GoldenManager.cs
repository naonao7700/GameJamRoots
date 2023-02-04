using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenManager : SingletonMonoBehaviour<GoldenManager>
{
    [SerializeField] private GoldenBar bar;

    //ゴールデン状態か判定
    public bool goldenFlag;

    //ゴールデンタイム
    public Timer goldenTimer;

    //ゴールデンタイム開始
    public void StartGoldenTime()
    {
        goldenFlag = true;
        goldenTimer.Reset();
        bar.StartGoldenTime();
    }

    private void Update()
    {
        goldenTimer.DoUpdate(Time.deltaTime);
        if( goldenTimer.IsEnd() )
        {
            goldenFlag = false;
            bar.EndGoldenTime();
        }
    }


}
