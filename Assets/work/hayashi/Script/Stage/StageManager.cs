using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    [SerializeField]
    Vector2 stageSizeMin;
    public Vector2 GetStageSizeMin { get => stageSizeMin; }


    [SerializeField]
    Vector2 stageSizeMax;
    public Vector2 GetStageSizeMax { get => stageSizeMax; }



}
