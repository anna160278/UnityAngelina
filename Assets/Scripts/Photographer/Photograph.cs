using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Photograph : MonoBehaviour
{
    public float speed;
    //public GameManager2 gm;
    private float rotateSpeed = 70f;
    private Rigidbody rb;
  

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) 
        { transform.Translate(Vector3.forward * Time.deltaTime * speed); }

        if (Input.GetKey(KeyCode.S))
        { transform.Translate(Vector3.back * Time.deltaTime * speed); }

        if (Input.GetKey(KeyCode.D))
        { transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed); }

        if (Input.GetKey(KeyCode.A))
        { transform.Rotate(Vector3.down * Time.deltaTime * rotateSpeed); }
    }
}