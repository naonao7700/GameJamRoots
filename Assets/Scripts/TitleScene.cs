using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes
{
    public class TitleScene : BaseScene
    {
        [SerializeField] private AudioClip submitSE;
        [SerializeField] private Image logo;
        [SerializeField] private float logoTime;
        [SerializeField] private Vector3 hidePos;
        [SerializeField] private Vector3 viewPos;

        [SerializeField] private RectTransform start;
        [SerializeField] private Vector3 hidePos2;
        [SerializeField] private Vector3 viewPos2;

        protected override void OnInit()
        {
            base.OnInit();
            waitTimer.Reset( logoTime );
        }

        protected override void InitUpdate()
        {
            base.InitUpdate();

            var t = waitTimer.GetRate();
            t = t * t * (3.0f - 2.0f * t);
            logo.rectTransform.anchoredPosition = Vector3.Lerp(hidePos, viewPos, t);
            start.anchoredPosition = Vector3.Lerp(hidePos2, viewPos2, t);
            if( waitTimer.IsEnd() )
            {
                OnPhaseChange(PhaseID.Update);
            }
        }

        protected override void MainUpdate()
        {
            base.MainUpdate();
            if( Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.PlaySE(submitSE);
                nextSceneID = SceneID.Game;
                OnPhaseChange(PhaseID.Exit);
            }

            if( Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

    }

}

