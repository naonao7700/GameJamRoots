using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes
{
    public class GameScene : BaseScene
    {
        [SerializeField] private float gameTime;
        [SerializeField] private Player player;


        [SerializeField] private AudioClip startSE;
        [SerializeField] private AudioClip timeUpSE;

        [SerializeField] private Score score;

        [SerializeField] private Text text;
        [SerializeField] private float readyTime;
        [SerializeField] private float goTime;
        [SerializeField] private int initStep;

        [SerializeField] private float finishTime;

        protected override void OnEnter()
        {
            base.OnEnter();
            GameManager.Instance.SetScore(0);
            score.SetText(GameManager.Instance.GetScore());
            GameTimer.Instance.SetTime(gameTime);
            nextSceneID = SceneID.Result;
            text.enabled = false;
            GoldenManager.Instance.ResetGoldenTime();
            player.Initialize();

        }

        protected override void OnInit()
        {
            text.text = "Ready";
            text.enabled = true;
            initStep = 0;
            waitTimer.Reset(readyTime);
        }

        protected override void InitUpdate()
        {
            if( waitTimer.IsEnd() )
            {
                if( initStep == 0 )
                {
                    GameManager.Instance.PlaySE(startSE);
                    initStep++;
                    text.text = "GO!";
                    waitTimer.Reset(goTime);
                }
                else
                {
                    text.enabled = false;
                    OnPhaseChange(PhaseID.Update);
                }
            }
        }

        protected override void MainUpdate()
        {
            base.MainUpdate();
            GameTimer.Instance.DoUpdate(Time.deltaTime);
            score.SetText(GameManager.Instance.GetScore());

            if ( GameTimer.Instance.IsEnd() )
            {
                OnPhaseChange(PhaseID.Finish);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

        }

        protected override void OnFinish()
        {
            text.enabled = true;
            text.text = "Finish!";
            waitTimer.Reset(finishTime);
            GameManager.Instance.PlaySE(timeUpSE);
            GoldenManager.Instance.EndGoldenTime();
            player.GameEnd();
        }

        protected override void FinishUpdate()
        {
            if( waitTimer.IsEnd() )
            {
                GameManager.Instance.OnChangeScene(SceneID.Result);
                //OnPhaseChange(PhaseID.Exit);
            }
        }
    }

}

