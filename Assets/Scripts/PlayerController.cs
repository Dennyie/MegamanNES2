using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private float initialJumpForce;
    [SerializeField] private float maxJumpForce;
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float maxDistance;
    [SerializeField] private bool canJump = true;
    private float accumulatedJumpForce;
    private float moving;


    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        accumulatedJumpForce = initialJumpForce;
        canJump = true;
    }

    void Update()
    {
        PlayerMove();                  
    }
    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.Space) && IsGrounded() && canJump)
        {
            if (accumulatedJumpForce < maxJumpForce)
            {
                Debug.Log(accumulatedJumpForce);
                rigidbody2d.AddForce(Vector2.up * accumulatedJumpForce, ForceMode2D.Impulse);
                accumulatedJumpForce += 0.5f;
            }
        if (rigidbody2d.velocity.y < 0)
            {
                canJump = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            accumulatedJumpForce = initialJumpForce;
            Debug.Log(accumulatedJumpForce);
            canJump = true;
        }

        moving = Input.GetAxisRaw("Horizontal");

        rigidbody2d.velocity = new Vector2(speed * moving, rigidbody2d.velocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }

    private bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

