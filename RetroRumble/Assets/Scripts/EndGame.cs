using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject bg;
    private bool passou = false;
    public Collider2D collider;

    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(bg.transform.position, new Vector2(19.20f, 10.80f), 0f);
        int enemies = 0;
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Enemy")
            {
                enemies += 1;
            }
        }
        if (enemies == 0  && !passou)
        {
            collider.isTrigger = true;
            passou = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(End());
        }
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }
}
