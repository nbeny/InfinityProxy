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
