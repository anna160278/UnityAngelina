using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;
    [SerializeField] Player playerScript;
    private int obstIndex;

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 2, 1.5f);
    }

    void SpawnObstacle()
    {
        if (!playerScript.gameOver)
        {
            Vector3 pos = new Vector3(40, 0, 0);
            obstIndex = Random.Range(0, obstacles.Length);
            Instantiate(obstacles[obstIndex], pos, obstacles[obstIndex].transform.rotation);
        }
    }
}
