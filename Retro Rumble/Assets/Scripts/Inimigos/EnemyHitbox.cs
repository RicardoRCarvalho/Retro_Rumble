using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        other.GetComponent<PlayerControls>().Damage();
    }

}
