using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitController : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite half;
    public Sprite low;
    public Sprite full;
    public Sprite hitFull;
    public Sprite hitLow;
    public PlayerControls playerControls;
    private bool hit = false;
    
    void FixedUpdate()
    {
        if (!hit)
        {
            if (playerControls.life <= 30)
            {
                sr.sprite = low;
            }
            else if (playerControls.life <= 60)
            {
                sr.sprite = half;
            }
            else
            {
                sr.sprite = full;
            }
        }
    }

    public void PortraitHit()
    {
        if (playerControls.life <= 40)
        {
            StartCoroutine(LowHit());
        }
        else
        {
            StartCoroutine(FullHit());
        }
    }

    IEnumerator LowHit()
    {
        hit = true;
        sr.sprite = hitLow;
        yield return new WaitForSeconds(1f);
        hit = false;
    }

    IEnumerator FullHit()
    {
        hit = true;
        sr.sprite = hitFull;
        yield return new WaitForSeconds(1f);
        hit = false;
    }
}
