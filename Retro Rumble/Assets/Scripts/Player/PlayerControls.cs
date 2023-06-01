using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Player Component References")]
    [SerializeField] Rigidbody2D rbPcSprite;
    [SerializeField] Rigidbody2D rbPcSombra;

    [Header("Player Settings")]
    [SerializeField] float vSpeed;
    [SerializeField] float hSpeed;
    [SerializeField] float jumpingPower;

    [Header("Grounding")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    //Movimento
    private float horizontal;
    private float vertical;
    bool facingRight;

    //Variaveis de animação
    private Animator anim;
    private bool isWalking;
    private bool isAttacking;

    [Header("Hitbox")]
    [SerializeField] GameObject hitboxPoint;
    [SerializeField] float radiusHitbox;
    [SerializeField] BoxCollider2D hitbox;
    public LayerMask enemiesGround;

    [Header("Jump")]
    private bool isGrounded;

    #region PlayerMovement

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    //Atualiza de acordo com o editor
    private void FixedUpdate()
    {

        //Acabar com a animação de ataque
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackPlayer1")&& anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            anim.SetBool("isAttacking", false);
        }
       
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            anim.SetBool("isJumping", false);
            Debug.Log("Pousou");
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            Debug.Log("pulo");
        }
    }
    //Movimento
    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {


            horizontal = context.ReadValue<Vector2>().x;
            vertical = context.ReadValue<Vector2>().y;
            anim.SetBool("isWalking", true);
            Flip(horizontal);

            if (isGrounded)
            {

                rbPcSombra.velocity = new Vector2(horizontal * hSpeed, vertical * vSpeed);
                rbPcSprite.velocity = rbPcSombra.velocity;
                anim.SetBool("isWalking", true);

            }
            else
            {

                rbPcSombra.velocity = new Vector2(horizontal * hSpeed, vertical * vSpeed);
                rbPcSprite.velocity = new Vector2(horizontal * hSpeed, rbPcSprite.velocity.y);

                rbPcSprite.gravityScale = 1.5f;



                anim.SetBool("isJumping", true);

            }
          
        }
        else if(!isGrounded)
        {
            
            anim.SetBool("isWalking", false);
            rbPcSombra.velocity = new Vector2(0,0);
            rbPcSprite.velocity = new Vector2(0, rbPcSprite.velocity.y);

        }
        else
        {
            anim.SetBool("isWalking", false);
            rbPcSombra.velocity = new Vector2(0, 0);
            rbPcSprite.velocity = new Vector2(0, 0);
        }
    }


    
    //Pulo
    public void Jump(InputAction.CallbackContext context)
    {

        if (context.performed && isGrounded)
        {

                 
                isGrounded = false;
                rbPcSprite.gravityScale = 1.5f;
             // rbPcSprite.WakeUp();
                rbPcSprite.AddForce(new Vector2(transform.position.x , jumpingPower));



                anim.SetBool("isJumping", true);
     
            
      
        }
    }

    //inverter o lado que está olhando
    private void Flip(float horizontal)
    {
        if(horizontal < 0 && !facingRight || horizontal > 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }


    
    #endregion
    #region PlayerAttack
    public void Attack(InputAction.CallbackContext context) 
    {

        anim.SetBool("isAttacking", true);

      

    }
    public void AnimationEnd()
    {
        anim.SetBool("isAttacking", false);
    }



    #endregion

}
