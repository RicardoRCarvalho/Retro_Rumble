using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    public GameObject endCollider;
    public bool stair = false;
    public GameObject player1;
    public GameObject player2;
    private Animator anim1;
    private Animator anim2;
    public PlayerControls playerControls1;
    public PlayerControls playerControls2;
    public GameObject shadowP1;
    public GameObject shadowP2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stair == true)
        {
            Vector3 targetPosition = new Vector3 (endCollider.transform.position.x, endCollider.transform.position.y, endCollider.transform.position.z);
            player1.transform.position = Vector2.MoveTowards(player1.transform.position, targetPosition, 2.5f * Time.deltaTime);
            player2.transform.position = Vector2.MoveTowards(player2.transform.position, targetPosition, 3f * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            playerControls1.stair = true;
            playerControls2.stair = true;
            stair = true;
            player1.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, 0f);
            player2.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, 0f);
            shadowP1.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, 0f);
            shadowP2.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, 0f);
            player1.transform.position = transform.position;
            player2.transform.position = transform.position; 
            anim1 = player1.GetComponent<Animator>();
            anim1.SetBool("isWalking", true);
            anim2 = player2.GetComponent<Animator>();
            anim2.SetBool("isWalking", true);
        }
    }
}
