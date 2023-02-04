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
        private PhaseID phaseID;    //���݂̃t�F�[�Y�ԍ�
        protected Timer waitTimer;  //�^�C�}�[
        public PhaseID GetPhaseID() => phaseID; //�t�F�[�Y���擾����
        protected SceneID nextSceneID;  //���̃V�[���ԍ�

        //�X�V�֐�
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

        //�t�F�[�Y��؂�ւ������̏���
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

        //�t�F�[�Y�̐ؑ�
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

        //�X�V����
        private void Update()
        {
            //�^�C�}�[�̍X�V
            waitTimer.DoUpdate(Time.deltaTime);

            //�t�F�[�Y�̏������s��
            switch (phaseID)
            {
                case PhaseID.Enter: EnterUpdate(); break;
                case PhaseID.Init: InitUpdate(); break;
                case PhaseID.Update: MainUpdate(); break;
                case PhaseID.Finish: FinishUpdate(); break;
                case PhaseID.Exit: ExitUpdate(); break;
            }
        }

        //�V�[���ɓ����Ă����Ƃ�
        public void SceneEnter()
        {
            gameObject.SetActive(true);
            OnPhaseChange(PhaseID.Enter);
        }

        //�V�[�����I���Ƃ�
        public virtual void SceneExit()
        {
            gameObject.SetActive(false);
        }

    }

}

