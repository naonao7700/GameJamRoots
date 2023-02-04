//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class GameManager : SingletonMonoBehaviour<GameManager>
//{
//    enum GameState
//    {
//        Title,
//        Game,
//        Result
//    }

//    GameState gameState = GameState.Game;

//    [Space(1), Header("Title")]
//    [SerializeField]
//    GameObject titleUI;

//    [Space(1), Header("Game")]
//    [SerializeField]
//    GameObject gameUI;

//    [SerializeField]
//    float defaultTime;

//    float timer;
//    public float Timer{ get => timer; }

//    [SerializeField]
//    Text timerText;

//    int score;
//    public int Score { get => score;  }

//    [SerializeField]
//    Text gameScoreText;

//    [Space(1), Header("Result")]
//    [SerializeField]
//    GameObject resultUI;

//    [SerializeField]
//    Text resultText;
    

//    public bool IsOutGame { 
//        get 
//        {
//            return gameState != GameState.Game;  
//        } 
//    }

//    private void Start()
//    {
//        TitleStart();
//    }

//    private void Update()
//    {
//        switch (gameState)
//        {
//            case GameState.Title:
//                TitleUpdate();
//                break;
//            case GameState.Game:
//                GameUpdate();
//                break;
//            case GameState.Result:
//                ResultUpdate();
//                break;
//        }


        
        
//    }

//    void TimerCount()
//    {
//        if(timer <= 0)
//        {
//            GameEnd();
//            return;
//        }
//        timer -= Time.deltaTime;

//    }

//    void ResetTimer()
//    {
//        timer = defaultTime;
//    }

//    void DisplayTimer()
//    {
//        timerText.text = $"Limit : {Mathf.Ceil(timer)}";
//    }

//    void DisplayScore()
//    {
//        gameScoreText.text = $"Score : {score}";
//    }

//    void ResetScore()
//    {
//        score = 0;
//    }

//    public void AddScore(int s)
//    {
//        score += s;
//    }
 
    
//    void TitleStart()
//    {
//        gameState = GameState.Title;
//        titleUI.SetActive(true);
        
//        //ランキング取得
//    }

//    void TitleUpdate()
//    {
//        if(Input.GetKeyDown(KeyCode.Space))
//        {
//            titleUI.SetActive(false);
//            GameStart();
//        }
        
//    }

//    void GameStart()
//    {
//        gameState = GameState.Game;
//        gameUI.SetActive(true);
//        ResetTimer();
//        ResetScore();
//    }

//    void GameUpdate()
//    {
//        TimerCount();
//        DisplayTimer();
//        DisplayScore();
//    }

//    void GameEnd()
//    {
//        gameUI.SetActive(false);
//        ResultStart();
//    }


//    void ResultStart()
//    {
//        gameState = GameState.Result;
//        resultUI.SetActive(true);
        
//        //スコア出力
//    }

//    void ResultUpdate()
//    {
//        if(Input.GetKeyDown(KeyCode.Space))
//        {
//            resultUI.SetActive(false);
//            TitleStart();
//        }
//    }

//}
