using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
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
    public GameObject hitboxPoint;
    public float radiusHitbox;
    public LayerMask enemies;
    //pulo
    bool isJumping = false;
    float lastY;
    #region PlayerMovement

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    //Atualiza de acordo com o editor
    private void FixedUpdate()
    {
        if(isJumping)
        {
            if (transform.position.y <= lastY - 0.00000001)
            {
                onLanding();
            }
            
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
            if(!isJumping)
            {
                rb.velocity = new Vector2(horizontal * hSpeed, vertical * vSpeed);
            }
            else
            {
                rb.velocity = new Vector2(horizontal * hSpeed, rb.velocity.y);
            }
            Flip(horizontal);
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
            //acho q esses valores vão ter q mudar pra não bugar o pulo
            rb.velocity = new Vector2(0, 0);
        }
    }

    //Pulo
    public void Jump(InputAction.CallbackContext context)
    {
        //if (context.performed)
       // {
            if (!isJumping)
            {

                
                lastY = transform.position.y;
                isJumping = true;
                rb.gravityScale = 1.5f;
                rb.WakeUp();
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(transform.position.x + 7.5f, jumpingPower));

                anim.SetBool("isJumping", true);

            }
       // }
      
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
    private void onLanding()
    {
        isJumping = false;
        rb.gravityScale = 0f;
        rb.Sleep();
        rb.velocity = new Vector2(horizontal * hSpeed, vertical * vSpeed);
        lastY = transform.position.y;
        anim.SetBool("isJumping", false);
    }
    
    #endregion
    #region PlayerAttack
    public void Attack(InputAction.CallbackContext context) 
    {

        anim.SetBool("isAttacking", true);
        Collider2D[]enemy = Physics2D.OverlapCircleAll(hitboxPoint.transform.position,radiusHitbox, enemies);
        foreach(Collider2D enemyGameOBject in enemy)
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
