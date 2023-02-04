using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

    //移動時間
    [SerializeField]
    float moveSpeed;

    //収穫かかる時間
    [SerializeField]
    float harvestTime;
    [SerializeField]
    Text harvestText;


    [SerializeField]
    float stegeSizeMin;
    [SerializeField]
    float stegeSizeMax;

    //プレイヤーの大きさ
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

    //移動
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
            //UI表示
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

    //収穫
    IEnumerator HarvestCarrots()
    {
        if (Input.GetKey(KeyCode.Return) && !isHarvest )
        {
            isHarvest = true;
            //スコア加算
            GameManager.Instance.AddScore(10);
            yield return new WaitForSeconds(1f);
            isHarvest = false;
        }
        yield break;
    }

    


}
