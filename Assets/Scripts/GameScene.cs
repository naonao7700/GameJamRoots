using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes
{
    public class GameScene : BaseScene
    {
        [SerializeField] private Score score;

        private void Start()
        {
            nextSceneID = SceneID.Result;
            score.ResetScore();
        }


    }

}

