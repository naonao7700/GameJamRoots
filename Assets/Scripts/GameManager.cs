using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenes;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    //���݂̃V�[���ԍ�
    [SerializeField] private SceneID sceneID;

    //�V�[���̎Q��(�s��)
    [SerializeField] private BaseScene[] scenes;

    //����������
    private void Start()
    {
        //�V�[������U�S�Ĕ�\���ɂ���
        for( int i=0; i<scenes.Length; ++i )
        {
            scenes[i].SceneExit();
        }

        //�^�C�g����ʂ��ŏ��ɕ\������
        sceneID = SceneID.Title;
        scenes[(int)sceneID].SceneEnter();
    }

    //�V�[���̐ؑ�
    public void OnChangeScene(SceneID sceneID)
    {
        scenes[(int)this.sceneID].SceneExit();
        this.sceneID = sceneID;
        scenes[(int)this.sceneID].SceneEnter();
    }

    //�Q�[�����ł͂Ȃ������擾����
    public bool IsOutGame
    {
        get
        {
            //�Q�[���V�[�����Q�[�����s���łȂ���΃Q�[��������Ȃ�
            return !(sceneID == SceneID.Game && scenes[(int)SceneID.Game].GetPhaseID() == PhaseID.Update);
            //return gameState != GameState.Game;
        }
    }

    public int score;

    //�X�R�A�����Z���鏈��
    public void AddScore(int value )
    {
        score += value;
        if( score < 0 )
        {
            score = 0;
        }
    }

    public void SetScore( int value )
    {
        score = value;
    }

    public int GetScore() { return score; }

    //�t�F�[�h�̎���
    [SerializeField] private float m_fadeTime;
    public float FadeTime => m_fadeTime;
}
