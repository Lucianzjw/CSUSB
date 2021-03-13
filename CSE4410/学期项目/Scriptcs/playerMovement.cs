using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpd = 1;
    public float JumpForce = 1;

    Rigidbody2D bod;

    private void Start()
    {
        bod = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * moveSpd;

        if (Input.GetButtonDown("Jump") && Mathf.Abs(bod.velocity.y) <0.001f)
        {
            bod.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);

        }
    }
}