using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreadDemo01 : MonoBehaviour
{

    private ManualResetEvent signal;
    private Thread thread03;


    // 调度脚本生命周期的线程称之为“主线程”
    // 自己创建的线程成为辅助线程
    void Start()
    {
        //信号灯，线程事件。创建信号灯对象，成员变量方便调用
        signal = new ManualResetEvent(false);
        
        
//         //1.通过方法创建线程
//         Thread thread01 = new Thread(Func1);
//         Thread thread02 = new Thread(Func2);
//         //2.开启线程,传递参数
//         thread01.Start();
//         thread02.Start(2);

        thread03 = new Thread(Func3);
        thread03.Start();

    }

    private void Func2(object obj)
    {
        int n = (int)obj;
        print("thread02");
        print(n);
    }

    private void Func1()
    {
        print("thread01");
        for (int i = 0; i < 5; i++)
        {
            //等一下
            signal.WaitOne();
            Thread.Sleep(1000);
            print(i);
        }
        
        
        
    }


    private void Func3(object obj)
    {
        //signal.WaitOne();
        int i = 0;
        print("thread03");
        while (true)
        {
            //if()break;
            i++;
            print(i);
            Thread.Sleep(1000);
        }         
    }


    private void OnGUI()
    {
        if (GUILayout.Button("暂停"))
        {
            //红灯
            signal.Reset();

        }
        if (GUILayout.Button("恢复"))
        {
            //绿灯
            signal.Set();

        }


    }

    private void OnApplicationQuit()
    {
        thread03.Abort();//本质抛异常，不建议用，耗性能

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
