using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : parent
{
    [SerializeField] private Transform Player;
    
    public NavMeshAgent agent;
public int health = 2;

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
}}