using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStairsController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    private Animator anim1;
    private Animator anim2;
    public StairsController stairsController;
    public GameObject shadowP1;
    public GameObject shadowP2;
    public PlayerControls playerControls1;
    public PlayerControls playerControls2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            playerControls1.stair = false;
            playerControls2.stair = false;
            stairsController.stair = false;
            player1.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, 0f);
            player2.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, 0f);
            shadowP1.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, 0f);
            shadowP2.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, 0f);
            player1.transform.position = transform.position;
            player2.transform.position = transform.position; 
            anim1 = player1.GetComponent<Animator>();
            anim1.SetBool("isWalking", false);
            anim2 = player2.GetComponent<Animator>();
            anim2.SetBool("isWalking", false);
        }
    }
}
