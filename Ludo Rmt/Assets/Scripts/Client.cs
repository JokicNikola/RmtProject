using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class Client : MonoBehaviour
{
    public bool socketReady;
    private String clientName;
    public String clientColor;
    public bool isMyMove;
    public String readData;

    public String whosMove;
    public String previousMove;
    

    private TcpClient socket;
    public NetworkStream stream;
    public StreamWriter writer;
    public StreamReader reader;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        isMyMove = false;
        readData = "";
        
    }

    public bool ConnectToServer(string host, int port)
    {
        if (socketReady)
        {
            return false;
        }

        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);

            socketReady = true;

        }
        catch(Exception e)
        {
            UnityEngine.Debug.Log("Socket error " + e.Message);
        }

        return socketReady;
    }

    public void Update()
    {
        if (socketReady)
        {
            if (stream.DataAvailable)
            {
                string data = reader.ReadLine();
               
                if (data != null)
                {
                    Debug.Log(data);
                    OnIncomingData(data);
                }
                
            }
        }
    }
    

    public void Send(string data)
    {
        if (!socketReady)
        {
            return;
        }


        writer.WriteLine(data);
        
        writer.Flush();
    }
    public void OnIncomingData(string data)
    {
       Debug.Log(clientColor+": "+ data);
       
        if (data.Equals("Start"))
           changeScene(data);

        if (data.StartsWith("#"))
            nameSet(data.Substring(1));

        if (data.Contains("Play"))
        {
            whosMove = data.Substring(data.IndexOf('-')+1);
            
            //Debug.Log("Sada igra " + whosMove);
            if(whosMove.Equals(clientColor))
                isMyMove = true;

            switch (whosMove)
            {
                case "Red": previousMove = "Green"; break;
                case "Blue": previousMove = "Red"; break;
                case "Yellow": previousMove = "Blue"; break;
                case "Green": previousMove = "Yellow"; break;
            }   
        }

        if (data.StartsWith("GO"))
        {
            CloseSocket();
            SceneManager.LoadScene("end");
            
        }
        

        readData = data;
        

    }

    public void nameSet(string data)
    {
        clientColor = data;
        return;
    }

    public void changeScene(string data)
    {
        
        SceneManager.LoadScene("game");
        return;
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }
    private void OnDisable()
    {
        CloseSocket();
    }
    private void CloseSocket()
    {
        if (!socketReady)
            return;
        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
    }
}


