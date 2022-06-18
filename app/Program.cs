using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace WebServerCert4
{
    class Prop
    {
        public string task;
        public string data;
    }
    class Program
    {
        static void Main(string[] args)
        {
            OpenStandardStreamIn();
        }
        static private string GetCertsJsonString()
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            List<X509Certificate2> certs = new List<X509Certificate2>();
            foreach (X509Certificate2 certificate in store.Certificates)
            {
                certs.Add(certificate);
            }
            return System.Text.Json.JsonSerializer.Serialize(certs);
        }
        async static public void WriteToFile(string data)
        {
            using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\test\\data.txt", true))
            {
                await writer.WriteLineAsync($"[{DateTime.Now.ToString("hh:mm:ss")}] {data}");
            }
        }

        private static void OpenStandardStreamIn()
        {
            using (Stream stdin = Console.OpenStandardInput())
            {
                using (Stream stdout = Console.OpenStandardOutput())
                {
                    byte[] bytes = new byte[32768];
                    int outputLength = stdin.Read(bytes, 0, 32768);
                    char[] chars = Encoding.UTF8.GetChars(bytes, 4, outputLength);
                    try
                    {
                        List<Prop> list = JsonConvert.DeserializeObject<List<Prop>>(new string(chars));
                        string task = list[0].task;
                        string data = list[0].data;
                        WriteToFile("Task: " + task);
                        if(task == "get_certs")
                        {
                            OpenStandardStreamOut(GetCertsJsonString(), stdout);
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteToFile(ex.Message);
                        throw;
                    }
                }
            }
        }
        private static void OpenStandardStreamOut(string stringData, Stream stdout)
        {
            // We need to send the 4 btyes of length information
            string msgdata = "{\"data\":\"" + GetUnicodeString(new string(stringData.Where(c => !char.IsControl(c)).ToArray())) + "\"}";
            int DataLength = msgdata.Length;
            stdout.WriteByte((byte)((DataLength >> 0) & 0xFF));
            stdout.WriteByte((byte)((DataLength >> 8) & 0xFF));
            stdout.WriteByte((byte)((DataLength >> 16) & 0xFF));
            stdout.WriteByte((byte)((DataLength >> 24) & 0xFF));
            //Available total length : 4,294,967,295 ( FF FF FF FF )
            Console.Write(msgdata);
        }
        private static string GetUnicodeString(string s)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in s)
            {
                sb.Append("\\u");
                sb.Append(String.Format("{0:x4}", (int)c));
            }
            return sb.ToString();
        }
    }
}
