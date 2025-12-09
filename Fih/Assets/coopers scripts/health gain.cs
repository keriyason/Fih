using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthgain : MonoBehaviour
{
 public List<GameObject> enemiesInside = new List<GameObject>();
    public int enemyCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            if (!enemiesInside.Contains(other.gameObject))
            {
                enemiesInside.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            enemiesInside.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        // Clean up dead enemies (destroyed = null)
        enemiesInside.RemoveAll(e => e == null);

        // Update the public count value
        enemyCount = enemiesInside.Count;
    }
}