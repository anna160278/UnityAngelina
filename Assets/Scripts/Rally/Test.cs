using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    public int apples;
    private float speed = 15.45f;
    private GameObject panel;

    //public int health = 15;

    //private int maxHealth = 100;
    //private int number = 5;
    //private float number2 = 5.5f; // 1.2f, 2.5, 
    //private string text = "Hello world";
    //private char letter = 'D';
    //private bool isBool = true;

    //private GameObject player;
    //private Transform player1;


    void Start()
    {
        if (SceneManager.GetActiveScene().name == "RoadsCity")
        {
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            panel = canvas.transform.Find("Panel").gameObject;
            Debug.Log(panel.name);
        }

    }


    void Update()
    {

    }
}
