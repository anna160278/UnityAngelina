using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public Test test;


    void Start()
    {
        test.apples = 150;
        Debug.Log(test.apples);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
