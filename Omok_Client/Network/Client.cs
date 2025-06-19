// Omok_Client/Network/Client.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Omok_Client.Network
{
    public static class Client
    {
        // recvLock 동기화를 위해 사용
        internal static readonly object recvLock = new object();

        private static TcpClient tcpClient;
        private static StreamReader reader;
        private static StreamWriter writer;

        private static readonly string host = "127.0.0.1";
        private static readonly int port = 9999;

        // 서버 연결 상태를 반환
        public static bool IsConnected => tcpClient != null && tcpClient.Connected;

        // 서버에 연결
        public static bool Connect()
        {
            try
            {
                tcpClient = new TcpClient(host, port);
                NetworkStream stream = tcpClient.GetStream();
                reader = new StreamReader(stream, Encoding.UTF8);
                writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
                return true;
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"[Connect] SocketException – 코드: {ex.SocketErrorCode}, 메시지: {ex.Message}");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Connect] 예외 발생: {ex.GetType().Name} – {ex.Message}");
            }
            return false;
        }

        // 서버 연결을 종료
        public static void Disconnect()
        {
            try { writer?.Close(); } catch { }
            try { reader?.Close(); } catch { }
            try { tcpClient?.Close(); } catch { }
        }

        // 메시지를 서버로 전송
        public static void Send(string message)
        {
            if (writer == null) throw new InvalidOperationException("Client not connected");
            writer.WriteLine(message);
        }

        // 서버로부터 한 줄을 읽어옴
        public static string Receive()
        {
            lock (recvLock)
            {
                return reader.ReadLine();
            }
        }

        // 메시지를 보내고, 바로 한 줄 응답을 반환
        public static string Request(string message)
        {
            lock (recvLock)
            {
                Send(message);
                return Receive();
            }
        }

        // 메시지를 보내고, endTag를 만날 때까지 모든 응답 라인을 parser로 파싱하여 배열로 반환
        public static T[] RequestResponse<T>(string request, string endTag, Func<string, T> parser)
        {
            lock (recvLock)
            {
                Send(request);
                var list = new List<T>();
                string msg;
                while ((msg = Receive()) != null)
                {
                    if (msg == endTag) break;
                    list.Add(parser(msg));
                }
                return list.ToArray();
            }
        }

        public static string GetHost() { return host; }

        public static int GetPort() { return port; }

    }
}
