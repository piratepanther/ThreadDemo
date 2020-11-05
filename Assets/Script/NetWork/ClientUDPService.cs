using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using Common;
using System;
using System.Threading;


namespace Network
{
    public class ClientUDPService : MonoSingleton<ClientUDPService>
	{
        private UdpClient udpService;
        //加event方便使用+=、-=，私有化
        public event EventHandler<MessageArrivedEventArgs> MessageArrived;
        
        private Thread threadReceive;
        //创建Socket对象(登录窗口传递服务端地址、端口)
        public void Initialized(string serverIP,int serverPort)
        {
            DontDestroyOnLoad(gameObject);//加载场景不销毁
            
            //随机分配可以使用的端口
            udpService = new UdpClient();
            //与服务端连接
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
            
            udpService.Connect(serverEP);
            threadReceive = new Thread(ReceiveChatMessage);
            threadReceive.Start();
            //发送上线通知
            NotifyServer(MeaasgeType.OnLine);

        }
	    
	    //通知服务器
        public void NotifyServer(MeaasgeType type)
        {
            ChatMessage msg = new ChatMessage();
            msg.type = type;
            msg.SenderName = "yang";
            msg.Content = "Hello";
            
            SendMessage(msg);
        } 
        
        //发送数据

        public void SendMessage(ChatMessage msg)
        {
            byte[] data = msg.ObjectToBytes();
            int count = udpService.Send(data, data.Length);
            Debug.Log(count);
            
        }

                	
	    //接收数据
        private void ReceiveChatMessage()
        {
            while (true)
            {
	            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
	            byte[] data = udpService.Receive(ref remote);
	            //解析数据
	            ChatMessage msg = ChatMessage.ByteToObject(data);
	            //引发事件
                if (MessageArrived == null) continue;//ThreadCrossHelper
	            MessageArrivedEventArgs args = new MessageArrivedEventArgs()
	            {
	                
	            };
	            MessageArrived(this, args);
            }
        }
	    //印发事件（1.事件参数类2.委托3.声明事件4.引发）
        //关闭释放资源
        private void OnApplicationQuit()
        {
            NotifyServer(MeaasgeType.OffLine);
            //Thread.Sleep(500);
            threadReceive.Abort();
            udpService.Close();
        }

	
	
	}
}
