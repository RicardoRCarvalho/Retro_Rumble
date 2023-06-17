using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBotao : MonoBehaviour
{
    public GameObject player2;
    public GameObject player2UI;
    public PlayerControls script;

    public static int numPlayers = 1;

    // Start is called before the first frame update
    public void SinglePlayer()
    {
        Debug.Log("click");
        numPlayers = 1;
        SceneManager.LoadScene("Cena1");

       
    }
    public void MultiPlayer()
    {
        Debug.Log("click");
        numPlayers = 2;

        SceneManager.LoadScene(1);
    }    
}
