using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes
{
    public enum SceneID
    {
        Title,
        Game,
        Result,
    };

    public enum PhaseID
    {
        Enter,
        Init,
        Update,
        Finish,
        Exit,
    };

    public class BaseScene : MonoBehaviour
    {
        private PhaseID phaseID;    //現在のフェーズ番号
        protected Timer waitTimer;  //タイマー
        public PhaseID GetPhaseID() => phaseID; //フェーズを取得する
        protected SceneID nextSceneID;  //次のシーン番号

        //更新関数
        protected virtual void EnterUpdate()
        {
            var t = waitTimer.GetRate();
            Fade.Instance.SetRate(1.0f - t);
            if( waitTimer.IsEnd() )
            {
                OnPhaseChange(PhaseID.Init);
            }
        }
        protected virtual void InitUpdate() { }
        protected virtual void MainUpdate() { }
        protected virtual void FinishUpdate() { }
        protected virtual void ExitUpdate()
        {
            var t = waitTimer.GetRate();
            Fade.Instance.SetRate(t);
            if( waitTimer.IsEnd() )
            {
                GameManager.Instance.OnChangeScene(nextSceneID);
            }
        }

        //フェーズを切り替えた時の処理
        protected virtual void OnEnter()
        {
            waitTimer.Reset(GameManager.Instance.FadeTime);
            Fade.Instance.SetRate(1.0f);
        }
        protected virtual void OnInit() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnFinish() { }
        protected virtual void OnExit()
        {
            waitTimer.Reset( GameManager.Instance.FadeTime );
            Fade.Instance.SetRate(0.0f);
        }

        //フェーズの切替
        protected void OnPhaseChange(PhaseID phaseID)
        {
            this.phaseID = phaseID;
            switch (phaseID)
            {
                case PhaseID.Enter: OnEnter(); break;
                case PhaseID.Init: OnInit(); break;
                case PhaseID.Update: OnUpdate(); break;
                case PhaseID.Finish: OnFinish(); break;
                case PhaseID.Exit: OnExit(); break;
            }
        }

        //更新処理
        private void Update()
        {
            //タイマーの更新
            waitTimer.DoUpdate(Time.deltaTime);

            //フェーズの処理を行う
            switch (phaseID)
            {
                case PhaseID.Enter: EnterUpdate(); break;
                case PhaseID.Init: InitUpdate(); break;
                case PhaseID.Update: MainUpdate(); break;
                case PhaseID.Finish: FinishUpdate(); break;
                case PhaseID.Exit: ExitUpdate(); break;
            }
        }

        //シーンに入ってきたとき
        public void SceneEnter()
        {
            gameObject.SetActive(true);
            OnPhaseChange(PhaseID.Enter);
        }

        //シーンを終わるとき
        public virtual void SceneExit()
        {
            gameObject.SetActive(false);
        }

    }

}

