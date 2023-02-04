using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{

    //移動時間
    [SerializeField]
    float moveSpeed;

    //収穫かかる時間
    [SerializeField]
    float harvestTime;
    [SerializeField]
    SpriteRenderer harvestSprite;

    Animator animator;

    //プレイヤーの大きさ
    float playerSize;

    bool isHarvest = false;


    GameManager gameManager;

    StageManager stageManager;

    Vector3 dir;


    AudioSource source;

    [Header("SE")]
    [SerializeField]
    AudioClip move;
    [SerializeField]
    AudioClip carrotGrab;
    [SerializeField]
    AudioClip pull;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        stageManager = StageManager.Instance;
        playerSize = transform.lossyScale.x / 2;
        animator = GetComponent<Animator>();
        harvestSprite.enabled = false;
        source = GetComponent<AudioSource>();
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
        moveAnimation(v);
        v.Normalize();
        if (v.x + v.z != 0 && !source.isPlaying)
        {
            source.Play();
        }
        else if (v.x + v.z == 0)
        {
            source.Pause();
        }

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



        if (v.x * v.y == 0)
        {
            dir = v;
        }


        transform.Translate(v);
    }

    void moveAnimation(Vector3 v)
    {

        if (isHarvest)
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
        if (v.x * v.z == 0)
        {
            animator.SetBool("isMove", false);
            return;
        }



    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Carrot") && !gameManager.IsOutGame)
        {
            //UI表示
            if (!harvestSprite.enabled)
            {

                harvestSprite.enabled = true;
            }
            if (Input.GetKey(KeyCode.Return) && !isHarvest)
            {


                StartCoroutine(HarvestCarrots(other.gameObject.GetComponent<Carrot>()));

            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (harvestSprite.enabled)
        {
            harvestSprite.enabled = false;
        }
    }

    //収穫

    IEnumerator HarvestCarrots(Carrot carrot)
    {

        isHarvest = true;
        source.PlayOneShot(carrotGrab);

        yield return null;
        if (carrot == null)
        {
            yield break;
        }

        switch (carrot.GetState())
        {
            case Carrot.CarrotState.Enter:
            case Carrot.CarrotState.Exit:
                break;

            case Carrot.CarrotState.Active00:
                carrot.carrotStop = true;
                for (int n = 0; n < 2; n++)
                {
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    yield return null;
                }
                gameManager.AddScore(30); ;
                break;
            case Carrot.CarrotState.Active01:
                carrot.carrotStop = true;
                for (int n = 0; n < 1; n++)
                {
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    yield return null;
                }
                gameManager.AddScore(20); ;
                break;
            case Carrot.CarrotState.Active02:
                carrot.carrotStop = true;
                gameManager.AddScore(10); ;
                break;
        }

        if (dir.z > 0)
        {
            animator.Play("ForwardHarvest");


        }
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

        Destroy(carrot.gameObject);


        harvestSprite.enabled = false;
        source.PlayOneShot(pull);
        yield return new WaitForSeconds(harvestTime);

        isHarvest = false;



        yield break;
    }




}
