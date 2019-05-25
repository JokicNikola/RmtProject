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
    int roll;

    private Coroutine b;

    private String whosMove;

    private TcpListener server;
    private bool serverStarted;

    public void Init()
    {
        whosMove = "Red";
        DontDestroyOnLoad(gameObject);
        clientsList = new List<ServerClient>();
        disconnectList = new List<ServerClient>();
        move = 0;
        roll = 0;

        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            StartListening();


            serverStarted = true;
        }
        catch (Exception e)
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


        for (int i = 0; i < clientsList.Count(); i++)
        {

            if (clientsList.ElementAt(i) != null)
            {

                if (!IsConnected(clientsList.ElementAt(i).tcp))
                {
                    clientsList.ElementAt(i).tcp.Close();
                    clientsList[i] = null;


                    disconnectList.Add(clientsList.ElementAt(i));
                    continue;
                }
                else
                {
                    NetworkStream s = clientsList.ElementAt(i).tcp.GetStream();

                    if (s.DataAvailable)
                    {
                        StreamReader reader = new StreamReader(s, true);
                        string data = reader.ReadLine();


                        if (data != null)
                        {
                            Debug.Log("Server: " + data);
                            OnIncomingData(clientsList.ElementAt(i), data);

                        }


                    }

                }
            }
        }

        for (int i = 0; i < disconnectList.Count; i++)
        {
            // clientsList.Remove(disconnectList[i]);
            // Debug.Log("izbacio sam" + disconnectList.ElementAt(i).color);
            // disconnectList.RemoveAt(i);

        }
    }
    private void StartListening()
    {
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
        // Debug.Log("Cekam konekciju");
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

        if (clientsList.Count == 2)
        {
            System.Threading.Thread.Sleep(200);
            BroadCast("Start", clientsList);
            //b= StartCoroutine(wait());
            //changeMove();
        }
        else
            StartListening();



    }
    private bool IsConnected(TcpClient c)
    {
        try
        {
            if (c != null && c.Client != null && c.Client.Connected)
            {

                if (c.Client.Poll(0, SelectMode.SelectRead))
                {
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

    private IEnumerator RollTheDice(String colorDice) {

        
        BroadCast("StartRoll|" + colorDice, clientsList);
        yield return new WaitForSeconds(1.4f);
        roll = UnityEngine.Random.Range(0, 6);

        BroadCast("Roll|" + roll + "|" + whosMove, clientsList);
    }

    private void OnIncomingData(ServerClient c, string data)
    {


        if (data.StartsWith("Roll"))
        {
            string colorDice = data.Substring(data.IndexOf('|')+1);

            StartCoroutine(RollTheDice(colorDice));

           
            return;

        }


        if (data.Equals("Played"))
        {
            if(b!=null)
                StopCoroutine(b);
            changeMove();
            return;

        }

        if (data.StartsWith("$"))
        {
            string check = data.Substring(data.IndexOf('|') + 1);


            if (check.Equals("out"))
            {
                if (b != null)
                    StopCoroutine(b);
                b = StartCoroutine(Wait());
                BroadCast(data, clientsList);
                return;
            }

            if (check == "6")
            {
                if (b != null)
                    StopCoroutine(b);
                b = StartCoroutine(Wait());
            }

            if (int.Parse(check) == roll + 1)
                BroadCast(data, clientsList);
            else changeMove();

            return;
        }

        if (data.StartsWith("%"))
        {
            BroadCast(data, clientsList);
            return;
        }

        if (data.StartsWith("END"))
        {
            string[] income = data.Split('|');
            BroadCast("GO|"+income[1], clientsList);
            return;
        }


    }
    
    private IEnumerator Wait()
    {

        int broj = 0;
        for (int i = 0; i < 14; i++) {
            Debug.Log("Server " + ++broj);
            yield return new WaitForSeconds(1f);
        }
        changeMove();
    }

    private void changeMove()
    {

        

        switch (++move % clientsList.Count())
        {
            case 0:

                if (clientsList.ElementAt(0) == null)
                {
                    changeMove();
                    return;
                }
                whosMove = clientsList.ElementAt(0).color;
                break;

            case 1:

                if (clientsList.ElementAt(1) == null)
                {
                    changeMove();
                    return;
                }

                whosMove = clientsList.ElementAt(1).color;

                break;

            case 2:


                if (clientsList.ElementAt(2) == null)
                {
                    changeMove();
                    return;
                }
                whosMove = clientsList.ElementAt(2).color;
                break;

            case 3:

                if (clientsList.ElementAt(3) == null)
                {
                    changeMove();
                    return;
                }
                whosMove = clientsList.ElementAt(3).color;
                break;
        }

        BroadCast("Play-" + whosMove, clientsList);

        if(b!=null)
        {
            StopCoroutine(b);
        }

       b= StartCoroutine(Wait());
    }

    private void BroadCast(string data, List<ServerClient> scl)
    {

        foreach (ServerClient sc in scl)
        {

            if (sc != null)
            {

                try
                {
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
