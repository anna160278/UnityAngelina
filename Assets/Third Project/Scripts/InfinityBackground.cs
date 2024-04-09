using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityBackground : MonoBehaviour
{
    private float width;
    private float speed = 20;
    private Vector3 startPosition;
    [SerializeField] Player playerScript;

    void Start()
    {
        startPosition = transform.position;
        width = GetComponent<BoxCollider>().size.x / 2;
    }


    void Update()
    {
        if (!playerScript.gameOver)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);

            if (transform.position.z <= startPosition.z - width)
            {
                transform.position = startPosition;
            }
        }
    }
}
