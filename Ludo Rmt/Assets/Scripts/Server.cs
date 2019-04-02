using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.IO;
using System.Linq;

public class Server : MonoBehaviour
{
    public int port = 6321;
    

    private List<ServerClient> clientsList;
    private List<ServerClient> disconnectList;

    int move;

    private TcpListener server;
    private bool serverStarted;

    public void Init()
    {
        DontDestroyOnLoad(gameObject);
        clientsList = new List<ServerClient>();
        disconnectList = new List<ServerClient>();
        move = 0;

        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            StartListening();
            

            serverStarted = true;
        }
        catch(Exception e)
        {
            UnityEngine.Debug.Log(e.Message);
        }
    }
    private void Update()
    {
        if (!serverStarted)
        {
            return;
        }
    

        foreach(ServerClient c in clientsList)
        {

            if (!IsConnected(c.tcp))
            {
                c.tcp.Close();
                
                disconnectList.Add(c);
                continue;
            }
            else
            {
                NetworkStream s = c.tcp.GetStream();
 
                if (s.DataAvailable)
                {
                    StreamReader reader = new StreamReader(s, true);
                    string data = reader.ReadLine();
                    
                    
                    if (data != null)
                    {
                        Debug.Log(data);
                        OnIncomingData(c, data);
                        
                    }
                   
                    
                }

            }
        }

        for(int i=0; i < disconnectList.Count; i++)
        {
            clientsList.Remove(disconnectList[i]);
            Debug.Log("izbacio sam" + disconnectList.ElementAt(i).color);
            disconnectList.RemoveAt(i);
            
        }
    }
    private void StartListening()
    {
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
        Debug.Log("Cekam konekciju");
    }

    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;

        ServerClient sc = new ServerClient(listener.EndAcceptTcpClient(ar));
        Debug.Log("Primio konekciju");

        switch (clientsList.Count)
        {
            case 0:
                sc.color = "Red";
                Send("#Red", sc);
                break;
            case 1:
                sc.color = "Blue";
                Send("#Blue", sc);
                break;
            case 2:
                sc.color = "Yellow";
                Send("#Yellow", sc);
                break;
            case 3:
                sc.color = "Green";
                Send("#Green", sc);
                break;
        }

        clientsList.Add(sc);

        if (clientsList.Count == 4)
        {
            System.Threading.Thread.Sleep(500);
            BroadCast("Start", clientsList);
        }else
            StartListening();

 
       
    }
    private bool IsConnected(TcpClient c)
    {
        try
        {
            if(c!=null && c.Client!=null && c.Client.Connected)
            {

                if (c.Client.Poll(0, SelectMode.SelectRead)){
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    private void OnIncomingData(ServerClient c, string data)
    {
        if (data.Equals("Played"))
        {
            //Debug.Log("Treba da posalje " + (++move % 4));
            Send("Play", clientsList.ElementAt(++move%4));
        }

        if (data.StartsWith("$"))
        {

            BroadCast(data, clientsList);

        }
    }

    private void BroadCast(string data, List<ServerClient> scl)
    {
        
        foreach (ServerClient sc in scl)
        {
            
            try {
                StreamWriter wr = new StreamWriter(sc.tcp.GetStream());
                wr.WriteLine(data);
                wr.Flush();
                
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }

    private void Send(string data, ServerClient sc)
    {
        StreamWriter wr = new StreamWriter(sc.tcp.GetStream());
        wr.WriteLine(data);
        wr.Flush();
       
        //Debug.Log("Poslato");
    }


}

public class ServerClient
{
    public string clientName;
    public TcpClient tcp;
    public string color;

    public ServerClient(TcpClient tcp)
    {
        this.tcp = tcp;
    }
}
