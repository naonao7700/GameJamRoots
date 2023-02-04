using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes
{
    public class TitleScene : BaseScene
    {
        [SerializeField] private AudioClip submitSE;

        protected override void OnInit()
        {
            base.OnInit();
            waitTimer.Reset();
        }

        protected override void InitUpdate()
        {
            base.InitUpdate();
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
        }

    }

}

