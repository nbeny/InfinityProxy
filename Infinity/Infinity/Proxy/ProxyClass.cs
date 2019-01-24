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
using System.Globalization;
using System.Net;
using System.Threading;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Infinity.Proxy
{
    public class ProxyClass
    {
        public ProxyClass()
        {
            ListIp = new List<string>();
            ListPort = new List<string>();
        }

        public ChromeDriver Driver { set; get; }

        public List<string> ListIp { set; get; }
        public List<string> ListPort { set; get; }

        public static int I { set; get; } = 0;
        public int Ip { set; get; } = 0;
        public int Port { set; get; } = 0;

        public string urlProxy = "http://spys.one/free-proxy-list/FR/";
        public string urlCheck = "http://www.whatsmyip.org/more-info-about-you/";
        //html/body/section[1]/div/div[2]/div/div[2]/div/table/tbody/tr[1]/td[1]
        //html/body/section[1]/div/div[2]/div/div[2]/div/table/tbody/tr[2]/td[1]

        public string X500 = "//*[@id=\"xpp\"]/option[6]";

        public string XIpAdressStart = "/html/body/table[2]/tbody/tr[4]/td/table/tbody/tr[";
        public string XIpAdressEnd = "]/td[1]/font[2]/text()[1]";

        //html/body/section[1]/div/div[2]/div/div[2]/div/table/tbody/tr[1]/td[2]
        public string XPortStart = "/html/body/table[2]/tbody/tr[4]/td/table/tbody/tr[";
        public string XPortEnd = "]/td[1]/font[2]/text()[2]";

        public string XhttpsStart = "/html/body/table[2]/tbody/tr[4]/td/table/tbody/tr[";
        public string XhttpsEnd = "]/td[2]/a/font[2]";

        public string XhiaStart = "/html/body/table[2]/tbody/tr[4]/td/table/tbody/tr[";
        public string XhiaEnd = "]/td[3]/font";

        public string XLatencyStart = "/html/body/table[2]/tbody/tr[4]/td/table/tbody/tr[";
        public string XLatencyEnd = "]/td[6]/font";

        public int NbProxy = 0;

        public static ProxyClass GetIpAndPort(ProxyClass ProxyClass, ChromeDriver Driver)
        {
            Driver.Navigate().GoToUrl(ProxyClass.urlProxy);

            IWebElement X500 = Driver.FindElementByXPath(ProxyClass.X500);
            X500.Click();

            IWebElement X501 = Driver.FindElementByXPath(ProxyClass.X500);
            X501.Click();

            var page = Driver.PageSource;
            var Doc = new HtmlDocument();
            Doc.LoadHtml(page);
            Thread.Sleep(2000);

            for (int i = 4; i < 404; i++)
            {
                var Https = Doc.DocumentNode.SelectNodes(ProxyClass.XhttpsStart + i.ToString() + ProxyClass.XhttpsEnd);
                if (Https != null)
                {
                    var Hia = Doc.DocumentNode.SelectNodes(ProxyClass.XhiaStart + i.ToString() + ProxyClass.XhiaEnd);
                    var Latency = Doc.DocumentNode.SelectNodes(ProxyClass.XLatencyStart + i.ToString() + ProxyClass.XLatencyEnd);
                    float lat = float.Parse(Latency[0].InnerText.Trim(), CultureInfo.InvariantCulture.NumberFormat);
                    if (Https[0].InnerText.Contains("S") && Hia[0].InnerText.Contains("HIA") && lat < 1)
                    {
                        var IpAdress = Doc.DocumentNode.SelectNodes(ProxyClass.XIpAdressStart + i.ToString() + ProxyClass.XIpAdressEnd);
                        if (!IpAdress[0].InnerText.Contains("5.135.20.71"))
                        {
                            ProxyClass.ListIp.Add(IpAdress[0].InnerText);
                            var IpPort = Doc.DocumentNode.SelectNodes(ProxyClass.XPortStart + i.ToString() + ProxyClass.XPortEnd);
                            ProxyClass.ListPort.Add(IpPort[0].InnerText);
                        }
                    }
                }
            }

            ProxyClass.NbProxy = ProxyClass.ListIp.Count;

            return ProxyClass;
        }

        public static ProxyClass ProxyListCreation(ProxyClass ProxyClass)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--user-agent=Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36");
            options.AddArgument("--mute-audio");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--incognito");
            //options.BinaryLocation = "/Users/nbeny/Desktop/Google Chrome.app/Contents/MacOS/Google Chrome";
            ChromeDriver Driver = new ChromeDriver(options);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(60);

            Driver.Navigate().GoToUrl(ProxyClass.urlCheck);
            Thread.Sleep(20);

            ProxyClass = ProxyClass.GetIpAndPort(ProxyClass, Driver);

            Driver.Close();

            options.AddArguments("--proxy-server=" + ProxyClass.ListIp[1] + ":" + ProxyClass.ListPort[1]);

            /*
            //Create a new proxy object
            var proxy = new WebProxy();
            //Set the http proxy value, host and port.
            proxy.Address = ProxyClass.IP + ":" + ProxyClass.Port;
            //Set the proxy to the Chrome options
            ChromeOptions.Proxy = proxy;
            */

            Driver = new ChromeDriver(options);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(60);

            ProxyClass.Driver = Driver;

            ProxyClass = TestProxy(ProxyClass);

            ProxyClass.Driver.Close();

            return ProxyClass;
        }

        public static ProxyClass ChangeProxy(ProxyClass ProxyClass, int ip, int port)
        {
            ProxyClass.Driver.Close();

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--user-agent=Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36");
            options.AddArgument("--mute-audio");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--incognito");
            //options.BinaryLocation = "/Users/nbeny/Desktop/Google Chrome.app/Contents/MacOS/Google Chrome";
            options.AddArguments("--proxy-server=" + ProxyClass.ListIp[ip] + ":" + ProxyClass.ListPort[port]);

            ChromeDriver Driver = new ChromeDriver(options);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(60);

            ProxyClass.Driver = Driver;

            return ProxyClass;
        }

        public static ProxyClass TestProxy(ProxyClass Proxy)
        {
            string X = "http://example.com/";
            // Navigates to the url
            Proxy.Driver.Navigate().GoToUrl(X);

            var page = Proxy.Driver.PageSource;
            var DocRelated = new HtmlDocument();
            DocRelated.LoadHtml(page);
            var quitAccessSite = DocRelated.DocumentNode.SelectNodes("//*[@id=\"main-message\"]/h1/span");
            var quitCapcha = DocRelated.DocumentNode.SelectNodes("//*[@id=\"cf-error-details\"]/div[1]/h2/span");
            if (quitAccessSite != null || quitCapcha != null)
            {
                if (quitAccessSite != null)
                    Console.WriteLine(quitAccessSite[0].InnerText);
                if (quitCapcha != null)
                    Console.WriteLine(quitCapcha[0].InnerText);
                Console.WriteLine("delete: [" + Proxy.ListIp[Proxy.Ip] + ":" + Proxy.ListPort[Proxy.Port] + "]");
                Proxy.ListIp.RemoveAt(Proxy.Ip);
                Proxy.ListPort.RemoveAt(Proxy.Port);
                Proxy.NbProxy--;
                if (Proxy.Ip >= Proxy.NbProxy)
                {
                    Proxy.Ip = 0;
                    Proxy.Port = 0;
                    return Proxy;
                }
            }
            if (Proxy.Ip >= Proxy.NbProxy)
                return Proxy;
            Proxy.Ip++;
            Proxy.Port++;
            ProxyClass.ChangeProxy(Proxy, Proxy.Ip, Proxy.Port);
            TestProxy(Proxy);
            return Proxy;
        }

        public static ProxyClass NewDriverProxy(ProxyClass Proxy)
        {
            ProxyClass ProxyClass = new ProxyClass();

            foreach (var item in Proxy.ListIp)
            {
                ProxyClass.ListIp.Add(item);
            }
            foreach (var item in Proxy.ListPort)
            {
                ProxyClass.ListPort.Add(item);
            }

            ProxyClass.NbProxy = Proxy.NbProxy;
            ProxyClass.Ip = 0;
            ProxyClass.Port = 0;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--user-agent=Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36");
            options.AddArgument("--mute-audio");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--incognito");
            //options.BinaryLocation = "/Users/nbeny/Desktop/Google Chrome.app/Contents/MacOS/Google Chrome";
            options.AddArguments("--proxy-server=" + ProxyClass.ListIp[0] + ":" + ProxyClass.ListPort[0]);

            ChromeDriver Driver = new ChromeDriver(options);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(60);

            ProxyClass.Driver = Driver;

            return ProxyClass;
        }
    }
}
