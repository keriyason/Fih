using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class ImageTransitions : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> scenes;

    GameObject currentscene;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        currentscene = scenes[index];

        
        InvokeRepeating("SetActive", 5f, 5);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (currentscene.activeInHierarchy && index < scenes.Count - 1)
        {
            index++;
            currentscene = scenes[index];
        }
    }
    
    void SetActive()
    {
        currentscene.SetActive(true);
    }
}
