﻿using UnityEngine;
using Common;
using UnityEngine.UI;
using System;

namespace Network
{
	public class ChatServer : MonoBehaviour
	{
        //private InputField messageInput, nameInput;
        private Text ChatMessageText;
        
        // Start is called before the first frame update
	    void Start()
	    {
//             transform.FindChildByName("Send").GetComponent<Button>().onClick.AddListener(OnSendMessageButtonClick);
//             messageInput = transform.FindChildByName("SentContent").GetComponent<InputField>();
//             nameInput = transform.FindChildByName("NameInput").GetComponent<InputField>();
            //注册事件
            ServerUDPService.instance.MessageArrived += DisplayMessage;
            ChatMessageText = transform.FindChildByName("ChatMessageText").GetComponent<Text>();

	    }

        private void DisplayMessage(object sender, MessageArrivedEventArgs e)
        {

            ChatMessageText.text += string.Format("{0}---{1}---{2}\n", DateTime.Now,e.Message.SenderName,e.Message.Content);

            //e.ArrivedTime)+"/n"+
        }

//         private void OnSendMessageButtonClick()
//         {
//             //数据验证.....
//             ChatMessage msg = new ChatMessage()
//             {
//                 type=MeaasgeType.General,
//                 SenderName=nameInput.text,
//                 Content=messageInput.text
// 
//             };
// 
//             ClientUDPService.instance.SendMessage(msg);
// 
//         }
	
	    
	}
}
