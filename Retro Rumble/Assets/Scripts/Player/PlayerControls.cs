using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Player Component References")]
    [SerializeField] Rigidbody2D rb;

    [Header("Player Settings")]
    [SerializeField] float speed;
    [SerializeField] float jumpingPower;

    [Header("Grounding")]
    //Movimento
    private float horizontal;
    private float vertical;
    bool facingRight;

    private Animator anim;
    private bool isWalking;
    private bool isAttacking;
    //pulo
    bool isJumping;
    float lastY;
    #region PlayerMovement
    //Atualiza de acordo com o editor
    private void FixedUpdate()
    {
        if(!isJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, vertical * speed);

            anim.SetBool("isJumping", true);
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            if (transform.position.y <= lastY - 0.00000001)
            {
                onLanding();
            }
            anim.SetBool("isJumping", false);
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
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
        Flip(horizontal);
        anim.SetBool("isWalking", true);

    }

    //Pulo
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!isJumping)
            {

                lastY = transform.position.y;
                isJumping = true;
                rb.gravityScale = 1.5f;
                rb.WakeUp();
                rb.AddForce(new Vector2(transform.position.x + 7.5f, jumpingPower));



            }
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
    private void onLanding()
    {
        isJumping = false;
        rb.gravityScale = 0f;
        rb.Sleep();
        lastY = transform.position.y;
    }
    
    #endregion
    #region PlayerAttack
    public void Attack(InputAction.CallbackContext context) 
    {

        isAttacking = true;

        
    }
 
    #endregion

}
