using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private Animator anim;
    public GameObject enemy;
    public AudioClip[] soundsEnemy;
    [SerializeField] AudioSource camera;
    void Start()
    {
        anim = enemy.GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackFighter1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            other.GetComponent<PlayerControls>().Damage();
            camera.clip = soundsEnemy[Random.Range(0, soundsEnemy.Length)];
            camera.Play();
        }
        
    }

}
