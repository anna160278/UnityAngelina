using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour // Объявление класса CarMovement, наследующего MonoBehaviour
{
    public float speed; // Публичное поле для скорости машины
    public float maxSpeed; // Максимальная скорость вперед
    private float rotationSpeed = 100f; // Публичное поле для скорости вращения машины

    [HideInInspector]
    public float acceleration = 20f; // Значение ускорения для быстрого старта
    public float deceleration = 10f; // Значение замедления для быстрой остановки
    public float maxReverseSpeed = -20f; // Максимальная скорость назад
    public float brakeSpeed = 20f; // Торможение до полной остановки

    public float driftForce; // Уменьшенный коэффициент управления во время дрифта

    public Rigidbody rb;

    // step2 - check circle
    public GameManager manager;
    // step3 - nitro
    public float nitroAmount = 100f; // Начальное количество нитро
    public float nitroConsumption = 40f; // Сколько нитро потребляется за активацию
    public float nitroRechargeRate = 5f; // Скорость восстановления нитро

    public float nitroBoost = 2f;       // Коэффициент увеличения скорости для нитро
    public float nitroDuration = 3f;    // Продолжительность нитро
    public float nitroCooldown = 5f;    // Время на перезарядку нитро (кулдаун)

    private float nitroEndTime = 0f;    // Время окончания нитро
    private float nextNitroTime = 0f;   // Время, когда нитро снова будет доступно

    public Text textField;
    public Slider nitroSlider;

    public TrailRenderer trailLeft;
    public TrailRenderer trailRight;


    void Start()
    {
        // Инициализация Slider. Мы устанавливаем максимальное и текущее значение при старте игры
        nitroSlider.maxValue = nitroAmount;
        nitroSlider.value = nitroAmount;
    }


    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        bool isBraking = false;
        bool handbrake = Input.GetKey(KeyCode.Space);

        if (Input.GetKey(KeyCode.W))
        {
            // Ускоряемся и ограничиваем скорость максимальным значением
            speed = Mathf.MoveTowards(speed, maxSpeed, acceleration * Time.deltaTime);

            // Важно убедиться, что мы не сдвигаемся назад, пока ускоряемся вперед
            if (speed < 0)
            {
                speed = Mathf.MoveTowards(speed, 0, brakeSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (speed > 0)
            {
                // Если мы двигались вперед и нажали S, то начинаем интенсивное торможение
                isBraking = true;
                speed = Mathf.MoveTowards(speed, 0, brakeSpeed * Time.deltaTime);

            }
            else
            {
                // Как только скорость достигла 0, начинаем двигаться назад
                speed = Mathf.MoveTowards(speed, maxReverseSpeed, acceleration * Time.deltaTime);
            }
        }
        else if (!isBraking)
        {
            // Плавное замедление при отпускании клавиш
            speed = Mathf.MoveTowards(speed, 0, deceleration * Time.deltaTime);
        }


        // Обычное управление
        transform.Rotate(Vector3.up, moveHorizontal * rotationSpeed * Time.deltaTime);
        // Перемещение и вращение
        transform.Translate(Vector3.forward * speed * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            trailLeft.time = 0.5f;
            trailRight.time = 0.5f;
            rb.AddForce(transform.right * driftForce, ForceMode.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            trailLeft.time = 0;
            trailRight.time = 0;
        }

        // Обновляем текстовое поле со скоростью
        textField.text = "SPEED: " + Mathf.Abs((int)speed);

        // дрифты на пробел
        // движение без клавиши W при снижении скорости


        // step3
        // Проверяем возможность активации нитро
        if (Input.GetKeyDown(KeyCode.V) && Time.time > nextNitroTime && nitroAmount >= 100f)
        {
            ActivateNitro();
        }

        if (nitroBoost > 1) // Когда нитро активно
        {
            nitroAmount -= nitroConsumption * Time.deltaTime; // Потребляем нитро
        }
        else if (Time.time >= nextNitroTime && nitroAmount < 100f) // Когда нитро в cooldown, оно восстанавливается
        {
            nitroAmount += nitroRechargeRate * Time.deltaTime; // Восстанавливаем нитро
        }

        nitroAmount = Mathf.Clamp(nitroAmount, 0, 100f); // Убедимся, что значение нитро не выйдет за рамки
        nitroSlider.value = nitroAmount; // Обновляем значение Slider

        // Если время нитро истекло, сбрасываем скорость до обычной
        if (nitroBoost > 1 && Time.time > nextNitroTime) // Проверяем, что нитро активно (nitroBoost > 1) и время истекло
        {
            speed /= nitroBoost; // Возвращаем обычную скорость
            nitroBoost = 1; // Сбрасываем коэффициент ускорения
        }
    }

    void OnTriggerEnter(Collider other) // Не забудь добавить Collider с галочкой isTrigger
    {
        if (other.CompareTag("Block")) // Предполагаем, что у машины тег "Player"
        {
            Debug.Log("Block");
            manager.CheckpointReached(other.gameObject);
        }

        if (other.CompareTag("Finish"))
        {
            Debug.Log("Finish");
            manager.FinishLineReached(other.gameObject);
        }
    }

    void ActivateNitro()
    {
        // Проверяем, что нитро не активно уже (nitroBoost == 1) и нет в активной фазе cooldown
        if (nitroBoost == 1 && Time.time >= nextNitroTime)
        {
            nitroBoost = 2f;
            speed *= nitroBoost; // Увеличиваем скорость
            nitroAmount -= nitroConsumption;

            // Фиксируем время окончания нитро и следующего доступного использования
            nitroEndTime = Time.time + nitroDuration;
            nextNitroTime = Time.time + nitroDuration + nitroCooldown;
            
            Debug.Log("Нитро активировано!");
        }
    }

    IEnumerator timer()
    {
        for (int i = 4; i >= 0; i--)
        {
            yield return new WaitForSeconds(0.5f);
            trailLeft.time = i / 10;
            trailRight.time = i / 10;
        }
    }
}
