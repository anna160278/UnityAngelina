using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameManagerSpace : MonoBehaviour
{
    public List<GameObject> rings;
    public TMP_Text ringsText;
    private int ringIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject points = GameObject.Find("Rings");

        foreach (Transform ring in points.transform)
        {
            rings.Add(ring.gameObject);
        }

        ringsText.text = ringIndex.ToString() + "/" + rings.Count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ringsText.text = ringIndex.ToString() + "/" + rings.Count.ToString();
    }

    public void RingReached(GameObject point)
    {
        if (rings[ringIndex] == point)
        {
            if (ringIndex != rings.Count - 1)
            {
                ringIndex++;
                rings[ringIndex].SetActive(true);
            }    
            else
            {
                ringIndex++;
                ringsText.text = ringIndex.ToString() + "/" + rings.Count.ToString();
            }
            
        }    
    }
}
