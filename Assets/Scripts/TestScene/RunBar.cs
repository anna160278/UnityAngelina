using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunBar : MonoBehaviour
{
    public Slider runningLevelSlider;
    public float maxRunningLevel = 100f;
    public float runningSpeed = 1f;
    public float decreaseSpeed = 0.1f;
    public float lerpSpeed = 1f;

    private float currentRunningLevel;

    private void Start()
    {
        currentRunningLevel = maxRunningLevel;
    }

    private void Update()
    {
        float targetRunningLevel = Input.GetKey(KeyCode.W) ? currentRunningLevel - decreaseSpeed : currentRunningLevel + runningSpeed * Time.deltaTime;
        currentRunningLevel = Mathf.Clamp(targetRunningLevel, 0, maxRunningLevel);

        // Плавно обновляем значение слайдера
        runningLevelSlider.value = Mathf.Lerp(runningLevelSlider.value, currentRunningLevel / maxRunningLevel, lerpSpeed * Time.deltaTime);
    }
}
