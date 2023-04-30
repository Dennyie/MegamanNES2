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
    [SerializeField] private bool intendToJump = false;
    [SerializeField] private bool didJump = false;
    //[SerializeField] private bool canJump;
    [SerializeField] private float accumulatedJumpForce;
    [SerializeField] private float forceToAccumulate;
    private float moving;


    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        accumulatedJumpForce = initialJumpForce; 
    }

    void Update()
    {
        // utilizado somente para verificar se o IsGrounded está sendo ativado//
        /*if (IsGrounded())
        {
            canJump = true;
        }
        if (!IsGrounded())
        {
            canJump = false;
        } */

        if (Input.GetKeyDown(KeyCode.Space))
        {
            intendToJump = true;
        }
        

        if (intendToJump && IsGrounded()) 
        {
            InitialJump();
        }

        PlayerMove();    
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            didJump = false;
            intendToJump = false;
            accumulatedJumpForce = initialJumpForce;
        }
    }

    void InitialJump()
    {
        rigidbody2d.velocity = Vector2.up * initialJumpForce;
        didJump = true;
    }

    void PlayerMove()
    {
        
        if (didJump && Input.GetKey(KeyCode.Space) && !IsGrounded() )
        {
            if (accumulatedJumpForce < maxJumpForce)
            {
                Debug.Log(accumulatedJumpForce);
                rigidbody2d.AddForce(Vector2.up * accumulatedJumpForce, ForceMode2D.Impulse);
                accumulatedJumpForce += forceToAccumulate;
            }
        }

        if (rigidbody2d.velocity.y <= 0)
        {
            didJump = false;
            intendToJump = false;
        }

        if (IsGrounded())
        {
            accumulatedJumpForce = initialJumpForce;
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

