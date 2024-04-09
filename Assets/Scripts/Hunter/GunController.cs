using Assets.Scripts.Hunter;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class GunController : MonoBehaviour
{
    private float sensitivity = 100f;
    public float verticalRange = 90f;

    private Quaternion gunRotation;
    private float verticalAngle = 45f;  // Начальный угол

    private Camera _camera;
    private Transform _spawnPoint;
    private Quaternion camera_rotation;

    [Header("Crosshair")]
    public bool isZoom = false;

    private void Start()
    {
        Quaternion old_camera_rotation = camera_rotation;
        Input.ResetInputAxes();
        Cursor.lockState = CursorLockMode.Locked;   // Замыкаем курсор в центре экрана.
        Cursor.visible = false;                     // Скрываем курсор.

        gunRotation = Quaternion.identity;          // Устанавливаем вращение ружья по умолчанию.
        transform.localRotation = gunRotation;      // Применяем начальное вращение.

        
        transform.position = new Vector3(-115.569f, 14.0220003f, 159.243057f);

        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        camera_rotation = new Quaternion(0f, 0.707106829f, 0, 0.707106829f);
        _spawnPoint = GetComponent<BulletSpawner>().spawn;
    }

    private void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.visible = false;                 // Убедитесь что курсор невидим.
            Cursor.lockState = CursorLockMode.Locked; // Повторно блокируем курсор.
        }

        if (Input.GetMouseButton(1))
        {
            isZoom = true;
            Quaternion rotation = Quaternion.LookRotation(_spawnPoint.forward);
            Vector3 eulerRotation = rotation.eulerAngles;
            eulerRotation.x += 5f; // Отрицательное значение для смещения 
            rotation = Quaternion.Euler(eulerRotation);
            _camera.transform.rotation = rotation;
        }
        else
        {
            isZoom = false;
            camera_rotation = new Quaternion(0f, 0, 0, 0);

            // Получаем движение мыши
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            // Добавляем горизонтальное движение мыши к текущему углу
            float newRotationY = transform.localEulerAngles.y + mouseX;

            // Добавляем вертикальное движение мыши к текущему углу, с учётом ограничения
            verticalAngle -= mouseY;
            verticalAngle = Mathf.Clamp(verticalAngle, -verticalRange, verticalRange);

            // Применяем ограничение по оси Y
            if (newRotationY > 180f)
            {
                newRotationY -= 360f;
            }

            // Применяем ограничение по оси Y
            newRotationY = Mathf.Clamp(newRotationY, -45f, 45f);

            // Применяем вращение
            transform.localEulerAngles = new Vector3(0f, newRotationY, verticalAngle);

            // Лочим курсор
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        if (isZoom)
        {
            _camera.fieldOfView = 20;
        }
        else
        {
            _camera.fieldOfView = 60;
        }
    }
}

/*private float horizontal;
    private float vertical;
    private float sensitivity = 2f;

    void Start()
    {
        
    }

 
    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float vertical = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        horizontal = Input.GetAxis("Mouse X") * sensitivity;
        vertical = Input.GetAxis("Mouse Y") * sensitivity;

        transform.Rotate(0, horizontal, 0);
        transform.Rotate(0, 0, vertical);
    }
 
 */

/*
  // Получаем движение мыши
 float horizontal = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
 float vertical = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

 // Добавляем движение мыши к текущему углу
 verticalAngle += vertical;
 verticalAngle = Mathf.Clamp(verticalAngle, -verticalRange, verticalRange); // Ограничение углов в пределах диапазона

 // Вращение ружья
 // Горизонт (Y)
 gunRotation *= Quaternion.Euler(0f, horizontal, 0f);
 // Вертикал (Х) с ограничениями
 gunRotation = Quaternion.Euler(verticalAngle, gunRotation.eulerAngles.y, 0f);

 // Применяем вращение - это указатель на объект ружья
 transform.localRotation = gunRotation;
 */
