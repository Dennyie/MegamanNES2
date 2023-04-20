using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private float initialJumpForce;
    [SerializeField] private float maxJumpForce;
    private float accumulatedJumpForce;
    private float moving;

    private void Start()
    {
        accumulatedJumpForce = initialJumpForce;
    }

    void Update()
    {
        
      /*  if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.AddForce(Vector2.up * initialJumpForce, ForceMode2D.Impulse);
        } */

        if (Input.GetKey(KeyCode.Space))
        {         
            if (accumulatedJumpForce < maxJumpForce)
            {
                Debug.Log(accumulatedJumpForce);
                rigidbody2d.AddForce(Vector2.up * accumulatedJumpForce, ForceMode2D.Impulse);
                accumulatedJumpForce += 0.1f;
                
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            accumulatedJumpForce = initialJumpForce;
            Debug.Log(accumulatedJumpForce);
        }

        moving = Input.GetAxisRaw("Horizontal");

        rigidbody2d.velocity = new Vector2(speed * moving, rigidbody2d.velocity.y);

        
    }
}

