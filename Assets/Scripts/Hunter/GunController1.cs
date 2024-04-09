using Assets.Scripts.Hunter;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GunController1 : MonoBehaviour
{
    private float sensitivity = 100f;
    private float verticalRange = 45f;
    private float horizontalRange = 15f;


    private float verticalAngle = 45f;  // Начальный угол
    private float horizontalAngle = 15;  // Начальный угол
    private Quaternion camera_rotation;

    [Header("Crosshair")]
    public bool isZoom = false;

    private void Start()
    {
        Quaternion old_camera_rotation = camera_rotation;
        Input.ResetInputAxes();
        Cursor.lockState = CursorLockMode.Locked;   // Замыкаем курсор в центре экрана.
        Cursor.visible = false;                     // Скрываем курсор.
    }

    private void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.visible = false;                 // Убедитесь что курсор невидим.
            Cursor.lockState = CursorLockMode.Locked; // Повторно блокируем курсор.
        }


        // Получаем движение мыши
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Добавляем горизонтальное движение мыши к текущему углу
        float newRotationY = transform.localEulerAngles.y + mouseX;
        float newRotationZ = transform.localEulerAngles.y + mouseX;

        // Добавляем вертикальное движение мыши к текущему углу, с учётом ограничения
        verticalAngle -= mouseY;
        verticalAngle = Mathf.Clamp(verticalAngle, -verticalRange, verticalRange);
        horizontalAngle = Mathf.Clamp(horizontalAngle, -horizontalRange, horizontalRange);
 

        // Применяем ограничение по оси Y
        if (newRotationY > 180f)
        {
            newRotationY -= 360f;
        }

        // Применяем ограничение по оси Y
        newRotationY = Mathf.Clamp(newRotationY, -verticalRange, verticalRange);
        newRotationZ = Mathf.Clamp(newRotationZ, -horizontalRange, horizontalRange);

        // Применяем вращение
        transform.localEulerAngles = new Vector3(0f, newRotationY, newRotationZ);

        // Лочим курсор
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
