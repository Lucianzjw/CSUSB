using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D bod;
    public float spd;
    public float randomUpSpd;

    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        int dir = Random.Range(0, 2);
        bod.AddForce(Vector2.up * Random.Range(-randomUpSpd, randomUpSpd));

    }

    private void OnEnable()
    {
        bod.AddForce(Vector2.right * spd);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(10);
        }
        Destroy(gameObject);
    }
}
