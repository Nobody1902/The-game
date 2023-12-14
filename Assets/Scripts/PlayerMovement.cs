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
    public float GravityScale = 1;
    public float Mass = 10;
    [Range(0.0f, 10.0f)]
    public float MovementSpeed = 5;
    [Range(0.0f, 5.0f)]
    public float SprintMultiplyer = 2.0f;
    [Range(0.0f, 10.0f)]
    public float AirSpeed = 2.5f;
    [Range(0f, 1f)]
    public float FrictionMultiplyer = 0.1f;
    public float JumpHeight = 500;
    public LayerMask GroundLayer;
    [Range(0, 0.5f)]
    public float ErrorMarigin = 0.05f;

    [Header("Debug"), SerializeField]
    private bool Grounded = false;
    [SerializeField]
    private bool WallLeft = false;
    [SerializeField]
    private bool WallRight = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = Mass * 0.7f;
    }
    private void FixedUpdate()
    {
        bool left = Physics2D.Raycast(transform.position + transform.right * transform.localScale.x / 2, Vector2.down, transform.localScale.y/2 + ErrorMarigin, GroundLayer).collider != null;
        bool right = Physics2D.Raycast(transform.position - transform.right * transform.localScale.x / 2, Vector2.down, transform.localScale.y / 2 + ErrorMarigin, GroundLayer).collider != null;

        if (left || right)
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }

        bool leftCenter = Physics2D.Raycast(transform.position, Vector2.left, transform.localScale.x / 2 + ErrorMarigin, GroundLayer).collider != null;
        bool rightCenter = Physics2D.Raycast(transform.position, Vector2.right, transform.localScale.x / 2 + ErrorMarigin, GroundLayer).collider != null;
        bool leftDown = Physics2D.Raycast(transform.position - transform.up * transform.localScale.y / 2, Vector2.left, transform.localScale.x / 2 + ErrorMarigin, GroundLayer).collider != null;
        bool rightDown = Physics2D.Raycast(transform.position - transform.up * transform.localScale.y / 2, Vector2.right, transform.localScale.x / 2 + ErrorMarigin, GroundLayer).collider != null;
        bool leftUp = Physics2D.Raycast(transform.position + transform.up * transform.localScale.y / 2, Vector2.left, transform.localScale.x / 2 + ErrorMarigin, GroundLayer).collider != null;
        bool rightUp = Physics2D.Raycast(transform.position + transform.up * transform.localScale.y / 2, Vector2.right, transform.localScale.x / 2 + ErrorMarigin, GroundLayer).collider != null;

        WallLeft = leftCenter || leftDown || leftUp;
        WallRight = rightCenter || rightDown || rightUp;

        // Apply gravity
        if (!Grounded)
        {
            rb.AddForce(GravityScale * G * Mass * Vector2.down, ForceMode2D.Force);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }
    private void Update()
    {
        float speed = Grounded ? MovementSpeed : AirSpeed;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            // TODO: Add some sprint timer
            speed *= SprintMultiplyer;
        }

        if (!WallLeft && Input.GetKey(KeyCode.A))
        {
            rb.rotation = 180;
            rb.position += speed * Time.deltaTime * Vector2.left;
        }
        if(!WallRight && Input.GetKey(KeyCode.D))
        {
            rb.rotation = 0;
            rb.position += speed * Time.deltaTime * Vector2.right;
        }

        if(Grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(JumpHeight * Vector2.up, ForceMode2D.Force);
        }
    }
}