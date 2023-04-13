using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float jumpforce;
    [SerializeField] private float speed;
    [SerializeField] private float moving;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private 

#if UNITY_EDITOR

    void OnValidate()   
    {
        rigidbody2d = player.GetComponent<Rigidbody2D>();
    }


#endif
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            rigidbody2d.velocity = Vector2.up * jumpforce;
        }

        moving = Input.GetAxis("Horizontal");

        rigidbody2d.velocity = new Vector2(speed * moving, rigidbody2d.velocity.y);
    }

}
