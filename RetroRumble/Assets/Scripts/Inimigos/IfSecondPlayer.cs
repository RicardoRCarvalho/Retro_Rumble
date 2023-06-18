using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfSecondPlayer : MonoBehaviour
{
    public PlayerControls player2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        if (player2.life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
