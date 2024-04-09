using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public Vector3 minPosition;
    public Vector3 maxPosition;

    void Start()
    {
        // Создаем объект в случайных координатах
        Vector3 randomPosition = new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y),
            Random.Range(minPosition.z, maxPosition.z)
        );
        Instantiate(objectPrefab, randomPosition, Quaternion.identity);
    }
}