using System.Collections;
using System.Collections.Generic;
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

        Server s = Instantiate(serverPrefab).GetComponent<Server>();
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

        Client c = Instantiate(clientPrefab).GetComponent<Client>();
        c.ConnectToServer("127.0.0.1", 6321);

            if (c.socketReady)
            {

            }

            }
        catch(Exception e)
        {
           
        }

    }
}
