using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ranking : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rank1;
    [SerializeField] TextMeshProUGUI rank2;
    [SerializeField] TextMeshProUGUI rank3;

    public int[] rank = { 0, 0, 0};

    public void CountRanking(int score)
	{
        if (rank[0] < score)
        {
            rank[2] = rank[1];
            rank[1] = rank[0];
            rank[0] = score;
        }
        else if (rank[1] < score)
        {
            rank[2] = rank[1];
            rank[1] = score;
        }
        else if (rank[2] < score)
        {
            rank[2] = score;
        }
	}

    public int[] GetRanking()
	{
        return rank;
	}

    void Update()
    {
        rank1.text = "1st: " + rank[0];
        rank2.text = "2nd: " + rank[1];
        rank3.text = "3rd: " + rank[2];

    }
}
