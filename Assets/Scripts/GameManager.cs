using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenes;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    //現在のシーン番号
    [SerializeField] private SceneID sceneID;

    //シーンの参照(不変)
    [SerializeField] private BaseScene[] scenes;

    //初期化処理
    private void Start()
    {
        //シーンを一旦全て非表示にする
        for( int i=0; i<scenes.Length; ++i )
        {
            scenes[i].SceneExit();
        }

        //タイトル画面を最初に表示する
        sceneID = SceneID.Title;
        scenes[(int)sceneID].SceneEnter();
    }

    //シーンの切替
    public void OnChangeScene(SceneID sceneID)
    {
        scenes[(int)this.sceneID].SceneExit();
        this.sceneID = sceneID;
        scenes[(int)this.sceneID].SceneEnter();
    }

    //ゲーム中ではないかを取得する
    public bool IsOutGame
    {
        get
        {
            //ゲームシーンかつゲーム実行中でなければゲーム中じゃない
            return !(sceneID == SceneID.Game && scenes[(int)SceneID.Game].GetPhaseID() == PhaseID.Update);
            //return gameState != GameState.Game;
        }
    }

    public int score;

    //スコアを加算する処理
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

    //フェードの時間
    [SerializeField] private float m_fadeTime;
    public float FadeTime => m_fadeTime;
}
