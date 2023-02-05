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
        [SerializeField] private AudioClip submitSE;

        [SerializeField] private Score score;

        [SerializeField] private Text text;
        [SerializeField] private float readyTime;
        [SerializeField] private float goTime;
        [SerializeField] private int initStep;

        [SerializeField] private Image ruleImage;
        [SerializeField] private bool ruleFlag;
        [SerializeField] private bool ruleFlag2;
        [SerializeField] private Timer ruleTimer;

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
            Mole.Instance.Init();

            ruleFlag = true;
            ruleFlag2 = false;
            ruleImage.gameObject.SetActive(true);
            ruleImage.rectTransform.localScale = Vector3.zero;

        }

        protected override void OnInit()
        {
            ruleTimer.Reset();
        }

        protected override void InitUpdate()
        {
            if( ruleFlag )
            {
                ruleTimer.DoUpdate(Time.deltaTime);
                var t = ruleTimer.GetRate();
                ruleImage.rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);

                if( ruleTimer.IsEnd() )
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ruleFlag2 = true;
                        ruleFlag = false;
                        ruleTimer.Reset();
                        GameManager.Instance.PlaySE(submitSE);
                    }
                }

                return;
            }

            if( ruleFlag2 )
            {
                ruleTimer.DoUpdate(Time.deltaTime);
                var t = ruleTimer.GetRate();
                ruleImage.rectTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);

                if( ruleTimer.IsEnd() )
                {
                    ruleFlag2 = false;
                    text.text = "Ready";
                    text.enabled = true;
                    initStep = 0;
                    waitTimer.Reset(readyTime);
                    ruleFlag = false;
                    ruleImage.gameObject.SetActive(false);
                }
                return;
            }

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

