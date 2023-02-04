using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldenBar : MonoBehaviour
{
    [SerializeField] private Image baseImage;
    [SerializeField] private Image image;
    [SerializeField] private Vector3 viewPos;   //表示座標
    [SerializeField] private Vector3 hidePos;   //非表示座標

    [SerializeField] private float count;
    [SerializeField] private float time;

    public float rate;
    public State state;

    public enum State
    {
        Hide,
        Enter,
        View,
        Exit,
    };


    public void SetRate( float rate )
    {
        image.fillAmount = rate;
    }

    [ContextMenu("StartGolden")]
    public void StartGoldenTime()
    {
        if( state == State.Hide )
        {
            state = State.Enter;
        }
    }

    [ContextMenu("EndGolden")]
    public void EndGoldenTime()
    {
        if( state == State.View )
        {
            state = State.Exit;
        }
    }

    public void ResetBar()
    {
        rate = 0.0f;
        state = State.Hide;
        baseImage.rectTransform.anchoredPosition = hidePos;
    }

    private void Update()
    {
        if( state == State.Enter )
        {
            count += Time.deltaTime;
            if (count > time) count = time;
            rate = count / time;

            if( count >= time )
            {
                state = State.View;
            }
            var t = rate;
            t = t * t * (3.0f - 2.0f * t);
            baseImage.rectTransform.anchoredPosition = Vector3.Lerp(hidePos, viewPos, t);
        }
        else if( state == State.Exit )
        {
            count -= Time.deltaTime;
            if (count < 0) count = 0.0f;
            rate = count / time;

            if (count <= 0.0f)
            {
                state = State.Hide;
            }
            var t = 1.0f - rate;
            t = t * t;
            baseImage.rectTransform.anchoredPosition = Vector3.Lerp(viewPos, hidePos, t);
        }
    }

}
