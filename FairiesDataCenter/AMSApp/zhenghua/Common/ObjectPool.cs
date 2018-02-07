using System;
using System.Collections;
using System.Timers;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// ���ƣ��������ӳس����ࡣ
    /// �汾��V1.0
    /// ������Fightop Lin
    /// ���ڣ�2005-12-18
    /// �������̳в�ʵ�� Create() Validate() Expire() ����ض����ӳع���
    ///
    /// Log ��1
    /// �汾��
    /// �޸ģ�
    /// ���ڣ�
    /// ������
    ///       
    /// </summary>
    public abstract class ObjectPool
    {
        // ���һ�δӶ������ȡ�������ʱ��
        private long _lastCheckOut;

        // �Ѿ�ȡ���Ķ���
        private static Hashtable locked;
        // ��ǰ���ö���
        private static Hashtable unlocked;

        // ����ʱ���ʱ��
        private static long GARBAGE_INTERVAL = 90 * 1000; // 90 ��

        static ObjectPool()
        {
            locked   = Hashtable.Synchronized(new Hashtable());
            unlocked = Hashtable.Synchronized(new Hashtable());
        }

        internal ObjectPool()
        {
            _lastCheckOut = DateTime.Now.Ticks;

            // ����һ����ʱ�������ʱ����
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Enabled  = true;
            aTimer.Interval = GARBAGE_INTERVAL;
            aTimer.Elapsed += new 
                System.Timers.ElapsedEventHandler(CollectGarbage);
        }

        // ���������ɼ̳���ʵ��
        protected abstract object Create();
        // ��֤�����ɼ̳���ʵ��
        protected abstract bool Validate(object o);
        // ��������ɼ̳���ʵ��
        protected abstract void Expire(object o);

        // �ӳ���ȡ����
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
                        {// ��Ч����ȡ��
                            unlocked.Remove(o);
                            locked.Add(o,now);

                            return(o);
                        }
                        else
                        {// ��Ч�������
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

        // ������黹������
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

        // ��ʱ�����ʱ����
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
                        {// ��ʱ�������
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
