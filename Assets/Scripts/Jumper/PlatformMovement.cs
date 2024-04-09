using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float speed = 2f; // Скорость перемещения платформы

    private PlatformShake platformShake; // Ссылка на скрипт PlatformShake

    void Start()
    {
        platformShake = GetComponent<PlatformShake>(); // Получаем ссылку на скрипт PlatformShake
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime); // Перемещение платформы вверх
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            platformShake.Shake(); // Вызываем функцию тряски платформы
        }
    }
}