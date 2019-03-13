using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class Menu : MonoBehaviour
{

    public static Menu Instance { get; set; }

    public GameObject connect;
    public GameObject host;
    public GameObject menu;
    public GameObject clientPrefab;
    public GameObject serverPrefab;
    public Server s;
    public Client c;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        connect.SetActive(false);
        host.SetActive(false);
        menu.SetActive(true);
    }

    public void ConnectMenu()
    {
        connect.SetActive(true);
        menu.SetActive(false);
    }

    public void HostMenu()
    {
        host.SetActive(true);
        menu.SetActive(false);

        s = Instantiate(serverPrefab).GetComponent<Server>();
        s.Init();
    }

    public void BackBtn()
    {
        connect.SetActive(false);
        host.SetActive(false);
        menu.SetActive(true);
    }

    public void connectBtn()
    {

        try {

            if (c==null)
            {
                c = Instantiate(clientPrefab).GetComponent<Client>();
            }

            if (c.ConnectToServer("127.0.0.1", 6321))
            {
               // SceneManager.LoadScene("game");
            }

            }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }

    }
}
