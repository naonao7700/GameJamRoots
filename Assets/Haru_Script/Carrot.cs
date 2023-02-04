using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    [SerializeField] GameObject Carrot1;
    [SerializeField] GameObject Carrot2;
    [SerializeField] GameObject Carrot3;

	public enum CarrotState
	{ 
        Enter,
        Active00,
        Active01,
        Active02,
        Exit
    }
    CarrotState state = CarrotState.Enter;
    public CarrotState GetState()
	{
        return state;
	}

	float time;
    
    void Start()
    {
        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time >= 3.5f)
            Destroy(gameObject);

        if (time >= 3.0f)
		{
            state = CarrotState.Exit;
            transform.position = transform.position - transform.up * 1 * Time.deltaTime;
        }
        else if (time >= 2.5f)
		{
            state = CarrotState.Active02;
            //人参の状態変化（枯れる）
            Carrot3.SetActive(true);
            Carrot2.SetActive(false);
        }
        else if (time >= 1.5f)
		{
            state = CarrotState.Active01;
            //人参の状態変化（傷つき）
            Carrot2.SetActive(true);
            Carrot1.SetActive(false);
        }
        else if(time > 0.5)
		{
            state = CarrotState.Active00;
        }
        else if (time <= 0.5f)
		{
            //生えてくる
            transform.position = transform.position + transform.up * 1 * Time.deltaTime  ;
        }
    }

}
