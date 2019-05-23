using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
    public Client client;
    public Text text;
    void Start()
    {
        client = FindObjectOfType<Client>();
        text.text = "Winner is " + client.winner + "!";

        switch (client.winner)
        {
            case "Red":
                text.color = Color.red;
                break;
            case "Blue":
                text.color = Color.blue;
                break;
            case "Yellow":
                text.color = Color.yellow;
                break;
            case "Green":
                text.color = Color.green;
                break;
        }


    }

    


    public void QuitGame()
    {
        Application.Quit();
    }
}
