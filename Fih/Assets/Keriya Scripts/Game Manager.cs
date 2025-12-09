using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemiesLeftText;
    List<Enemy> enemies = new List<Enemy>();

    [SerializeField] private GameObject wallRemove;
    [SerializeField] private GameObject wallParticles;

    private void OnEnable()
    {
        Enemy.OnEnemyKilled += HandleEnemyDefeated;
    }
    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= HandleEnemyDefeated;
    }
    private void Awake()
    {
        enemies = GameObject.FindObjectsOfType<Enemy>().ToList(); // How many enemies left
        UpdateEnemiesLeftText();
    }

    void HandleEnemyDefeated(Enemy enemy)
    {
        if (enemies.Remove(enemy)) // Removes Enemy from list on kill
        {
            UpdateEnemiesLeftText();
            if (enemies.Count ==0)
            {
                UnlockNextLevel();
            }
        }
    }
    void UpdateEnemiesLeftText()
    {
        enemiesLeftText.text = $"Enemies Left: {enemies.Count}"; // Updates Text with Enemies Remaining
    }
    private void UnlockNextLevel()
    {
        Vector3 wallPos = wallRemove.transform.position;

        // Destroy or disable the wall
        Destroy(wallRemove);

        // Spawn particles at wall position
        if (wallParticles != null)
        {
            GameObject particles = Instantiate(wallParticles, wallPos, Quaternion.identity);
            Destroy(particles, 3f); // auto-clean after 3 seconds
        }

        Debug.Log("Enemies have been defeated. Unlocked");
    }

}


