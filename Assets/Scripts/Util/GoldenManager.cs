using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenManager : SingletonMonoBehaviour<GoldenManager>
{
    [SerializeField] private GoldenBar bar;
    [SerializeField] private AudioSource audioSource;

    //�S�[���f����Ԃ�����
    public bool goldenFlag = false;

    //�S�[���f���^�C��
    public Timer goldenTimer;

    private float bgmCount;
    [SerializeField] private float bgmFadeTime;
    public float bgmVolumeRate;

    //�S�[���f���^�C���J�n
    public void StartGoldenTime()
    {
        goldenFlag = true;
        goldenTimer.Reset();
        bar.StartGoldenTime();
    }

    public void EndGoldenTime()
    {
        bar.EndGoldenTime();
    }

    public void ResetGoldenTime()
    {
        goldenFlag = false;
        bar.ResetBar();
    }

    private void Update()
    {
        if( goldenFlag )
        {
            bgmCount += Time.deltaTime;
            if (bgmCount > bgmFadeTime) bgmCount = bgmFadeTime;
        }
        else
        {
            bgmCount -= Time.deltaTime;
            if (bgmCount < 0) bgmCount = 0;
        }
        bgmVolumeRate = bgmCount / bgmFadeTime;
        GameManager.Instance.SetBGMVolume(1.0f - bgmVolumeRate);
        audioSource.volume = bgmVolumeRate;

        goldenTimer.DoUpdate(Time.deltaTime);
        bar.SetRate(1.0f - goldenTimer.GetRate());
        if( goldenTimer.IsEnd() )
        {
            goldenFlag = false;
            bar.EndGoldenTime();
        }
    }


}
