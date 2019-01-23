using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infinity.Scroller;
using OpenQA.Selenium.Chrome;

namespace Infinity.Proxy
{
    public class FtThread
    {
        public FtThread(ProxyClass ProxyClass)
        {
            Proxy = ProxyClass;
        }

        public ProxyClass Proxy { set; get; }

        public int EndThread1 = 0;
        public int EndThread2 = 0;

        public void Ft_Thread1()
        {
            Console.WriteLine(Thread.GetDomain());

            for (int i = 0; i < 10; i++)
            {
                if (i == 0)
                {
                    Thread thr = Thread.CurrentThread;
                    Console.WriteLine(i.ToString() + " New Thread2 activat");

                    ProxyClass ProxyClass = ProxyClass.NewDriverProxy(Proxy);
                    YggUrl.StartScrapper1(ProxyClass);

//                  C:\Users\ynebo\Documents\InfinityProxy\Infinity\Infinity\Proxy\FtTread.cs

                 EndThread1 = 1;
                }
                else
                    EndThread1 = 2;
            }
        }

        public void Ft_Thread2()
        {
            Console.WriteLine(Thread.GetDomain());

            for (int i = 0; i < 10; i++)
            {
                if (i == 0)
                {
                    Thread thr = Thread.CurrentThread;
                    Console.WriteLine(i + " New Thread2 activate");

                    ProxyClass ProxyClass = ProxyClass.NewDriverProxy(Proxy);
                    YggUrl.StartScrapper2(ProxyClass);



                    EndThread2 = 1;
                }
                else
                    EndThread2 = 2;
            }
        }
    }
}
