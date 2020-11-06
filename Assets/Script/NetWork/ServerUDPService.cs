using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using Common;
using System;
using System.Threading;
using System.Collections.Generic;

namespace Network
{
    public class ServerUDPService : MonoSingleton<ServerUDPService>
	{
        
        private UdpClient udpService;
        //加event方便使用+=、-=，私有化
        public event EventHandler<MessageArrivedEventArgs> MessageArrived;

        private List<IPEndPoint> allClientEP;

        private Thread threadReceive;
        //创建Socket对象(登录窗口传递服务端地址、端口)

        public override void Init()
        {
            base.Init();
            allClientEP = new List<IPEndPoint>();

        }
        
        public void Initialized(string serverIP, int serverPort)
        {
            DontDestroyOnLoad(gameObject);//加载场景不销毁
                        
            //与服务端连接
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
            //分配端口
            udpService = new UdpClient(serverEP);
            //udpService.Connect(serverEP);
            threadReceive = new Thread(ReceiveChatMessage);
            threadReceive.Start();
            

        }

        //发送数据

        public void SendMessage(ChatMessage msg,IPEndPoint remote)
        {
            byte[] data = msg.ObjectToBytes();
            udpService.Send(data, data.Length, remote);
            //int count = udpService.Send(data, data.Length, remote);
            //Debug.Log(count);

        }


        //接收数据
        private void ReceiveChatMessage()
        {
            while (true)
            {
                IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
                //if (remote == null) continue;
                byte[] data = udpService.Receive(ref remote);
                //if (data == null) continue;
                //解析数据
                ChatMessage msg = ChatMessage.ByteToObject(data);

                //根据消息类型执行相关逻辑

                OnMessageArrived(msg, remote);

                //引发事件
                if (MessageArrived == null) continue;//ThreadCrossHelper
                MessageArrivedEventArgs args = new MessageArrivedEventArgs()
                {
                    ArrivedTime = DateTime.Now,
                    Message = msg
                };
                ThreadCrossHelper.instance.ExecuteOnMainThread(() =>
                {

                    MessageArrived(this, args);


                });
            }
        }
        //印发事件（1.事件参数类2.委托3.声明事件4.引发）
        //关闭释放资源
        private void OnApplicationQuit()
        {
            //NotifyServer(MeaasgeType.OffLine);
            //Thread.Sleep(500);
            threadReceive.Abort();
            udpService.Close();
        }

        private void OnMessageArrived(ChatMessage msg,IPEndPoint remote)
        {
            switch (msg.type)
            {
                case MeaasgeType.OnLine:
                    //添加客户端
                    allClientEP.Add(remote);

                    break;
                case MeaasgeType.OffLine:
                    //移除客户端
                    allClientEP.Remove(remote);

                    break;
                case MeaasgeType.General:
                    //转发数据
//                     foreach (var item in allClientEP)
//                     {
//                         SendMessage(msg, item);
//                     }
                    allClientEP.ForEach(item=>SendMessage(msg, item));

                    break;

            }


        }



	
	    
	
	    
	}
}
