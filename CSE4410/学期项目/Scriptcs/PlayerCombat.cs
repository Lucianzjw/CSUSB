using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Sword
    public Animator anim;
    public Transform swordAttPoint;
    public float swordAttRange;
    public LayerMask enemyLayers;
    public float SwordAttRate = 2f;
    float SwordNextAttTime = 0f;

    //Laser
    public Transform LaserfirePoint;
    public LineRenderer lineRenderer;
    public GameObject Fxef;

    //Shotgun
    public GameObject ball;
    public GameObject FireEffect;
    public Transform ShotgunfirePoint;
    public float ShotgunFireRate = 2f;
    float ShotgunNextFireTime = 0f;
    public float BallNumber;

    void Update()
    {
        //three attack modes of the Sword
        if (Time.time >= SwordNextAttTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                SwordAttackStab();
                SwordNextAttTime = Time.time + 1f / SwordAttRate;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                SwordAttackChop();
                SwordNextAttTime = Time.time + 2f / SwordAttRate;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                SwordAttackUpward();
                SwordNextAttTime = Time.time + 3f / SwordAttRate;
            }
        }

        //Laser
        if (Input.GetKeyDown(KeyCode.U))
        {
            lineRenderer.enabled = true;
            shotline();

        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            lineRenderer.enabled = false;
            Fxef.SetActive(false);
        }


        //shotgun
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Time.time >= ShotgunNextFireTime)
            {
                shotgun();
                FireEffect.transform.position = ShotgunfirePoint.position;
                FireEffect.SetActive(true);
                ShotgunNextFireTime = Time.time + 1f / ShotgunFireRate;
            }
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            FireEffect.SetActive(false);
        }
    }
    void SwordAttackStab()
    {
        anim.SetTrigger("SwordStab");

        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(swordAttPoint.position, swordAttRange, enemyLayers);
        foreach (Collider2D enemy in hitInfo)
        {
            enemy.GetComponent<Enemy>().TakeDamage(30);
        }
    }
    void SwordAttackChop()
    {
        anim.SetTrigger("SwordChop");

        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(swordAttPoint.position, swordAttRange, enemyLayers);
        foreach (Collider2D enemy in hitInfo)
        {
            enemy.GetComponent<Enemy>().TakeDamage(40);
        }
    }
    void SwordAttackUpward()
    {
        anim.SetTrigger("SwordUpward");

        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(swordAttPoint.position, swordAttRange, enemyLayers);
        foreach (Collider2D enemy in hitInfo)
        {
            enemy.GetComponent<Enemy>().TakeDamage(50);
        }
    }
    //Draw Sword Range
    void OnDrawGizmosSelected()
    {
        if (swordAttPoint == null) return;

        Gizmos.DrawWireSphere(swordAttPoint.position, swordAttRange);
    }

    void shotline()
    {
        anim.SetTrigger("LaserFire");
        RaycastHit2D hit;
        hit = Physics2D.Raycast(LaserfirePoint.position, LaserfirePoint.right);
        //Hitting the target generates damage
        if (hit && lineRenderer.enabled == true)
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(10);
            }
            //Laser startposition
            lineRenderer.SetPosition(0, LaserfirePoint.position);
            //Laser endposition
            lineRenderer.SetPosition(1, hit.point);
            Fxef.transform.position = hit.point;
            Fxef.SetActive(true);
        }
        //Failure to hit generates a ray of fixed length
        else
        {
            lineRenderer.SetPosition(0, LaserfirePoint.position);
            lineRenderer.SetPosition(1, LaserfirePoint.position + LaserfirePoint.right * 100);
        }
    }


    void shotgun()
    {
        anim.SetTrigger("ShotgunFire");
        for (int i = 0; i < BallNumber; i++)
        {
            Instantiate(ball, ShotgunfirePoint.position, Quaternion.identity);
        }
    }
}
