using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Quit : MonoBehaviour
{

    public void GameQuit()
    {
        Application.Quit();
        Debug.Log("quit");
        
    }
}

