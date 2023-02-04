using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text text;

    public void SetText( int value )
    {
        text.text = "Score:" + value.ToString();
    }
}
