using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPivot : MonoBehaviour
{
    public int damage = 1;
    public float swingAngle = 90f;
    public float swingSpeed = 10f;

    private bool isSwinging = false;
    private Quaternion startRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        startRotation = transform.localRotation; // pivot rotation
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            StartCoroutine(Swing());
        }
    }

    private System.Collections.IEnumerator Swing()
    {
        isSwinging = true;

        targetRotation = Quaternion.Euler(0f, swingAngle, 0f) * startRotation;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * swingSpeed;
            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        transform.localRotation = startRotation;
        isSwinging = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
