using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject viewer;
    [SerializeField] private GameObject screenshot;
    [SerializeField] private RawImage screenshotImage;


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Animal"))
                {
                    Debug.Log("Animal");
                    point.SetActive(true);
                    viewer.SetActive(false);

                    StartCoroutine(offLight(0.5f));
                    string filename = "screenshot.png";
                    ScreenCapture.CaptureScreenshot(filename);
                    Debug.Log("Screenshot captured: " + filename);

                    Texture2D texture = new Texture2D(1, 1);
                    texture.LoadImage(System.IO.File.ReadAllBytes(filename));
                    screenshotImage.texture = texture;
                    StartCoroutine(offPhoto(3f));
                }
            }
        }
    }

    IEnumerator offLight(float deltaTime)
    {
        yield return new WaitForSeconds(deltaTime);
        point.SetActive(false);
        viewer.SetActive(true);
        screenshot.SetActive(true);
    }

    IEnumerator offPhoto(float deltaTime)
    {
        yield return new WaitForSeconds(deltaTime);
        screenshot.SetActive(false);
    }
}
