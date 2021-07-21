using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    static public SpawnManager instance;

    int spawnTime = 2;
    bool isPaused;
    int gameTime = 300;
    [SerializeField] GameObject ballPrefab;

    public TextMeshProUGUI scoreL;
    public TextMeshProUGUI scoreR;
    public TextMeshProUGUI textR;
    public TextMeshProUGUI textL;
    public TextMeshProUGUI counter;
    public TextMeshProUGUI highscoreText;
    public GameObject gameoverScreen;
    public int scoreLL = 0;
    public int scoreRR = 0;
    public bool ballIsDestroyed = false;
    public bool wonR;

    
    // Start is called before the first frame update
    void Start()
    {
        if(MenuManager.instance.gameTime != 0)
            gameTime = MenuManager.instance.gameTime;

        highscoreText.text = "HighScore : "+ MenuManager.instance.score.ToString();
        Time.timeScale = 1;
        instance = this;
        StartCoroutine(MakeBall());
        StartCoroutine(TimeCounter());
    }

    // Update is called once per frame
    void Update()
    {
        if (ballIsDestroyed)
        {
            ballIsDestroyed = false;
            updateScore();
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
        float speedIncrement = (scoreLL + scoreRR) / 5f;
        return speedIncrement;
    }
    
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseButton()
    {
        isPaused = !isPaused;
        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void updateScore()
    {
        scoreL.text = "Score : " + scoreLL;
        scoreR.text = "Score : " + scoreRR;
    }

    IEnumerator TimeCounter()
    {
        while (gameTime != 0)
        {
            int min = gameTime / 60;
            int sec = gameTime - (min * 60);

            counter.text = "Time Left : " + min.ToString("00") + ":" + sec.ToString("00");
            gameTime--;
            yield return new WaitForSeconds(1);
        }

        if(gameTime == 0)
        {
            Time.timeScale = 0;
            int x = scoreRR >= scoreLL ? scoreRR : scoreLL;
            if (x > MenuManager.instance.score)
                MenuManager.instance.score = x;
            MenuManager.instance.SaveHighScore();
            gameoverScreen.gameObject.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        gameoverScreen.gameObject.SetActive(false);
    }
}
