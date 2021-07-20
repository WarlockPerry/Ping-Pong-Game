using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    static public SpawnManager instance;
    [SerializeField] GameObject ballPrefab;
    public TextMeshProUGUI scoreL;
    public TextMeshProUGUI scoreR;
    public TextMeshProUGUI textR;
    public TextMeshProUGUI textL;
    public int scoreLL = 0;
    public int scoreRR = 0;

    public bool ballIsDestroyed = false;
    int spawnTime = 2;
    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(MakeBall());
    }

    // Update is called once per frame
    void Update()
    {
        if (ballIsDestroyed)
        {
            ballIsDestroyed = false;
            StartCoroutine(MakeBall());
        }
    }

    IEnumerator MakeBall()
    {
        yield return new WaitForSeconds(spawnTime);
        textL.gameObject.SetActive(false);
        textR.gameObject.SetActive(false);
        Instantiate(ballPrefab, ballPrefab.transform.position, Quaternion.identity);
    }

    public float BallSpeedIncrement()
    {
        float speedIncrement = (scoreLL + scoreRR) / 10f;
        return speedIncrement;
    }

    
}
