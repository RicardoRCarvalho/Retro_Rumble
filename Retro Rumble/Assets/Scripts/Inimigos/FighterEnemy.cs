using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemy : MonoBehaviour
{
    public float speed;
    public float chaseDistance;
    public float stopDistance;
    public float radius;
    public GameObject target1;
    public GameObject target2;

    private Animator anim;
    private float target1Distance;
    private float target2Distance;

    private int target1EnemiesEngaged;
    private int target2EnemiesEngaged = 0;
    private Vector3 targetPosition;
    private bool hasAttacked;


    [SerializeField]Rigidbody2D rigidbody;
    private float Knockback = 100;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody.gravityScale = 0f;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Choice());
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
        targetPosition = new Vector3(target.transform.position.x + 2f, target.transform.position.y, 0f);
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
        if (GameObject.Find("Player2") == null)
        {
            ChasePlayer(target1);
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
        yield return new WaitForSeconds(1);
        anim.SetBool("isAttacking", true);
        StartCoroutine(Attacked());
        yield return new WaitForSeconds(1);
        anim.SetBool("isAttacking", false);
    }

    IEnumerator Attacked()
    {
        hasAttacked = true;
        yield return new WaitForSeconds(4);
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

    }
    
}
