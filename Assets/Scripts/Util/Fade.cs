using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : SingletonMonoBehaviour<Fade>
{
    [SerializeField] private Image image;

    public void SetRate(float rate)
    {
        var color = image.color;
        color.a = rate;
        image.color = color;
    }
}
