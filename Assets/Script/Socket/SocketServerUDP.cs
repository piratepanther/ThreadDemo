using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

using Common;
public class SocketServerUDP : MonoBehaviour
{
    public string ip;
    public int port;
    private UdpClient udpService;
    private Thread threadReceive;
    
    // Start is called before the first frame update
    void Start()
    {
        IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(ip), port);

//         var serverTest = FindObjectOfType<SocketServerUDP>();
//         //服务端终结点
//         IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(serverTest.ip), serverTest.port);
        UdpClient udpService = new UdpClient(localEP);

        //通过线程调度接收消息方法

        Thread threadReceive = new Thread(ReceiveMessage);
        threadReceive.Start();
    }

    private void ReceiveMessage()
    {
        while (true)
            {
	            IPEndPoint remote = new IPEndPoint(IPAddress.Any,0);//任意IP，端口
	            Debug.Log("消息后" + remote.Address + "-----" + remote.Port);
	            //receive接受方法：
	            //--收到消息前，参数表示需要监听的终结点
	            //--收到消息后，参数表示实际接受的终结点
	            //--收到消息前，阻塞线程
	            byte[] data = udpService.Receive(ref remote);
	
	            Debug.Log("消息前" + remote.Address + "-----" + remote.Port);
	            //byte[]==>string
	            string msg = Encoding.UTF8.GetString(data);
	            ThreadCrossHelper.instance.ExecuteOnMainThread(() => {
	                DisplayMessage(msg);
	        
	            });
            }  
    }

    private void DisplayMessage(string msg)
    {
        //输出到指定UI


    }

    private void OnApplicationQuit()
    {
        threadReceive.Abort();
        udpService.Close();
    }
   
}
