using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    [SerializeField] GameObject Carrot1;
    [SerializeField] GameObject Carrot2;
    [SerializeField] GameObject Carrot3;
    [SerializeField] GameObject CarrotEffect;
    [SerializeField] GameObject GoldCarrotEffect;


    public float getSpeed;

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

    [SerializeField]
    bool GoldFlag;

    CapsuleCollider capsule;

    bool isAcquired = false;
    /// <summary>
    /// 金人参かどうかの判定
    /// </summary>
    /// <returns></returns>
    public bool CheckGold()
	{
        return GoldFlag;
	}
    /// <summary>
    /// 人参を回収した時
    /// </summary>
    public void GetCarrot()
	{
        capsule.enabled = false;
        isAcquired = true;
        //人参の座標
        Vector3 carrotPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        
        //金人参の時
        if (CheckGold() == true)
        {
            Instantiate(GoldCarrotEffect, carrotPos, Quaternion.Euler(0, 0, 0));
        }
        //普通の人参の時
        else
        {
            Instantiate(CarrotEffect, carrotPos, Quaternion.Euler(0, 0, 0));
        }
        
	}

    /// <summary>
    /// 成長を止める
    /// </summary>
    public bool carrotStop;  
    
	float time;
    

    void Start()
    {
        time = 0;
        capsule = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if(isAcquired)
        {
            transform.Translate(transform.up * Time.deltaTime * getSpeed);
        }

        //プレイヤーが回収していない時
        if (carrotStop == false)            
		{
            time += Time.deltaTime;
        }

        if (time >= 4.5f)
            Destroy(gameObject);            //破壊
		
        if (time >= 4.0f)
		{
            state = CarrotState.Exit;
            //埋まっていく
            transform.position = transform.position - transform.up * 1 * Time.deltaTime;
        }

        
        //金人参のとき
        //if(CarrotManager.Gold == true)
        if( GoldFlag )
		{
            state = CarrotState.Active00;
            if (time <= 0.5f)
            {
                //生えてくる
                transform.position = transform.position + transform.up * 1 * Time.deltaTime / 2;
            }
        }
        

        
        //金人参じゃないとき
        if (time >= 3.5f)
        {
            state = CarrotState.Active02;
            //人参の状態変化（枯れる）
            Carrot3.SetActive(true);
            Carrot2.SetActive(false);
        }
        else if (time >= 3.0f)
        {
            state = CarrotState.Active01;
            //人参の状態変化（傷つき）
            Carrot2.SetActive(true);
            Carrot1.SetActive(false);
        }
        else if (time > 0.5)
        {
            state = CarrotState.Active00;
        }
        if (time <= 0.5f)
        {
            //生えてくる
            transform.position = transform.position + transform.up * 1 * Time.deltaTime;
        }
    }
}
