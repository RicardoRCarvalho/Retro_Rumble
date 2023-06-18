using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public Collider2D collider;
    public GameObject bg;
    public GameObject nextBG;
    public CameraController cameraController;
    public GameObject player1;
    public GameObject player2;
    private Vector3 targetPosition1;
    private Vector3 targetPosition2;
    private bool passou = false;
    // Update is called once per frame
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
            Debug.Log("player");
            cameraController.trackingTarget = GameObject.Find(nextBG.name);
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(1);
        player1.transform.position = new Vector3(transform.position.x + 2f, player1.transform.position.y, 0f);
        player2.transform.position = new Vector3(transform.position.x + 2f, player2.transform.position.y, 0f);
        collider.isTrigger = false;
    }

}
