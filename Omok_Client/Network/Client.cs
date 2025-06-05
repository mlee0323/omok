using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Omok_Client.Network
{
    public static class Client
    {
        private static TcpClient tcpClient;
        private static StreamReader reader;
        private static StreamWriter writer;
        public static bool IsConnected
        {
            get
            {
                return tcpClient != null && tcpClient.Connected;
            }
        }

        public static bool Connect(string host = "127.0.0.1", int port = 9999)
        {
            try
            {
                tcpClient = new TcpClient(host, port);
                NetworkStream stream = tcpClient.GetStream();
                reader = new StreamReader(stream, Encoding.UTF8);
                writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Disconnect()
        {
            writer?.Close();
            reader?.Close();
            tcpClient?.Close();
        }

        public static void Send(string message)
        {
            writer.WriteLine(message);
        }

        public static string Receive()
        {
            return reader.ReadLine();
        }
    }
}
