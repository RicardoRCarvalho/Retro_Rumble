using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] AudioSource camera;
    public AudioClip[] soundsPlayer;
    public PlayerControls playerControls;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.position.x < other.transform.position.x)
        {

        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit");     
            name = other.name;   
            if (name.Contains("Enemy1"))
            {
                if (transform.position.x < other.transform.position.x)
                {
                    FighterEnemy controller = other.GetComponent<FighterEnemy>();
                    controller.Knockback = 100;
                }
                else
                {
                    FighterEnemy controller = other.GetComponent<FighterEnemy>();
                    controller.Knockback = -100;
                }
                other.GetComponent<FighterEnemy>().BASIC_STUN();
            }
            else if (name.Contains("Enemy2"))
            {
                if (transform.position.x < other.transform.position.x)
                {
                    SkeletonEnemy controller = other.GetComponent<SkeletonEnemy>();
                    controller.Knockback = 100;
                }
                else
                {
                    SkeletonEnemy controller = other.GetComponent<SkeletonEnemy>();
                    controller.Knockback = -100;
                }
                other.GetComponent<SkeletonEnemy>().BASIC_STUN();
            }
            else if (name.Contains("Boss"))
            {
                if (transform.position.x < other.transform.position.x)
                {
                    BossEnemy controller = other.GetComponent<BossEnemy>();
                    controller.Knockback = 100;
                }
                else
                {
                    BossEnemy controller = other.GetComponent<BossEnemy>();
                    controller.Knockback = -100;
                }
                other.GetComponent<BossEnemy>().BASIC_STUN();
            }
            camera.clip = soundsPlayer[Random.Range(0, soundsPlayer.Length)];
            camera.Play();
            if (playerControls.mana <= 95)
            {
                playerControls.mana += 5;
            }   
        }
    }
}
