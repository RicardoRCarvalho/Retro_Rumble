using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemy : EnemyController
{
    public float speed;
    public float chaseDistance;
    public float stopDistance;
    public GameObject target1;
    public GameObject target2;

    private float target1Distance;
    private float target2Distance;


    [SerializeField]Rigidbody2D rigidbody;
    private float Knockback = 100;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
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
                if(target1Distance < chaseDistance && target1Distance > stopDistance)
                {
                    ChasePlayer(target1);
                }
            }
            else
            {
                if(target2Distance < chaseDistance && target2Distance > stopDistance)
                {
                    ChasePlayer(target2);
                }
            }
        }
    }

    private void ChasePlayer(GameObject target)
    {
        if (transform.position.x < target.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void BASIC_STUN()
    {

        Debug.Log("ENEMY_BasicStun");
       // rigidbody.gravityScale = 1.5f;
        rigidbody.AddForce(new Vector2(Knockback, 0));
    }
    
}
