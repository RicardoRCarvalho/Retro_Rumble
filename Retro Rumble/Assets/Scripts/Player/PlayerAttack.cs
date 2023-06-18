using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] AudioSource camera;
    public AudioClip[] soundsPlayer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit");            
            other.GetComponent<FighterEnemy>().BASIC_STUN();
            camera.clip = soundsPlayer[Random.Range(0, soundsPlayer.Length)];
            camera.Play();
        }
    }
}
