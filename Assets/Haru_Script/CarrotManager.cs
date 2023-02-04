using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotManager : MonoBehaviour
{
    [SerializeField] GameObject Carrot;

    public AudioClip carrotSpawn;
    AudioSource audioSource;

    #region
    [Header("GenerationTime")]
    public float wave1;
    public float wave2;
    #endregion
    float time1;
    float time2;

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


        Vector3 pos = new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(-4.5f, 4.5f));

        if (time1 >= wave1)
		{
            audioSource.PlayOneShot(carrotSpawn);
            Instantiate(Carrot, pos, Quaternion.Euler(0, 0, 0));
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
