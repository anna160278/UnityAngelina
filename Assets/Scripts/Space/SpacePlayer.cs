using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePlayer : MonoBehaviour
{

    private float speed = 10;
    private float rotationSpeed = 50f;
    private float rotateAngle;

    private GameManagerSpace gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManagerSpace>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rotateAngle = Mathf.Clamp(rotateAngle - rotationSpeed * Time.deltaTime, -20, 20);
            transform.localEulerAngles = new Vector3(0f, 0f, rotateAngle);
            transform.Rotate(Vector3.forward, rotateAngle * Time.deltaTime);
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rotateAngle = Mathf.Clamp(rotateAngle + rotationSpeed * Time.deltaTime, -20, 20);
            transform.localEulerAngles = new Vector3(0f, 0f, rotateAngle);
            transform.Rotate(Vector3.forward, rotateAngle * Time.deltaTime);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ring"))
        {
            other.gameObject.SetActive(false);
            gm.RingReached(other.gameObject);
        }
    }
}
