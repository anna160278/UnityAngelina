using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    [SerializeField] private float _height;
    [SerializeField] private float _coins;
    [SerializeField] private string _name;
    [SerializeField] private Color _bodyColor;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private bool _isAlive;

    [SerializeField] private Light _sun;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _ball;


    void Start()
    {
        transform.localScale = new Vector3(1, _height, 1);
        gameObject.name = _name;
        gameObject.GetComponent<Renderer>().material.color = _bodyColor;
        transform.position = _startPosition;
        gameObject.SetActive(_isAlive);

        _sun.intensity = 2;
        _sun.color = Color.black;

        _camera.fieldOfView = 100;

        _ball.GetComponent<Renderer>().material.color = Color.yellow;
    }

  
    void Update()
    {
        _ball.position = transform.position + new Vector3(0, 1, 0);
    }
}
