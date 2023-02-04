using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text text;

    public int scoreValue;

    public void ResetScore( )
    {
        scoreValue = 0;
    }

    public void AddScore( int value )
    {
        scoreValue += value;
        if (scoreValue < 0)
        {
            scoreValue = 0;
        }
        SetText(scoreValue);
    }

    public void SetText( int value )
    {
        text.text = scoreValue.ToString();
    }
}
