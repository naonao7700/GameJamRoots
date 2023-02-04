using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes
{
    public class ResultScene : BaseScene
    {
        [SerializeField] private Image backGround;

        protected override void OnEnter()
        {
            waitTimer.Reset();
            var color = backGround.color;
            color.a = 0.0f;
            backGround.color = color;
        }

        protected override void EnterUpdate()
        {
            var t = waitTimer.GetRate();
            var color = backGround.color;
            color.a = Mathf.Lerp(0.0f, 0.5f, t);
            backGround.color = color;
        }

        protected override void InitUpdate()
        {
            base.InitUpdate();
            OnPhaseChange(PhaseID.Update);
        }

        protected override void MainUpdate()
        {
            if( Input.GetKeyDown(KeyCode.Space))
            {
                nextSceneID = SceneID.Title;
                OnPhaseChange(PhaseID.Exit);
            }
        }
    }

}

