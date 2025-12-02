using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    
    public Slider healthBar; // UI Slider
    public int maxHealth = 100; //health
    private int currentHealth; //tracks players health

   
    [SerializeField] public float knockbackForce = 10f; //knockback
    private Rigidbody rb;

   
    public GameObject gameOverScreen;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get player Rigidbody

        healthBar.maxValue = maxHealth; //max health
        healthBar.value = maxHealth; 
        currentHealth = maxHealth; //track how much health the player has

        gameOverScreen.SetActive(false); // You died screen
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            if (other.GetComponent<Enemy>() == null) return;
            Debug.Log($"{gameObject.name} collided with {other.gameObject.name}");
            TakeDamage(1); //if collision with enemy player takes 1 dmg
            ApplyKnockback(other.transform); //applies knockback on collision
        }
        
    }

    private void ApplyKnockback(Transform enemy)
    {
        
        Vector3 direction = (transform.position - enemy.position).normalized; //knockback
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

        if (currentHealth <= 0) //if the players health reaches 0 players dies
        {
            Die();
        }
    }

    public void Die()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true); //on death triggers try again screen
    }
}
