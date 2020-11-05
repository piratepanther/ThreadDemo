using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine.UI;

public class SocketClientUDP : MonoBehaviour
{
    public string ip;
    public int port;
    private UdpClient udpService;
    public IPEndPoint endPoint { get; set; }

    public Input textInput;
    private Button sendButton;
//     public string endIp { get; set; }
// 
     //public int endPort { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        //本地终结点
        IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(ip),port);

        var serverTest = FindObjectOfType<SocketServerUDP>();
        //服务端终结点
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(serverTest.ip), serverTest.port);
        udpService = new UdpClient(localEP);
//         sendButton=GetComponentInChildren<Button>();
//         textInput=GetComponentInChildren<Input>();

    }

    private void SendMessage(string msg)
    {
        //string==>Byte[]
        byte[] dgram = Encoding.UTF8.GetBytes(msg);
        udpService.Send(dgram,dgram.Length,endPoint);
    }

//     public void DisplayMessage()
//     {
//         //string value = textInput.;
//         string value="1";
//             SendMessage(value);
// 
//     }






    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        udpService.Close();
    }





}
