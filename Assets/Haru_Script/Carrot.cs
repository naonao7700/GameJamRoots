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
    /// ���l�Q���ǂ����̔���
    /// </summary>
    /// <returns></returns>
    public bool CheckGold()
	{
        return GoldFlag;
	}
    /// <summary>
    /// �l�Q�����������
    /// </summary>
    public void GetCarrot()
	{
        capsule.enabled = false;
        isAcquired = true;
        //�l�Q�̍��W
        Vector3 carrotPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        
        //���l�Q�̎�
        if (CheckGold() == true)
        {
            Instantiate(GoldCarrotEffect, carrotPos, Quaternion.Euler(0, 0, 0));
        }
        //���ʂ̐l�Q�̎�
        else
        {
            Instantiate(CarrotEffect, carrotPos, Quaternion.Euler(0, 0, 0));
        }
        
	}

    /// <summary>
    /// �������~�߂�
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

        //�v���C���[��������Ă��Ȃ���
        if (carrotStop == false)            
		{
            time += Time.deltaTime;
        }

        if (time >= 4.5f)
            Destroy(gameObject);            //�j��
		
        if (time >= 4.0f)
		{
            state = CarrotState.Exit;
            //���܂��Ă���
            transform.position = transform.position - transform.up * 1 * Time.deltaTime;
        }

        
        //���l�Q�̂Ƃ�
        //if(CarrotManager.Gold == true)
        if( GoldFlag )
		{
            state = CarrotState.Active00;
            if (time <= 0.5f)
            {
                //�����Ă���
                transform.position = transform.position + transform.up * 1 * Time.deltaTime / 2;
            }
        }
        

        
        //���l�Q����Ȃ��Ƃ�
        if (time >= 3.5f)
        {
            state = CarrotState.Active02;
            //�l�Q�̏�ԕω��i�͂��j
            Carrot3.SetActive(true);
            Carrot2.SetActive(false);
        }
        else if (time >= 3.0f)
        {
            state = CarrotState.Active01;
            //�l�Q�̏�ԕω��i�����j
            Carrot2.SetActive(true);
            Carrot1.SetActive(false);
        }
        else if (time > 0.5)
        {
            state = CarrotState.Active00;
        }
        if (time <= 0.5f)
        {
            //�����Ă���
            transform.position = transform.position + transform.up * 1 * Time.deltaTime;
        }
    }
}
