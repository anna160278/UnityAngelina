using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager3 : MonoBehaviour
{
    public static GameManager3 instance;
    public int currentScore = 0;

    private void Awake()
    {
        //  Проверяем, существует ли уже экземпляр GameManager
        if (instance == null)
        {
            // Если нет, то делаем текущий экземпляр основным экземпляром
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Если уже существует другой экземпляр GameManager, уничтожаем этот
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", currentScore);
        PlayerPrefs.Save();
    }

    public void LoadScore()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            currentScore = PlayerPrefs.GetInt("Score");
        }
        else
        {
            currentScore = 0;
        }
    }
}
