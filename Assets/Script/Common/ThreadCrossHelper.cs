using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ThreadCrossHelper : MonoSingleton<ThreadCrossHelper>
	{

        public override void Init()
        {
            base.Init();
            actionList=new List<DelayItem>();
        }
        
        class DelayItem
        {
            public Action CurrentAction { get; set; }

            public DateTime Time { get; set; }
        }

        //         private List<Action> action;
        //         private List<float> timeList;


        private List<DelayItem> actionList;

        // Update is called once per frame
        void Update()
        {
//             if (action != null)
//             {
//                 action();
//                 action = null;
//             }
            lock (actionList)
            {
                for (int i = actionList.Count-1; i >0; i--)
                {
                    //如果发现到达执行时间，则执行移除
                    if (actionList[i].Time<=DateTime.Now)
                    {
                        //执行
                        actionList[i].CurrentAction();
                        //移除
                        actionList.RemoveAt(i);
                    }
                }

            }
            
            
        }
        /// <summary>
        /// 在主线程中执行的方法
        /// </summary>
        /// <param name="action"></param>
        /// <param name="delay"></param>
        public void ExecuteOnMainThread(Action action, float delay = 0)
        {
            lock (actionList)
            {
                var item = new DelayItem()
                {
                    CurrentAction = action,
                    Time = DateTime.Now.AddSeconds(delay)

                };
                actionList.Add(item);

            }
            
            
//             if (action == null)
//             {
//                 this.action = action;
//             }
//             else
//             {
//                 this.action += action;
//             }

        }
	}
}
