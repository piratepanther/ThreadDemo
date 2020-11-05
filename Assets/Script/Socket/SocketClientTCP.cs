using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System;

using Common;

public class SocketClientTCP : MonoBehaviour
{

    public string ip;
    public int port;
    private TcpClient tcpService;


    // Use this for initialization
    void Start()
    {

//         IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(ip), port);
//         tcpService = new TcpClient(localEP);
        //不绑定端口会自动分配端口
        tcpService = new TcpClient();
        var serverTest = GetComponent<SocketServerTCP>();
        IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(serverTest.ip), serverTest.port);
        tcpService.Connect(serverEP);


    }


    public void SendChatMessage(string message)//button.onclick.addlistener 
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        NetworkStream stream = tcpService.GetStream();
        stream.Write(data,0,data.Length);

    }

    private void OnApplicationQuit()
    {
        //问题：客户端绑定端口，关闭后30s-4min再登陆会提示端口被占用
        //1.让服务器先下线（先断开的一方会延时释放端口）2.每次换端口
//         SendChatMessage("Quit");
//         Thread.Sleep(500);
        tcpService.Close();
    }


}
