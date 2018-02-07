using System;
using System.Collections;
using System.Timers;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// 名称：对象连接池抽象类。
    /// 版本：V1.0
    /// 创建：Fightop Lin
    /// 日期：2005-12-18
    /// 描述：继承并实现 Create() Validate() Expire() 完成特定连接池管理
    ///
    /// Log ：1
    /// 版本：
    /// 修改：
    /// 日期：
    /// 描述：
    ///       
    /// </summary>
    public abstract class ObjectPool
    {
        // 最后一次从对象池中取出对象的时间
        private long _lastCheckOut;

        // 已经取出的对象
        private static Hashtable locked;
        // 当前可用对象
        private static Hashtable unlocked;

        // 对象超时清除时间
        private static long GARBAGE_INTERVAL = 90 * 1000; // 90 秒

        static ObjectPool()
        {
            locked   = Hashtable.Synchronized(new Hashtable());
            unlocked = Hashtable.Synchronized(new Hashtable());
        }

        internal ObjectPool()
        {
            _lastCheckOut = DateTime.Now.Ticks;

            // 创建一个定时器清楚超时对象
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Enabled  = true;
            aTimer.Interval = GARBAGE_INTERVAL;
            aTimer.Elapsed += new 
                System.Timers.ElapsedEventHandler(CollectGarbage);
        }

        // 创建对象，由继承类实现
        protected abstract object Create();
        // 验证对象，由继承类实现
        protected abstract bool Validate(object o);
        // 清除对象，由继承类实现
        protected abstract void Expire(object o);

        // 从池在取对象
        internal object GetObjectFromPool()
        {
            long now      = DateTime.Now.Ticks;
            _lastCheckOut = now;
            object o      = null;

            lock(this)
            {
                try
                {
                    foreach(DictionaryEntry myEntry in unlocked)
                    {
                        o = myEntry.Key;
                        if(Validate(o))
                        {// 有效，则取出
                            unlocked.Remove(o);
                            locked.Add(o,now);

                            return(o);
                        }
                        else
                        {// 无效，则清除
                            unlocked.Remove(o);
                            Expire(o);
                            o = null;
                        }
                    } // end foreach
                }
                catch ( Exception ) {}

                o = Create();
                locked.Add(o,now);

            }// end lock
            return(o);
        }

        // 将对象归还到池中
        internal void ReturnObjectToPool(object o)
        {
            if (null != o)
            {
                lock(this)
                {
                    locked.Remove(o);
                    unlocked.Add(o,DateTime.Now.Ticks);
                }
            }
        }

        // 定时清除超时对象
        private void CollectGarbage(object sender,
            System.Timers.ElapsedEventArgs ea)
        {
            lock(this)
            {
                object o;
                long now = DateTime.Now.Ticks;
                IDictionaryEnumerator e = unlocked.GetEnumerator();

                try
                {
                    while(e.MoveNext())
                    {
                        o = e.Key;

                        if((now-((long)unlocked[o])) > GARBAGE_INTERVAL)
                        {// 超时，则清除
                            unlocked.Remove(o);
                            Expire(o);
                            o = null;
                        }
                    }// end while
                }
                catch(Exception){}
            }// end lock
        }

    }
}
