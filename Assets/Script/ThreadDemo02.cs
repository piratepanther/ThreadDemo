using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Assets.Script;

public class ThreadDemo02 : MonoBehaviour
{
    public object state { get; set; }
    // Start is called before the first frame update
    void Start()
    {
//         Thread thread1 = new Thread(Func1);
//         thread1.IsBackground

        
        //ThreadPool.QueueUserWorkItem(Func1);
        //ThreadPool.QueueUserWorkItem(Func1);
        //ThreadPool.QueueUserWorkItem((obj) => { Func2(); });
        //ThreadPool.QueueUserWorkItem(obj => { Func2(); });
        //ThreadPool.QueueUserWorkItem(obj => { Func2(); },state);
        
        //同步调用（排队）
        //Bank.GetMoney(1);
        //异步调用（不排队）
        ThreadPool.QueueUserWorkItem(obj => { Bank.GetMoney(1); });
    }

    private void Func1(object obj)
    {
        Func2();
    }
    private void Func2()
    {

    }
    
    
    
    
    
    

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
