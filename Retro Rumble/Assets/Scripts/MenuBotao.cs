using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuBotao : MonoBehaviour
{
    //  public GameObject player2;
    //public GameObject player2UI;
    // public PlayerControls script;
    public Animator anim;
    public static int numPlayers = 1;
    private int selectedButton = 1;

    public void UpMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetBool("1Player", true);
            Debug.Log("click");
            selectedButton = 1;
        }
    }
    public void DownMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetBool("1Player", false);
            Debug.Log("click");
            selectedButton = 2;
        }
    }
    public void Click(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetTrigger("Click");
        }
    }

    public void SinglePlayer()
    {
        
        numPlayers = 1;
        SceneManager.LoadScene("Cena1");

       
    }
    public void MultiPlayer()
    {
        
        numPlayers = 2;

        SceneManager.LoadScene(1);
    }    
}
