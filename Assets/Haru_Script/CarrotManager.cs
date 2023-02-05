using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotManager : MonoBehaviour
{
    [SerializeField] GameObject Carrot;
    [SerializeField] GameObject GoldCarrot;

    public AudioClip carrotSpawn;
    AudioSource audioSource;

    #region
    [Header("GenerationTime")]
    public float wave1;
    public float wave2;
    #endregion
    float time1;
    float time2;
    //static public bool Gold;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {

        if (GameManager.Instance.IsOutGame) { return; }
        
        
        time1 += Time.deltaTime;
        time2 += Time.deltaTime;
        Vector2 min = StageManager.Instance.GetStageSizeMin;
        Vector2 max = StageManager.Instance.GetStageSizeMax;

        Vector3 pos = new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.y, max.y));

        if (time1 >= wave1)
		{
            audioSource.PlayOneShot(carrotSpawn);
            if (Random.Range(0, 100) >= 95)
			{
                // 20/19ÇÃämó¶Ç≈ã‡êléQî≠ê∂
                Instantiate(GoldCarrot, pos, Quaternion.Euler(0, 0, 0));
			}
            else
			{
                for (int i = 0; i < 3; ++i)
                {
                    pos = new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.y, max.y));
                    Instantiate(Carrot, pos, Quaternion.Euler(0, 0, 0));
                }
            }
            time1 = 0;
        }
        
        if (time2 >= wave2)
        {
            audioSource.PlayOneShot(carrotSpawn);
            Instantiate(Carrot, pos, Quaternion.Euler(0, 0, 0));
            time2 = 0;
        }
        

    }
}
