using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] renders;
    [SerializeField] private Sprite[] buds;
    [SerializeField] private Sprite[] flowers;

    [SerializeField] private int[] points;

    // Update is called once per frame
    void Update()
    {
        int score = GameManager.Instance.GetScore();
        for( int i=0; i<renders.Length; ++i )
        {
            if (score >= points[i])
            {
                renders[i].sprite = flowers[i];
            }
            else
            {
                renders[i].sprite = buds[i];
            }

        }        
    }
}
