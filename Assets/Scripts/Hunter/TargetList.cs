using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetList : MonoBehaviour
{
    [SerializeField] public List<GameObject> animals;
    [SerializeField] public bool levelLoaded;
    //private GameManagerTest gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManagerTest>();
       // Debug.Log("Animals: " + animals.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
