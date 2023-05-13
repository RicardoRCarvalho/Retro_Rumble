using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int movementSpeed = 1;

    float horizontal;
    float vertical;
    bool facingRight;
    Rigidbody2D rigidbody;
    float lastY;
    bool isJumping = false;
    float jumpForce = 300;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.Sleep();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            Vector3 movement = new Vector3(horizontal * movementSpeed, 0 * movementSpeed, 0.0f);
            transform.position = transform.position + movement * Time.deltaTime;
        }
        else
        {
            Vector3 movement = new Vector3(horizontal * movementSpeed, vertical * movementSpeed, 0.0f);
            transform.position = transform.position + movement * Time.deltaTime;
        }
        flip(horizontal);
        if(transform.position.y <= lastY)
        {
            OnLanding();
        }

        if(Input.GetButton("Jump") && !isJumping)
        {
            lastY = transform.position.y;
            rigidbody.gravityScale = 1;
            rigidbody.WakeUp();
            rigidbody.AddForce(new Vector2(transform.position.x + 7.5f, jumpForce));
            isJumping = true;
        }

    }

    void flip(float horizontal)
    {
        if (horizontal < 0 && !facingRight || horizontal > 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void OnLanding()
    {
        isJumping = false;
        rigidbody.gravityScale = 0f;
        rigidbody.Sleep();
        lastY = transform.position.y;

    }
}
