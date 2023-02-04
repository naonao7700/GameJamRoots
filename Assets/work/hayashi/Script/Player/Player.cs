using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

    //�ړ�����
    [SerializeField]
    float moveSpeed;

    //���n�����鎞��
    [SerializeField]
    float harvestTime;
    [SerializeField]
    Text harvestText;


    [SerializeField]
    float stegeSizeMin;
    [SerializeField]
    float stegeSizeMax;

    //�v���C���[�̑傫��
    float playerSize;

    bool isHarvest = false;


    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        playerSize = transform.lossyScale.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
    }

    //�ړ�
    void Move()
    {
        if (isHarvest || gameManager.IsOutGame)
        {
            return;
        }

        Vector3 v = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            v += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            v += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            v += Vector3.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            v += Vector3.left;
        }

        v.Normalize();

        v *= Time.deltaTime * moveSpeed;


        if (stegeSizeMin > transform.position.x + v.x - playerSize || transform.position.x + v.x + playerSize > stegeSizeMax)
        {
            v.x = 0;
        }
        if (stegeSizeMin > transform.position.z + v.z - playerSize || transform.position.z + v.z + playerSize > stegeSizeMax)
        {
            v.z = 0;
        }

        transform.Translate(v);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Carrot") && !gameManager.IsOutGame)
        {
            //UI�\��
            if(!harvestText.enabled)
            { 
                harvestText.enabled = true;
            }
            StartCoroutine(HarvestCarrots());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Carrot"))
        {
            harvestText.enabled = false;
        }
    }

    //���n
    IEnumerator HarvestCarrots()
    {
        if (Input.GetKey(KeyCode.Return) && !isHarvest )
        {
            isHarvest = true;
            //�X�R�A���Z
            GameManager.Instance.AddScore(10);
            yield return new WaitForSeconds(1f);
            isHarvest = false;
        }
        yield break;
    }

    


}
