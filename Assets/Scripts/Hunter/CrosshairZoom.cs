using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairZoom : MonoBehaviour
{
    private Camera _camera;

    [Header("Crosshair")]
    [SerializeField] private Image zoom;
    private bool isZoom = false;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isZoom = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isZoom = false;
        }

        if (isZoom)
        {
            zoom.gameObject.SetActive(true);
            _camera.fieldOfView = 10;
        }
        else
        {
            zoom.gameObject.SetActive(false);
            _camera.fieldOfView = 60;
        }
    }
}
