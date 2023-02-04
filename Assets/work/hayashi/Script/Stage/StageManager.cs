using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    [SerializeField]
    float stageSizeMin;
    public float GetStageSizeMin { get => stageSizeMin; }


    [SerializeField]
    float stageSizeMax;
    public float GetStageSizeMax { get => stageSizeMax; }



}
