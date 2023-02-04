using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{

    //à⁄ìÆéûä‘
    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float goldMoveSpeed;

    //é˚änÇ©Ç©ÇÈéûä‘
    [SerializeField]
    float harvestTime;
    [SerializeField]
    SpriteRenderer harvestSprite;

    Animator animator;

    //ÉvÉåÉCÉÑÅ[ÇÃëÂÇ´Ç≥
    float playerSize;

    bool isHarvest = false;


    GameManager gameManager;

    StageManager stageManager;

    Vector3 dir;


    AudioSource source;

    [SerializeField]
    RuntimeAnimatorController nomalContoroller;

    [SerializeField]
    RuntimeAnimatorController goldContoroller;

    [Header("SE")]
    [SerializeField]
    AudioClip move;
    [SerializeField]
    AudioClip carrotGrab;
    [SerializeField]
    AudioClip pull;


    bool isAnimGold;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        stageManager = StageManager.Instance;
        playerSize = transform.lossyScale.x / 2;
        animator = GetComponent<Animator>();
        harvestSprite.enabled = false;
        source = GetComponent<AudioSource>();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAnimGold && !GoldenManager.Instance.goldenFlag)
        {
            animator.runtimeAnimatorController = nomalContoroller;
        }

        if (gameManager.IsOutGame)
        {

            return;
        }
        
        Move();
    }

    //à⁄ìÆ
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

        if (GoldenManager.Instance.goldenFlag)
        {
            v *= Time.deltaTime * goldMoveSpeed;
        }
        else
        {
            v *= Time.deltaTime * moveSpeed;
        }


        if (stageManager.GetStageSizeMin.x > transform.position.x + v.x - playerSize || transform.position.x + v.x + playerSize > stageManager.GetStageSizeMax.x)
        {
            v.x = 0;
        }
        if (stageManager.GetStageSizeMin.y > transform.position.z + v.z - playerSize || transform.position.z + v.z + playerSize > stageManager.GetStageSizeMax.y)
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

            if (Input.GetKey(KeyCode.Space) && !isHarvest)
            {


                StartCoroutine(HarvestCarrots(other.gameObject.GetComponent<Carrot>()));

            }

        }

    }



    //é˚än

    IEnumerator HarvestCarrots(Carrot carrot)
    {

        isHarvest = true;

        source.PlayOneShot(carrotGrab);

        yield return null;
        if (carrot == null)
        {
            yield break;
        }

        
        if (dir.z > 0 && !GoldenManager.Instance.goldenFlag)
        {

            animator.Play("ForwardHarvestEnter");
        }
        else if (dir.z < 0 && !GoldenManager.Instance.goldenFlag)
        {

            animator.Play("BackHarvestEnter");

        }
        else if (dir.x > 0 && !GoldenManager.Instance.goldenFlag)
        {
            
            animator.Play("RightHarvestEnter");

        }
        else if (dir.x < 0 && !GoldenManager.Instance.goldenFlag)
        {

            animator.Play("LeftHarvestEnter");
        }
        Mole.Instance.SetCarrotState(carrot.GetState());
        switch (carrot.GetState())
        {
            case Carrot.CarrotState.Enter:
            case Carrot.CarrotState.Exit:
                break;

            case Carrot.CarrotState.Active00:

                carrot.carrotStop = true;
                harvestSprite.enabled = true;
                for (int n = 0; n < 2; n++)
                {
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || GoldenManager.Instance.goldenFlag);
                    yield return null;
                }
                gameManager.AddScore(30); ;
                break;
            case Carrot.CarrotState.Active01:
                carrot.carrotStop = true;
                harvestSprite.enabled = true;
                for (int n = 0; n < 1; n++)
                {
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || GoldenManager.Instance.goldenFlag);
                    yield return null;
                }
                gameManager.AddScore(20); ;
                break;
            case Carrot.CarrotState.Active02:
                carrot.carrotStop = true;
                gameManager.AddScore(10); ;
                break;
        }

        if(! GoldenManager.Instance.goldenFlag)
        {
            animator.SetTrigger("isHarvestEnd");
        }
        else if (dir.z > 0 )
        {

            animator.Play("ForwardHarvestExit");
        }
        else if (dir.z < 0)
        {

            animator.Play("BackHarvestExit");

        }
        else if (dir.x > 0)
        {

            animator.Play("RightHarvestExit");

        }
        else if (dir.x < 0)
        {

            animator.Play("LeftHarvestExit");
        }


        //ÉSÅ[ÉãÉfÉìÇ…ÇÒÇ∂ÇÒéÊìæÇµÇΩéû
        if (carrot.CheckGold())
        {
            isAnimGold = true;
            GoldenManager.Instance.StartGoldenTime();
            animator.runtimeAnimatorController = goldContoroller;
            animator.SetFloat("animSpeed", 2);
        }

        carrot.GetCarrot();


        harvestSprite.enabled = false;
        source.PlayOneShot(pull);
        yield return new WaitForSeconds(harvestTime);

        isHarvest = false;



        yield break;
    }

    public void Initialize()
    {
        transform.localPosition = Vector3.zero;
        animator.Play("BackStay");
    }

    public void GameEnd()
    {
        source.Pause();
    }


}
