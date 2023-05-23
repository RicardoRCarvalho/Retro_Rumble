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
    [SerializeField] GameObject Player;
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
    public LayerMask enemiesGround;

    [Header("Jump")]
    private bool isGrounded;
    [SerializeField] float timeToPeak = 0.5f;
    [SerializeField] float timeToFall = 1f;
    [SerializeField] float jumpTimer = 0f;

    #region PlayerMovement

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    //Atualiza de acordo com o editor
    private void FixedUpdate()
    {
        
        if(!isGrounded)
        {
            onAir();
        }

        
        //Acabar com a animação de ataque
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackPlayer1")&& anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            anim.SetBool("isAttacking", false);
        }
        //  if (isAttacking)
        // {
        //     anim.SetBool("isAttacking", true);
        //  }
        //  else
        //  {
        //      anim.SetBool("isAttacking", false);
        //   }
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
            isGrounded = false;
            Debug.Log("pulo");
        }
    }
    private void Update()
    {
        
    }
    //Movimento
    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {


            horizontal = context.ReadValue<Vector2>().x;
            vertical = context.ReadValue<Vector2>().y;


            rbPcSprite.velocity = new Vector2(horizontal * hSpeed, vertical * vSpeed);


            rbPcSombra.velocity = new Vector2(horizontal * hSpeed, vertical * vSpeed);


            Flip(horizontal);
            if (isGrounded)
            {
                anim.SetBool("isWalking", true);
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
            rbPcSprite.velocity = new Vector2(0, 0);
            rbPcSombra.velocity = new Vector2(0, 0);
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


                anim.SetBool("isWalking", false);
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

    //Aterrisar no mesmo y que pulou
    private void onAir()
    {
        
        rbPcSprite.gravityScale = 1.5f;
        //  rbPcSprite.gravityScale = 0f;
        // rbPcSprite.Sleep();
        // rbPcSombra.Sleep();
        // rbPcSprite.velocity = new Vector2(horizontal * hSpeed, vertical * vSpeed);
        // Landing = transform.position.y;
        anim.SetBool("isJumping", true);
    }

    
    #endregion
    #region PlayerAttack
    public void Attack(InputAction.CallbackContext context) 
    {

        anim.SetBool("isAttacking", true);
      

    }
    public void basicAttack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(hitboxPoint.transform.position, radiusHitbox, enemiesGround);
        foreach (Collider2D enemyGameOBject in enemy)
        {
            Debug.Log("HitEnemy");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitboxPoint.transform.position, radiusHitbox);

    }
    #endregion

}
