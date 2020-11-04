using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Script
{
    public class Bank
    {
        private static int totalmoney = 1;
        private static object locker = new object();

        public static void GetMoney(int val)
        {
            //线程锁 锁定程序执行流程
            //判断该对象locker 的同步块索引是否为-1
            lock (locker)//如果是-1进入程序，再改变索引。（引用类型都有）
            {
                if (totalmoney >= val)
                {
                    Thread.Sleep(1000);
                    totalmoney -= val;
                    Debug.Log("成功" + totalmoney);
                }
                else
                {
                    Debug.Log("失败" + totalmoney);
                }
                //改变索引
            }
            
            
            
        }




    }
}
