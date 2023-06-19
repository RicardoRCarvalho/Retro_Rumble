using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : MonoBehaviour
{
    public float speed;
    public float chaseDistance;
    public float stopDistance;
    public float radius;
    public int life;
    public GameObject target1;
    public PlayerControls playerControls1;
    public GameObject target2;
    public PlayerControls playerControls2;
    public Material flashMaterial;
    public GameObject powerExplosion;
    public GameObject bg;

    private Animator anim;
    private float target1Distance;
    private float target2Distance;

    private int target1EnemiesEngaged;
    private int target2EnemiesEngaged = 0;
    private Vector3 targetPosition;
    private bool hasAttacked;
    private bool asleep = true;


    // The SpriteRenderer that should flash.
    private SpriteRenderer spriteRenderer;
        
    // The material that was in use, when the script started.
    private Material originalMaterial;


    // The currently running coroutine.
    private Coroutine flashRoutine;


    [SerializeField]Rigidbody2D rigidbody;
    public float Knockback = 100;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody.gravityScale = 0f;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;

    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            anim.SetBool("isFalling", true);
            StartCoroutine(Destroy());
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
        else if (!asleep)
        {
            IAChoice();
        }
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(bg.transform.position, new Vector2(19.20f, 10.80f), 0f);
        int players = 0;
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Player")
            {
                players += 1;
            }
        }
        if (players != 0)
        {
            asleep = false;
        }
    }

    private void ChasePlayer(GameObject target)
    {
        if (transform.position.x > target.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        targetPosition = new Vector3(target.transform.position.x + 1f, target.transform.position.y + 1.5f, 0f);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            anim.SetBool("isWalking", false);
            if (!hasAttacked)
            {
                AttackPlayer(target);
            }
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    }

    private void AttackPlayer(GameObject target)
    {
        StartCoroutine(Attack());
    }

    private void IAChoice()
    {
        target1Distance = Vector2.Distance(transform.position, target1.transform.position);
        target2Distance = Vector2.Distance(transform.position, target2.transform.position);
        if (playerControls2.life <= 0)
        {
            ChasePlayer(target1);
        }
        else if (playerControls1.life <= 0)
        {
            ChasePlayer(target2);
        }
        else
        {
            if(target1Distance < target2Distance)
            {
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(target1.transform.position, radius);
                target1EnemiesEngaged = 0;
                foreach (Collider2D hitCollider in hitColliders)
                {
                    if (hitCollider.tag == "Enemy")
                    {
                        target1EnemiesEngaged += 1;
                    }
                }
                if (target1EnemiesEngaged < 3)
                {
                    ChasePlayer(target1);
                }
                else
                {
                    Collider2D[] hitColliders2 = Physics2D.OverlapCircleAll(target2.transform.position, radius);
                    target2EnemiesEngaged = 0;
                    foreach (Collider2D hitCollider in hitColliders2)
                    {
                        if (hitCollider.tag == "Enemy")
                        {
                            target2EnemiesEngaged += 1;
                        }
                    }
                    if (target2EnemiesEngaged < 3)
                    {
                        ChasePlayer(target2);
                    }
                    else
                    {
                        ChasePlayer(target1);
                    }
                    
                }
            }
            else
            {
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(target2.transform.position, radius);
                target2EnemiesEngaged = 0;
                foreach (Collider2D hitCollider in hitColliders)
                {
                    if (hitCollider.tag == "Enemy")
                    {
                        target2EnemiesEngaged += 1;
                    }
                }
                if (target2EnemiesEngaged < 3)
                {
                    ChasePlayer(target2);
                }
                else
                {
                    Collider2D[] hitColliders2 = Physics2D.OverlapCircleAll(target1.transform.position, radius);
                    target1EnemiesEngaged = 0;
                    foreach (Collider2D hitCollider in hitColliders2)
                    {
                      if (hitCollider.tag == "Enemy")
                      {
                          target1EnemiesEngaged += 1;
                      }
                    }
                    if (target1EnemiesEngaged < 3)
                    {
                        ChasePlayer(target1);
                    }
                    else
                    {
                        ChasePlayer(target2);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Power"))
        {
            StartCoroutine(Explosion());
            BASIC_STUN();
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(target1.transform.position, radius);
    }

    IEnumerator Choice()
    {
        yield return new WaitForSeconds(2);
        IAChoice();
    }

    IEnumerator Attack()
    {
        anim.SetBool("isAttacking", true);
        StartCoroutine(Attacked());
        yield return new WaitForSeconds(1);
        anim.SetBool("isAttacking", false);
    }

    IEnumerator Attacked()
    {
        hasAttacked = true;
        yield return new WaitForSeconds(2);
        hasAttacked = false;
        
    }

    IEnumerator Hit()
    {
        rigidbody.AddForce(new Vector2(Knockback, 0));
        yield return new WaitForSeconds(1);
        rigidbody.AddForce(new Vector2(-Knockback, 0));

    }
    public void BASIC_STUN()
    {

        Debug.Log("ENEMY_BasicStun");
        // rigidbody.gravityScale = 1.5f;
        StartCoroutine(Hit());
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine());

    }
    
    IEnumerator FlashRoutine()
        {
            life -= 1;
            // Swap to the flashMaterial.
            spriteRenderer.material = flashMaterial;

            // Pause the execution of this function for "0.125" seconds.
            yield return new WaitForSeconds(0.125f);

            // After the pause, swap back to the original material.
            spriteRenderer.material = originalMaterial;

            // Set the routine to null, signaling that it's finished.
            flashRoutine = null;
        }
    
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    IEnumerator Explosion()
    {
        GameObject explosionObject = Instantiate(powerExplosion);
        explosionObject.transform.position = new Vector3 (transform.position.x - 1.5f, transform.position.y + 2f, 0);
        yield return new WaitForSeconds(0.4f);
        Destroy(explosionObject);
    }
}
