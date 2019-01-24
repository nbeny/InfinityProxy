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

using Infinity.Proxy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Scroller
{
    public class YggUrl
    {
        public string FileName = @"";
        public string True = @"";
        public string False = @"";

        public static void StartScrapper1(ProxyClass proxyClass)
        {
            foreach (string FileName in Directory.GetFiles(False))
            {
                using (StreamReader r = new StreamReader(FileName))
                {
                    //OPENS AND DESERIALIZE JSON FILE
                    string fileurl = r.ReadToEnd();
                    string[] SplitUrl = fileurl.Split('\n');

                    JsonSerializer serializerJson = new JsonSerializer();
                    using (StreamWriter sw = new StreamWriter(True + @"\" + Split[Split.Length - 1]))
                    {
                        using (JsonWriter writer = new JsonTextWriter(sw))
                        {
                            serializerJson.Serialize(writer, CaseIpOeb);
                        }
                    }
                }
            }
        }

        public static void StartScrapper2(ProxyClass proxyClass)
        {
            foreach (string FileName in Directory.GetFiles(False))
            {
                using (StreamReader r = new StreamReader(FileName))
                {
                    //OPENS AND DESERIALIZE JSON FILE
                    string fileurl = r.ReadToEnd();
                    string[] SplitUrl = fileurl.Split('\n');

                    JsonSerializer serializerJson = new JsonSerializer();
                    using (StreamWriter sw = new StreamWriter(True + @"\" + Split[Split.Length - 1]))
                    {
                        using (JsonWriter writer = new JsonTextWriter(sw))
                        {
                            serializerJson.Serialize(writer, CaseIpOeb);
                        }
                    }
                }
            }
        }
    }
}
