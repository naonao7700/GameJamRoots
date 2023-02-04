using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes
{
    public class TitleScene : BaseScene
    {
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
                nextSceneID = SceneID.Game;
                OnPhaseChange(PhaseID.Exit);
            }
        }

    }

}

