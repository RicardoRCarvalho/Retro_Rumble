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

    //Pulo
    Rigidbody2D rigidbody;
    float lastY;
    bool isJumping;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.Sleep();
    }

    //Executado de acordo com o editor
    private void FixedUpdate()
    {
        if(!isJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            if (transform.position.y <= lastY-0.00000000000001)
            {
                OnLanding();
            }
        }
        Flip(horizontal);
    }

    //Executado todo frame
    void Update()
    {
        
    }

    //Pulo
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJumping)
        {
            lastY = transform.position.y;
            rigidbody.gravityScale = 1.5f;
            rigidbody.WakeUp();
            rigidbody.AddForce(new Vector2(transform.position.x + 7.5f, jumpingPower));
            isJumping = true;
        }
    }

    //Aterrisar
    void OnLanding()
    {
        isJumping = false;
        rigidbody.gravityScale = 0f;
        rigidbody.Sleep();
        lastY = transform.position.y;
    }
    //Movimento
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;

    }

    //inverter lado que está olhando
    private void Flip(float horizontal)
    {
        if (horizontal < 0 && !facingRight || horizontal > 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        } 
    }
}
