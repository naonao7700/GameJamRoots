using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenManager : SingletonMonoBehaviour<GoldenManager>
{
    [SerializeField] private GoldenBar bar;

    //�S�[���f����Ԃ�����
    public bool goldenFlag;

    //�S�[���f���^�C��
    public Timer goldenTimer;

    //�S�[���f���^�C���J�n
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
