# coding=utf-8

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
