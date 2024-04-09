using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManMovement : MonoBehaviour
{
    private float speed = 5f;
    private float rotationSpeed = 25f;
    private Animator animator;

    public Slider runningBar;
    public float maxRunningLevel = 100f;
    public float decreaseUnit = 0.1f;

    private float currentRunningLevel;
    public bool canRun = true;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
        currentRunningLevel = maxRunningLevel;
        runningBar.maxValue = maxRunningLevel;
        runningBar.value = currentRunningLevel;
    }

    void Update()
    {
        runningBar.value = currentRunningLevel;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && canRun)
        {
            DecreaseRunBarSlider();
            animator.SetBool("isRun", true);
            animator.SetBool("isWalk", false);
            transform.Translate(Vector3.forward * speed * 1.5f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && !canRun))
        {
            /*if (currentRunningLevel < maxRunningLevel && canRun)
            {
                Invoke("IncreaseRunBarSlider", 1f);
            }*/

            animator.SetBool("isWalk", true);
            animator.SetBool("isRun", false);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isWalk", true);
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("isLeft", true);
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("isLeft", false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("isRight", true);
            transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("isRight", false);
        }

        if (!canRun)
        {
            Invoke("IncreaseRunBarSlider", 2f);
        }

        
    }

    private void DecreaseRunBarSlider()
    {
        if (currentRunningLevel <= 0)
        {
            currentRunningLevel = 0;
            canRun = false;
        }
        else
        {
            currentRunningLevel -= decreaseUnit; //* Time.deltaTime;
            Debug.Log(currentRunningLevel);
        }
    }

    private void IncreaseRunBarSlider()
    {
        if (currentRunningLevel < maxRunningLevel)
        {
            currentRunningLevel += decreaseUnit;
            Debug.Log(currentRunningLevel);

            /*if (currentRunningLevel > 0)
            {
                canRun = true;
            }*/
        }
        else
        {
            currentRunningLevel = maxRunningLevel;
            canRun = true;
        }


    }
}
