using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleSceneManager : MonoBehaviour
{
    InputField gameTimeInput;
    int maxTime = 600;
    int minTime = 60;

    public TextMeshProUGUI highscoreText;

    private void Start()
    {
        MenuManager.instance.LoadHighScore();
        highscoreText.text = "HighScore : " + MenuManager.instance.score.ToString();
        gameTimeInput = GameObject.Find("GameTime").GetComponent<InputField>();
    }

    public void startNEW()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


    public void setGameTime()
    {
        int gameTime = int.Parse(gameTimeInput.text);
        if (gameTime < minTime)
            gameTime = minTime;
        else if (gameTime > maxTime)
            gameTime = maxTime;

        MenuManager.instance.gameTime = gameTime;
    }
}
