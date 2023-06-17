using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Player Component References")]
    [SerializeField] Rigidbody2D rbPcSprite;
    [SerializeField] Rigidbody2D rbPcSombra;
    public GameObject player;
    [SerializeField] GameObject SecondPC;
    [SerializeField] GameObject SecondUI;

    public MenuBotao scriptMenu;

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
    public bool isOnTop = false;

    //Variaveis de animação
    private Animator anim;
    private bool isWalking;
    private bool isAttacking;
    

    [Header("Hitbox")]
    [SerializeField] GameObject hitboxPoint;
    [SerializeField] float radiusHitbox;
    [SerializeField] BoxCollider2D hitbox;
    public LayerMask enemiesGround;

    [Header("Hitbox")]
    private int Combo=0;

    [Header("Jump")]
    private bool isGrounded;

    [Header("PowerUp")]
    public PowerUpController powerUp;
    public bool hasShot;
    public int mana;

    [Header("Damage")]
    public int life;
    // The SpriteRenderer that should flash.
    private SpriteRenderer spriteRenderer;
        
    // The material that was in use, when the script started.
    private Material originalMaterial;

    public Material flashMaterial;
    // The currently running coroutine.
    private Coroutine flashRoutine;
    public GameObject bulletExplosion;

    [Header("HUD")]
    public PortraitController portrait;
    public GameObject HP;
    public GameObject MP;

    #region PlayerMovement

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        if (MenuBotao.numPlayers==1)
        {
            SecondPC.transform.position = new Vector2(-100000, 0);
            SecondUI.transform.position = new Vector2(-100000, 0);
            life = 0;
        }
       
    }

    //Atualiza de acordo com o editor
    private void FixedUpdate()
    {
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetBool("attack2String1", false);
            anim.SetBool("attack3String1", false);
        }
        if (life <= 0)
        {
            StartCoroutine(Death());

        }
        if(isOnTop)
        {
            Debug.Log(vertical);
            if(vertical > 0)
            {
                rbPcSprite.velocity = new Vector2(rbPcSombra.velocity.x, 0f);
            }
        }
        //Acabar com a animação de ataque
       // if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackPlayer1")&& anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
       // {
       //     anim.SetBool("isAttacking", false);
      //  }
       
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collider"))
        {
            Debug.Log("collider");
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Damage();
            StartCoroutine(Explosion());
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

    //Especial
    public void Special(InputAction.CallbackContext context)
    {
        if (!hasShot && isGrounded && context.performed && mana >= 50)
        {
            powerUp.PowerAttack(!facingRight);
            mana -= 50;
            MP.transform.localScale = new Vector3(mana*0.01f, 1f, 1f);
            StartCoroutine(Shooted());
        }
    }
    
    #endregion
    
    #region PlayerAttack
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed )
        {
            Attack_String();

        }
    }
    private void Attack_String()
    {
        Debug.Log("botaoataque");

        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        

        {
            anim.SetBool("attack3String1", true);
            
           
        }

        else if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))


        {
            anim.SetBool("attack2String1", true);
            
            Debug.Log("combo=2");
        }
        else if(this.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("RunPlayer1"))
        {
            anim.SetBool("attack1String1", true);
            
            Debug.Log("combo=1");
        }
    }
    public void Attack1End()
    {

       

            anim.SetBool("attack1String1", false);
        
    }
    public void Attack2End()
    { 
     
            anim.SetBool("attack2String1", false);
     
    }
    public void Attack3End()
    {
  
            anim.SetBool("attack3String1", false);
   
  
    }


    #endregion

    public void Damage()
    {
        Debug.Log("damage");
        anim.SetBool("damage", true);
        portrait.PortraitHit();
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine());


    }

    public void EndDamage()
    {
        StartCoroutine(AnimDamage());
    }

    IEnumerator Shooted()
    {
        hasShot = true;
        yield return new WaitForSeconds(2);
        hasShot = false;
    }

    IEnumerator FlashRoutine()
        {
            life -= 10;
            HP.transform.localScale = new Vector3(life*0.01f, 1f, 1f);
            // Swap to the flashMaterial.
            spriteRenderer.material = flashMaterial;

            // Pause the execution of this function for "0.125" seconds.
            yield return new WaitForSeconds(0.125f);

            // After the pause, swap back to the original material.
            spriteRenderer.material = originalMaterial;

            // Set the routine to null, signaling that it's finished.
            flashRoutine = null;
        }

        IEnumerator AnimDamage()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("damage", false);
    }

    IEnumerator Explosion()
    {
        GameObject explosionObject = Instantiate(bulletExplosion);
        explosionObject.transform.position = new Vector3 (transform.position.x , transform.position.y, 0);
        yield return new WaitForSeconds(0.4f);
        Destroy(explosionObject);
    }

    IEnumerator Death()
    {
        anim.SetBool("death", true);
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }
}
