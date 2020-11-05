using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System;
using Common;

public class ThreadDemo03 : MonoBehaviour
{
    private Text txtTimer;

    public int second = 120;
    private Thread timerThread;

    private Action action; 
    // Start is called before the first frame update
    void Start()
    {
        txtTimer=GetComponent<Text>();
        timerThread = new Thread(Timer);
        timerThread.Start();

    }

    // Update is called once per frame
    void Timer()
    {
        while(second>0)
        {
            //second--;
            //在辅助线程中访问unity组件属性，会异常。
            //委托 txtTimer.text = string.Format("{0}:{1}",second/60,second%60);
//             action = () =>
//             {
//                 txtTimer.text = string.Format("{0}:{1}", second / 60, second % 60);
//             };
            ThreadCrossHelper.instance.ExecuteOnMainThread(() =>
            {
                second--;
                txtTimer.text = string.Format("{0}:{1}", second / 60, second % 60);
            },3);

            
            Thread.Sleep(1000);
        }
    }

//     private void Update()
//     {
//         if (action != null)
//         {
//             action();
//             action = null;
//         }
//     }

    private void OnApplicationQuit()
    {

        timerThread.Abort();

    }


}
