using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public bool IsOutGame
    {
        get
        {
            return false;
            //return gameState != GameState.Game;
        }
    }

    public void AddScore(int s)
    {
        //score += s;
    }
}
