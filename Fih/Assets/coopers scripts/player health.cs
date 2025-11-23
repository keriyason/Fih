using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerhealth : parent
{ public int health = 3;
[SerializeField] public float knockbackForce = 10f;

private Rigidbody rb;

 private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get player Rigidbody
    }

      private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            health--;
            Debug.Log("player hit Health:" + health);
            
              ApplyKnockback(other.transform);
          
            if (health <= 0)
            {
                Destroy(gameObject);
            Debug.Log("player killed");
            }


        }
    }
    private void ApplyKnockback(Transform enemy)
    { 
         Vector3 direction = (transform.position - enemy.position).normalized;

    rb.AddForce(direction * knockbackForce , ForceMode.Impulse);
}
}