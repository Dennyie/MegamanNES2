using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private GameObject shot;
    [SerializeField] private Rigidbody2D shotRb;
    [SerializeField] private float initialJumpForce;
    [SerializeField] private float maxJumpForce;
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float maxDistance;
    [SerializeField] private bool intendToJump = false;
    [SerializeField] private bool didJump = false;
    [SerializeField] private Animator animator;
    //[SerializeField] private bool canJump;
    [SerializeField] private float accumulatedJumpForce;
    [SerializeField] private float forceToAccumulate;
    [SerializeField] private GameObject [] bullets;
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

        Animating();

        if (Input.GetKeyDown(KeyCode.Space)) // A intenção de pular é sempre ativada quando o jogador aperta a barra de espaço
        {
            intendToJump = true;
        }
        

        if (intendToJump && IsGrounded()) // Para que o pulo somente ocorra caso o a intenção de pular ocorra enquanto o jogador está no chão
        {
            InitialJump();
        }

        PlayerMove();    
        
        if (Input.GetKeyUp(KeyCode.Space)) // Se o jogador soltar a barra de espaço ele desativa as variáveis que permitem o pulo e reseta o acumulo de força
        {
            didJump = false;
            intendToJump = false;
            accumulatedJumpForce = initialJumpForce;
        }

        if (Input.GetAxisRaw("Horizontal") > 0) // Para que o jogador sempre vire para direita quando o GetAxis for maior que 0
        {
            transform.localScale = new Vector3(1.5f, transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) // Para que o jogador sempre vire a esquerda quando o GetAxis for menor que 0
        {
            transform.localScale = new Vector3(-1.5f, transform.localScale.y, transform.localScale.z); 
        }

        TryintToShoot();
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
            if (accumulatedJumpForce < maxJumpForce) // Para deixar sempre que o accumulatedJumpForce acumule até o máximo que definimos no maxJumpForce
            {
                //Debug.Log(accumulatedJumpForce); // Debug para mostrar no console a força que está sendo acumulada para ajudar na hora da calibrar o pulo
                rigidbody2d.AddForce(Vector2.up * accumulatedJumpForce, ForceMode2D.Impulse);
                accumulatedJumpForce += forceToAccumulate;
            }
        }

        if (rigidbody2d.velocity.y <= 0) // Se o jogador estiver caindo irá impedir ele de pular novamente deixando as váriaveis abaixo como falsas
        {
            didJump = false;
            intendToJump = false;
        }

        if (IsGrounded()) // Se o jogador está no chão o acumulo de força do pulo irá voltar para o InitualJumpForce
        {
            accumulatedJumpForce = initialJumpForce;
        } 

        moving = Input.GetAxisRaw("Horizontal"); // responsável pela movimentação do jogador

        rigidbody2d.velocity = new Vector2(speed * moving, rigidbody2d.velocity.y); // funciona com o Speed que pode ser alterado o valor no Inspector para correr mais rápido ou mais devagar
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }

    private bool IsGrounded() // Responsável por verificar por meio de um BoxCast se o jogador está tocando na layer Ground
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

    private void Animating()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 ) // Verificando se a o jogador não está parado para ativar a booleana de controla da animação para verdadeiro
        {
            animator.SetBool("IsRunning", true);
            //Debug.Log("TA CORRENDO!");
        }
        else 
        {
            animator.SetBool("IsRunning", false);
            //Debug.Log("NAO TA CORRENDO!");
        }

        if (!IsGrounded()) // Verificando se o jogador está fora do chão para fazer a animação de pulo
        {
            animator.SetBool("InAir", true);
        }
        else
        {
            animator.SetBool("InAir", false);
        }
    }

    void TryintToShoot()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            
        }
    }
}

