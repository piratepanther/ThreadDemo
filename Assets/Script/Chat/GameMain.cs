using UnityEngine;
using System.Collections;
using Common;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Network
{
	public class GameMain : MonoBehaviour
	{
	
	    // Use this for initialization
	    void Start()
	    {
            transform.FindChildByName("ButtonClientEnter").GetComponent<Button>().onClick.AddListener(OnEnterClientButtonClick);
	    } 
        //单击按钮进入客户端发生事件
        private void OnEnterClientButtonClick()
        {
            string serverIP = transform.FindChildByName("InputserverIPForClient").GetComponent<InputField>().text;
            string serverPort = transform.FindChildByName("InputserverPortForClient").GetComponent<InputField>().text;
            int port = int.Parse(serverPort);
            ClientUDPService.instance.Initialized(serverIP, port);
            SceneManager.LoadScene("Chat");
        }
	
	    

	}
}
