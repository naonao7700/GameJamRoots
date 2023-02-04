using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{

    //�ړ�����
    [SerializeField]
    float moveSpeed;

    //���n�����鎞��
    [SerializeField]
    float harvestTime;
    [SerializeField]
    SpriteRenderer harvestSprite;

    Animator animator;

    //�v���C���[�̑傫��
    float playerSize;

    bool isHarvest = false;


    GameManager gameManager;

    StageManager stageManager;

    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        stageManager = StageManager.Instance;
        playerSize = transform.lossyScale.x / 2;
        animator = GetComponent<Animator>();
        harvestSprite.enabled = false;
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
        moveAnimation(v);
        v.Normalize();

        v *= Time.deltaTime * moveSpeed;

        
        if (stageManager.GetStageSizeMin > transform.position.x + v.x - playerSize || transform.position.x + v.x + playerSize > stageManager.GetStageSizeMax)
        {
            v.x = 0;
        }
        if (stageManager.GetStageSizeMin > transform.position.z + v.z - playerSize || transform.position.z + v.z + playerSize > stageManager.GetStageSizeMax)
        {
            v.z = 0;
        }

        
        dir = v;
        

        
        transform.Translate(v);
    }

    void moveAnimation(Vector3 v)
    {
       
        if(isHarvest)
        {
            return;
        }
        if (v.z > 0)
        {
            animator.SetBool("isMove", true);
            animator.Play("ForwardMove");
            return;
        }
        if (v.z < 0)
        {
            animator.SetBool("isMove", true);
            animator.Play("BackMove");
            return;
        }
        if (v.x > 0)
        {
            animator.SetBool("isMove", true);
            animator.Play("RightMove");
            return;
        }
        if (v.x < 0)
        {
            animator.SetBool("isMove", true);
            animator.Play("LeftMove");
            return;
        }
        if (v.x * v.z == 0 )
        {
            animator.SetBool("isMove", false);
            return;
        }
        


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Carrot") && !gameManager.IsOutGame)
        {
            //UI�\��
            if (!harvestSprite.enabled)
            {
                
                harvestSprite.enabled = true;
            }
            if (Input.GetKey(KeyCode.Return) && !isHarvest)
            {
                
                StartCoroutine(HarvestCarrots(other.gameObject));
            }
<<<<<<< Updated upstream
            StartCoroutine(HarvestCarrots());
=======
>>>>>>> Stashed changes
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (harvestSprite.enabled)
        {
            harvestSprite.enabled = false;
        }
    }

    //���n
<<<<<<< Updated upstream
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
=======
    IEnumerator HarvestCarrots(GameObject carrot)
    {

        isHarvest = true;

       

        yield return null;


        /*switch (CarrotState)
         {
             case state1:
                for (int n = 0; n < 2; n++)
                {
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    yield return null;
                }
                gameManager.AddScore(30);;
                break;
             case state2:
                 for (int n = 0; n < 1; n++)
                 {
                     yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                     yield return null;
                 }   
                gameManager.AddScore(20);;
                 break;
            case state3:
                gameManager.AddScore(10);;
                 break;
                
         }*/

        if (dir.z > 0)
        {
            animator.Play("ForwardHarvest");
        }
        else if (dir.z < 0)
        {

            animator.Play("BackHarvest");

        }
        else if (dir.x > 0)
        {

            animator.Play("RightHarvest");

        }
        else if (dir.x < 0)
        {
            animator.Play("LeftHarvest");
        }

        Destroy(carrot);
        harvestSprite.enabled = false;
        yield return new WaitForSeconds(harvestTime);
        isHarvest = false;
        

>>>>>>> Stashed changes
        yield break;
    }




}
