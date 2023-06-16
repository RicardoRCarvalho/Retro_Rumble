using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private Animator anim;
    public GameObject enemy;

    void Start()
    {
        anim = enemy.GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackFighter1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            other.GetComponent<PlayerControls>().Damage();
        }
        
    }

}
