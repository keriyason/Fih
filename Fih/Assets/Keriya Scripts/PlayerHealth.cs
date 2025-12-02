using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public Slider healthBar;
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Knockback Settings")]
    [SerializeField] public float knockbackForce = 10f;
    private Rigidbody rb;

    [Header("UI")]
    public GameObject gameOverScreen;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get player Rigidbody

        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        currentHealth = maxHealth;

        gameOverScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            // Apply damage
            TakeDamage(1); // or pass in enemy damage value

            // Apply knockback
            ApplyKnockback(other.transform);
        }
    }

    private void ApplyKnockback(Transform enemy)
    {
        // Push player away from enemy
        Vector3 direction = (transform.position - enemy.position).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode.Impulse);
    }

    public void Heal(int value)
    {
        currentHealth += value;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.value = currentHealth;
    }

    public void TakeDamage(int value)
    {
        currentHealth -= value;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
}
