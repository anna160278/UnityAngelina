using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShake : MonoBehaviour
{
    public float shakeIntensity = 0.1f; // Интенсивность тряски платформы
    public float shakeDuration = 0.5f; // Длительность тряски платформы
    public float fallForce = 100f; // Сила, с которой платформа будет отваливаться

    private bool shaking = false; // Флаг, указывающий, трясется ли уже платформа

    private Vector3 initialPosition; // Исходная позиция платформы

    void Start()
    {
        initialPosition = transform.position; // Сохраняем исходную позицию платформы
    }

    void Update()
    {
        if (shaking)
        {
            // Генерируем случайное смещение в пределах интенсивности тряски
            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;

            // Применяем смещение к позиции платформы
            transform.position = initialPosition + shakeOffset;

            // Уменьшаем длительность тряски с каждым обновлением
            shakeDuration -= Time.deltaTime;

            // Если тряска закончилась, отключаем тряску и отваливаем платформу
            if (shakeDuration <= 0f)
            {
                shaking = false;
                Fall();
            }
        }
    }

    void Fall()
    {
        // Отключаем коллайдер платформы
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = false;

        // Применяем силу, чтобы платформа отвалилась
        GetComponent<Rigidbody>().AddForce(Vector3.down * fallForce);
    }

    public void Shake()
    {
        if (!shaking)
        {
            shaking = true;
            initialPosition = transform.position; // Сохраняем текущую позицию платформы перед тряской
        }
    }
}