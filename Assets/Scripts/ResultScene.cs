using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes
{
    public class ResultScene : BaseScene
    {
        [SerializeField] private AudioClip submitSE;
        [SerializeField] private float fadeTime;
        [SerializeField] private Image backGround;
        [SerializeField] private GameObject uiObject;
        [SerializeField] private Ranking ranking;
        [SerializeField] private Score score;


        protected override void OnEnter()
        {
            uiObject.SetActive(false);
            waitTimer.Reset( fadeTime );
            var color = backGround.color;
            color.a = 0.0f;
            backGround.color = color;

            score.SetText(GameManager.Instance.GetScore());
            ranking.CountRanking(GameManager.Instance.GetScore());
        }

        protected override void EnterUpdate()
        {
            var t = waitTimer.GetRate();
            var color = backGround.color;
            color.a = Mathf.Lerp(0.0f, 0.5f, t);
            backGround.color = color;
            if( waitTimer.IsEnd() )
            {
                uiObject.SetActive(true);
                OnPhaseChange(PhaseID.Update);
            }
        }

        protected override void MainUpdate()
        {
            if( Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.PlaySE(submitSE);
                nextSceneID = SceneID.Game;
                OnPhaseChange(PhaseID.Exit);
            }
            else if( Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.PlaySE(submitSE);
                nextSceneID = SceneID.Title;
                OnPhaseChange(PhaseID.Exit);
            }
        }
    }

}

