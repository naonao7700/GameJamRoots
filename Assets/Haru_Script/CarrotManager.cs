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
    static public bool Gold;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        
        if (GameManager.Instance.IsOutGame)
		{
            return;
		}
        
        time1 += Time.deltaTime;
        time2 += Time.deltaTime;
        float min = StageManager.Instance.GetStageSizeMin;
        float max = StageManager.Instance.GetStageSizeMax;

        Vector3 pos = new Vector3(Random.Range(min, max), 0, Random.Range(min, max));

        if (time1 >= wave1)
		{
            audioSource.PlayOneShot(carrotSpawn);
            if (Random.Range(0, 10) >= 9)
			{
                // 20/19‚ÌŠm—¦‚Å‹àlŽQ”­¶
                Instantiate(GoldCarrot, pos, Quaternion.Euler(0, 0, 0));
                Gold = true;
			}
            else
			{
                Instantiate(Carrot, pos, Quaternion.Euler(0, 0, 0));
                Gold = false;
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
