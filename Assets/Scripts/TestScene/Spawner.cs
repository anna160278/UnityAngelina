using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] List<GameObject> cubes;
    private int counter = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (counter < 200)
        {
            GameObject new_cube= Instantiate(cube, new Vector3(0, 2, 0), Quaternion.identity);
            counter += 1;
            cubes.Add(new_cube);
        }

        else
        {
            counter = 0;
            foreach (GameObject cube in cubes)
            {
                Destroy(cube);
            }
            
        }
    }
}
