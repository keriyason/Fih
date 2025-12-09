using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : parent
{
    [SerializeField] private Transform Player;

    public NavMeshAgent agent;
    public int health = 2;

    public static event Action<Enemy> OnEnemyKilled;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (Player != null)
        {
            agent.SetDestination(Player.position);
        }
        else
        {
            agent.ResetPath();
            Debug.Log("enemy at rest");

        }

    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnEnemyKilled?.Invoke(this); //calls to the ui 
        Destroy(gameObject);
    }
}