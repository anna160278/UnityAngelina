using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float force;
    private Rigidbody rb;

    private float verticalInput;
    private float horizontalInput;
    public GameObject focalPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 moveVector = (focalPoint.transform.forward * verticalInput).normalized;
        transform.Translate(moveVector * force * Time.deltaTime, Space.World);
        //rb.AddForce(moveVector * force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Debug.Log("Player collided with " + collision.gameObject);
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (transform.position - collision.gameObject.transform.position).normalized;
            enemyRb.AddForce(awayFromPlayer, ForceMode.Impulse);
        }
    }
}
