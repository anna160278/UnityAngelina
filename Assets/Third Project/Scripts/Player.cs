using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float jumpForce = 10f;
    private Rigidbody rb;
    private bool isJump = false;
    private bool isGround = true;
    public bool gameOver = false;
    private Animator playerAnim;
    private ParticleSystem explosionParticle;
    public ParticleSystem jumpParticle;
    public AudioClip sound;
    private AudioSource source;
    public GameObject timer;
    public Text timerText;

    void Start()
    { 
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Transform boom = transform.Find("Explosion_Smoke");
        explosionParticle = boom.GetComponent<ParticleSystem>();
        source = GetComponent<AudioSource>();
        source.clip = sound;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            isJump = true;
        }
    }

    void FixedUpdate()
    {
        //rb.AddForce(Vector3.forward * speed, ForceMode.VelocityChange);
        if (isJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isJump = false;
            isGround = false;

            playerAnim.SetTrigger("Jump_trig");
            jumpParticle.Stop();
            source.PlayOneShot(sound, 10);
        }
    }
    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            Debug.Log("Game Over");
            explosionParticle.Play();
            gameOver = true;
            jumpParticle.Stop();

            playerAnim.SetTrigger("Death");
            //playerAnim.CrossFade("Death_01", 0.3f);
            timer.SetActive(true);
            StartCoroutine(timer_count());
            Invoke("restart", 5f);
          
        }
        if (collision.collider.tag == "Ground")
        {
            isGround = true;
            jumpParticle.Play();
        }
       
    }

    IEnumerator timer_count()
    {
        for (int i = 3; i > 0; i--)
        {
            timerText.text = "00:0" + i.ToString();
            yield return new WaitForSeconds(1);
        }
        timerText.text = "RESTART";
        yield return new WaitForSeconds(1.5f);
        timer.SetActive(false);
    }
}
