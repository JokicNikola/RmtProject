using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public static Menu Instance { get; set; }

    public GameObject connect;
    public GameObject host;
    public GameObject menu;

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
    }

    public void BackBtn()
    {
        connect.SetActive(false);
        host.SetActive(false);
        menu.SetActive(true);
    }
}
