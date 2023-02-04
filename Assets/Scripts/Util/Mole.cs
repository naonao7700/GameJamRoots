using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//モグラの処理
public class Mole : SingletonMonoBehaviour<Mole>
{
    [SerializeField] private Transform mole;
    [SerializeField] private Text text;
    [SerializeField] private float posY;
    [SerializeField] private float vy;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity;
    [SerializeField] private int jumpCount;
    [SerializeField] private bool jumpFlag;
    [SerializeField] private Timer textTimer;
    [SerializeField] private bool textFlag;

    private int carrotCount;
    [SerializeField] private int comboNum;

    public void Init()
    {
        carrotCount = 0;
        var pos = mole.localPosition;
        pos.y = posY;
        mole.localPosition = pos;

        textFlag = false;
        text.enabled = false;

        jumpCount = 0;
        jumpFlag = false;
    }

    //取ったニンジンの種類を設定する
    public void SetCarrotState( Carrot.CarrotState state )
    {
        if( state == Carrot.CarrotState.Active00 )
        {
            carrotCount++;
            if( carrotCount >= comboNum )
            {
                OnCombo();
                carrotCount = 0;
            }
        }
        else
        {
            carrotCount = 0;
        }
    }

    //コンボが発動したときに呼ばれる
    private void OnCombo()
    {
        if( !jumpFlag )
        {
            jumpFlag = true;
            vy = jumpPower;
            jumpCount = 0;
        }

        textTimer.Reset();
        if ( !textFlag )
        {
            textFlag = true;
            text.enabled = true;
        }
    }

    private void Update()
    {
        var pos = mole.localPosition;
        pos.y += vy * Time.deltaTime;
        vy -= gravity * Time.deltaTime;

        if (pos.y < posY)
        {
            pos.y = posY;

            if( jumpFlag )
            {
                if( jumpCount > 0 )
                {
                    jumpFlag = false;
                }
                else
                {
                    jumpCount++;
                    vy = jumpPower;
                }
            }
        }
        mole.localPosition = pos;

        if( textFlag )
        {
            textTimer.DoUpdate(Time.deltaTime);

            if( textTimer.IsEnd() )
            {
                textFlag = false;
                text.enabled = false;
            }
        }
    }
}
