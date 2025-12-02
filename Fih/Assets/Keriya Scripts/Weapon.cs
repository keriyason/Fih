using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 1; // dmg of weapin
    public float swingAngle = 90f; // how far the swing will go
    public float swingSpeed = 10f; // how fast the swing is

    public float stabDistance = 5f;   // how far forward the stab goes
    public float stabSpeed = 15f;     // speed of stab

    private bool swingLeft = true;
    private bool isSwinging = false;
    private bool isStabbing = false;

    private Quaternion startRotation; //rotation of swing
    private Quaternion targetRotation;
    private Vector3 startPosition;

    private void Start()
    {
        startRotation = transform.localRotation;
        startPosition = transform.localPosition;
    }

    private void Update()
    {
       
        if (Input.GetMouseButtonDown(0) && !isSwinging && !isStabbing)  // left click = swing
        {
            StartCoroutine(Swing());
        }

        
        if (Input.GetMouseButtonDown(1) && !isSwinging && !isStabbing) // right click = stab
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




