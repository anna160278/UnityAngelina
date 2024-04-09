using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRunner : MonoBehaviour
{
    public float speed;
    private float height = 0.5f;
    private float low = 0.5f;
    public GameManager2 gm;

    private float jumpForce = 10f;
    private Rigidbody rb;
  //  private bool isJump = false;
    public bool isGround = true;
  

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.A))
        { transform.Translate(Vector3.left * Time.deltaTime * speed); }

        if (Input.GetKey(KeyCode.D))
        { transform.Translate(Vector3.right * Time.deltaTime * speed); }

        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGround = false;
        }

        gm.updateLifeUI();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Height"))
        {
            transform.localScale += new Vector3(height, height, height);
        }

        if (other.CompareTag("Low"))
        {
            transform.localScale -= new Vector3(low, low, low);
        }

        if (other.CompareTag("Coin"))
        {
            gm.score++;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Health") && gm.lives < 3)
        {
            gm.lives++;

            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
        }*/

        if (collision.collider.tag == "Ground")
        {
            isGround = true;
        }


        if (collision.collider.tag == "Obstacle")
        {
            gm.lives--;

            if (gm.lives <= 0)
            {
                gm.game_over();
            }
        }
    }
}






/*private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Height"))
    {
        transform.localScale += new Vector3(height, height, height);
    }

    if (collision.gameObject.CompareTag("Low"))
    {
        transform.localScale -= new Vector3(low, low, low);
    }
}*/