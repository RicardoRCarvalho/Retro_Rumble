using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigoBasic : MonoBehaviour
{

   

    [Header("Player Component References")]
    [SerializeField] Rigidbody2D rb;
    [Header("Player Settings")]
    [SerializeField] float vSpeed;
    [SerializeField] float hSpeed;
    [SerializeField] float jumpingPower;

    [Header("Grounding")]
  //Movimento
    private float horizontal;
    private float vertical;
    bool facingRight;

    //Variaveis de animação
    private Animator anim;
    private bool isWalking;
    private bool isAttacking;

    //Variaveis de Ataque

    //pulo
    bool isJumping = false;
    float lastY;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    #region On Hit
    void OnTriggerEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hitbox"))
        {
            Debug.Log("HitEnemy");

        }


    }
    #endregion
}
