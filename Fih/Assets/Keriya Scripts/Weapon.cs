using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 1;
    public float swingAngle = 90f;
    public float swingSpeed = 10f;

    public float stabDistance = 5f;   // how far forward the stab goes
    public float stabSpeed = 15f;     // speed of stab

    private bool swingLeft = true;
    private bool isSwinging = false;
    private bool isStabbing = false;

    private Quaternion startRotation;
    private Quaternion targetRotation;
    private Vector3 startPosition;

    private void Start()
    {
        startRotation = transform.localRotation;
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        // Left click = swing
        if (Input.GetMouseButtonDown(0) && !isSwinging && !isStabbing)
        {
            StartCoroutine(Swing());
        }

        // Right click = stab
        if (Input.GetMouseButtonDown(1) && !isSwinging && !isStabbing)
        {
            StartCoroutine(Stab());
        }
    }

    private System.Collections.IEnumerator Swing()
    {
        isSwinging = true;

        float angle = swingLeft ? swingAngle : -swingAngle;
        targetRotation = Quaternion.Euler(0f, angle, 0f) * startRotation;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * swingSpeed;
            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        transform.localRotation = startRotation;

        swingLeft = !swingLeft;
        isSwinging = false;
    }

    private System.Collections.IEnumerator Stab()
    {
        isStabbing = true;

        Vector3 targetPosition = startPosition + transform.forward * stabDistance;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * stabSpeed;
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        // Return to original position
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * stabSpeed;
            transform.localPosition = Vector3.Lerp(targetPosition, startPosition, t);
            yield return null;
        }

        transform.localPosition = startPosition;
        isStabbing = false;
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




