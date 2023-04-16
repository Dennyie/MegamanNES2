using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float initialJumpForce;
    [SerializeField] private float jumpTime = 0.5f; // Tempo máximo de salto
    private float startY;
    private float moving;
    private bool isGrounded = false;
    private bool canJump = true;
    private bool hasPressedSpace = false;
    private float jumpTimeCounter = 0f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            hasPressedSpace = true;
            canJump = true;
            jumpTimeCounter = jumpTime;
            rigidbody2d.AddForce(Vector2.up * initialJumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.Space) && hasPressedSpace && canJump)
        {
            if (jumpTimeCounter > 0)
            {
                rigidbody2d.AddForce(Vector2.up * initialJumpForce * jumpTimeCounter, ForceMode2D.Force);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                canJump = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && hasPressedSpace)
        {
            canJump = false;
        }

        moving = Input.GetAxisRaw("Horizontal");

        rigidbody2d.velocity = new Vector2(speed * moving, rigidbody2d.velocity.y);

        float jumpHeight = transform.position.y - startY;
        //Debug.Log("Jump Height: " + jumpHeight);

        if (jumpHeight > startY + maxJumpHeight)
        {
            canJump = false;
        }

        if (isGrounded && !hasPressedSpace)
        {
            canJump = false;
        }

        if (!isGrounded)
        {
            hasPressedSpace = false;
        }
    }

    private void OnDrawGizmosSelected() //para mostrar o tamanho do gizmos no jogo
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
