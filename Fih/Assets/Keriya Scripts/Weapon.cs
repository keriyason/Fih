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

    [SerializeField] private Transform player; // reference to player transform

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging && !isStabbing)
            StartCoroutine(Swing());

        if (Input.GetMouseButtonDown(1) && !isSwinging && !isStabbing)
            StartCoroutine(Stab());
    }

    private IEnumerator Swing()
    {
        isSwinging = true;

        // Base rotation aligned with player facing
        Quaternion startRot = player.rotation;
        float angle = swingLeft ? swingAngle : -swingAngle;

        // Target rotation is player facing rotated around Y axis
        Quaternion targetRot = startRot * Quaternion.AngleAxis(angle, Vector3.up);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * swingSpeed;
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }

        // Reset back to player facing
     

        swingLeft = !swingLeft;
        isSwinging = false;
    }

    private IEnumerator Stab()
    {
        isStabbing = true;

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + player.forward * stabDistance;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * stabSpeed;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * stabSpeed;
            transform.position = Vector3.Lerp(targetPos, startPos, t);
            yield return null;
        }

        transform.position = startPos;
        isStabbing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(damage);
        }
    }
}





