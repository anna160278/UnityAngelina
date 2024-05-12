using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;


public class GameManagerTest : MonoBehaviour
{
    public static GameManagerTest instance;
    private TargetList list;
    [SerializeField] private Text levelsText;
    [SerializeField] private Text scoreText;
    public int score; // Убрали начальное значение, оно сохранится между сценами.
    public bool isNext;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Подписка на событие загрузки новой сцены.
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Отписка от события.
    }

    // Метод, который будет вызван после загрузки новой сцены
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndInitializeUI();
        list = GameObject.Find("Main Camera").GetComponent<TargetList>();
        isNext = list.levelLoaded;
    }

    private void FindAndInitializeUI()
    {
        // Поиск элементов UI на новой сцене
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        levelsText = GameObject.Find("Levels").GetComponent<Text>();

        if (scoreText == null || levelsText == null)
        {
            Debug.LogError("Не удалось найти один из элементов UI.");
            return;
        }

        // Обновление текста UI с текущими значениями
        UpdateUI();
    }

    private void Start()
    {
        // Мы вызываем этот метод здесь для инициализации UI элементов на старте игры.
        FindAndInitializeUI();
        isNext = list.levelLoaded;
    }

    private void FixedUpdate()
    {
        UpdateUI();
        LoadNextLevel();
    }

    // Обновление UI элементов
    private void UpdateUI()
    {
        if (scoreText != null) // Проверяем, не потеряли ли мы ссылку на scoreText.
        {
            scoreText.text = "SCORE: " + score.ToString();
        }

        if (levelsText != null)
        {
            levelsText.text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex + 1).ToString();
        }
    }

    private void LoadNextLevel()
    {
        if (list.animals.Count == 0 && !isNext)
        {
            Debug.Log(SceneManager.sceneCountInBuildSettings);
            Debug.Log(SceneManager.GetActiveScene().buildIndex + 1);
            isNext = !list.levelLoaded;
            // if (SceneManager.GetActiveScene().buildIndex + 1 <= SceneManager.sceneCountInBuildSettings)
            // {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            // }
        }
    }

    private void OnDestroy()
    {
        // Отписываемся от события, чтобы избежать утечек памяти.
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}




