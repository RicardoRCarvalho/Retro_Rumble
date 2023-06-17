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
    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(bg.transform.position, new Vector2(19.20f, 10.80f), 0f);
        int enemies = 0;
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Enemy")
            {
                //Debug.Log("enemy");
                enemies += 1;
            }
        }
        if (enemies == 0)
        {
            collider.isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cameraController.trackingTarget = GameObject.Find(nextBG.name);
            StartCoroutine(Teleport());

        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(2);
        player1.transform.position = new Vector3(transform.position.x, player1.transform.position.y, 0f);
        player2.transform.position = new Vector3(transform.position.x, player2.transform.position.y, 0f);
    }

}
