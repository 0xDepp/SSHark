using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SSHark
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PortScanner ps = new PortScanner("163.172", 22);
                ps.StartCheckAsync().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an exception {0}", e);
            }

            Console.ReadKey();
        }
    }

    class PortScanner
    {
        string ip { get; set; }
        int port { get; set; }

        public PortScanner(string startIP, int startPort)
        {
            ip = startIP;
            port = startPort;
        }

        public async Task StartCheckAsync()
        {
            string full_ip;

            for (int j = 0; j < 255; j++)
            {
                for (int i = 1; i < 255; i++)
                {
                    full_ip = $"{ip}.{j}.{i}";

                    await Task.Run(() => CheckPort(full_ip));
                }
            }
        }

        public async void CheckPort(string full_ip)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    await tcpClient.ConnectAsync(full_ip, port);
                    Console.WriteLine($"{full_ip} Port {port} open");
                }
                catch (Exception)
                {
                    Console.WriteLine($"{full_ip} Port {port} error / closed");
                }
            }
        }
    }
}
