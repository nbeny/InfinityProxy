using System;
using System.Threading;
using Infinity.Proxy;

namespace Infinity
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var ProxyClass = new ProxyClass();
                ProxyClass = ProxyClass.ProxyListCreation(ProxyClass);

                FtThread thr1 = new FtThread(ProxyClass);
                FtThread thr2 = new FtThread(ProxyClass);

                Thread tid1 = new Thread(new ThreadStart(thr1.Ft_Thread1));
                Thread tid2 = new Thread(new ThreadStart(thr2.Ft_Thread2));

                tid1.Start();
                tid2.Start();

                while(true)
                {
                    Thread.Sleep(5000);
                    if (thr1.EndThread1 == 1)
                    {
                        tid1.Abort();
                        thr1.EndThread1 = 2;
                    }
                    if (thr2.EndThread2 == 1)
                    {
                        tid2.Abort();
                        thr2.EndThread1 = 2;
                    }
                    if (thr1.EndThread1 == 2 && thr2.EndThread2 == 2)
                        break;
                }

            }
            catch
            {

            }
        }
    }
}
