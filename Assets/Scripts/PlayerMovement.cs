using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    public const float G = 9.81f;
    private Rigidbody2D rb;

    [Header("Settings")]
    public float mass = 10;
    public float movementSpeed = 100;
    public bool grounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = mass;
    }
    private void FixedUpdate()
    {
        rb.AddForce(G * mass * Vector2.down);
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            rb.position += movementSpeed * Time.deltaTime * Vector2.left;
        }
        if(Input.GetKey(KeyCode.D))
        {
            rb.position += movementSpeed * Time.deltaTime * Vector2.right;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }
}