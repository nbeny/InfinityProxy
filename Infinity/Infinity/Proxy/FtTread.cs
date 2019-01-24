/*
    // InfinityProxy - A C# .NET application
    // Copyright (C) 2018-2019 nbeny
    // <nbeny@student.42.fr>
    // This program is free software: you can redistribute it and/or modify
    // it under the terms of the GNU General Public License as published by
    // the Free Software Foundation, either version 3 of the License, or
    // (at your option) any later version.
    // This program is distributed in the hope that it will be useful,
    // but WITHOUT ANY WARRANTY; without even the implied warranty of
    // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    // GNU General Public License for more details.
    // You should have received a copy of the GNU General Public License
    // along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

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
