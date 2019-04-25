using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public string ipadress = "192.168.1.104";
    public int port = 7788;
    public InputField input;
    public Text output;
    private Socket clientSocket;
    
    //receivethread 
    Thread t;
    private byte[] data = new byte[65536];
    private string message = "";

    // Start is called before the first frame update
    void Start()
    {
        OnConnectedToServer();
    }

    // Update is called once per frame
    void Update()
    {
        if(message!= null&& message!= "")
        {
            output.text += '\n' + message;
            message = "";
        }
    }

    private void OnConnectedToServer()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ipadress), port));
        //creat a thread to receive接收消息线程
        t = new Thread(ReceiveMessage);
        t.Start();
    }

    private void ReceiveMessage()
    {
        while (true)
        {
            if (clientSocket.Poll(10, SelectMode.SelectRead))
                break;
            int length = clientSocket.Receive(data);
            message = Encoding.UTF8.GetString(data, 0, length);
        }
        
    }

    void SendMessage(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        if (data.Length != 0)
        {
            clientSocket.Send(data);
        }
    }

    public void OnSendButtonClick()
    {
        string value = input.text;
        SendMessage(value);
        input.text = "";
    }
    private void OnDestroy()
    {
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }
}
