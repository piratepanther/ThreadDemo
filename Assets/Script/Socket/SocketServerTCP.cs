using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

using Common;

public class SocketServerTCP : MonoBehaviour
{
    public string ip;
    public int port;
    public TcpListener tcpListener;

    // Use this for initialization
    void Start()
    {
        IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(ip), port);
        tcpListener = new TcpListener(localEP);
        tcpListener.Start();
        Thread erverThread = new Thread(ReceiveMessage);
        erverThread.Start();
    }

    private void ReceiveMessage()
    {
	    //如果监听到客户端，执行
	    //没有监听到，阻塞
	    TcpClient Client = tcpListener.AcceptTcpClient();
        using (NetworkStream stream = Client.GetStream())
        {
	        byte[] data = new byte[512];
	        int count;
            //如果读取到数据则解析、显示
            while ((count = stream.Read(data,0,data.Length))>0)
            {
	            //读取数据
	            //--返回值实际读取到的字节数,如果为0，表示客户端下线
	            //--如果没有读取到数据则阻塞线程（当监听多可客户端时，需要开辅助线程）
	            string msg = Encoding.UTF8.GetString(data, 0, count);
                //if (msg == "Quit") break;
                ThreadCrossHelper.instance.ExecuteOnMainThread(() => 
	            {
	                DisplayMessage(msg);	
	            });
            }
        }
    }


    private void DisplayMessage(string msg)
    {
        //输出到指定UI


    }

    private void OnApplicationQuit()
    {

        tcpListener.Stop();
    }



}
