using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour
{
    static public MenuManager instance;

    public int gameTime;
    public int score;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // saving high scorer name and their score between sessions...
    [System.Serializable]
    class HighScore
    {
        public int highScore;
    }

    public void SaveHighScore()
    {
        HighScore data = new HighScore();
        data.highScore = score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefi.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefi.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScore data = JsonUtility.FromJson<HighScore>(json);

            score = data.highScore;
        }
    }
}
