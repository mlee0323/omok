using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Omok_Server2.Network
{
    public class ServerCore
    {
        public static ServerCore Instance { get; private set; }

        private TcpListener listener;
        private Thread listenThread;
        private bool isRunning = false;
        private List<ClientHandler> clients = new List<ClientHandler>();
        private Dictionary<ClientHandler, Thread> clientThreads = new();
        private readonly Action<string> logCallback;

        public ServerCore(Action<string> logCallback)
        {
            Instance = this;
            this.logCallback = logCallback;
        }

        public void Start(int port = 9999)
        {
            if (isRunning) return;

            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            listenThread = new Thread(ListenLoop);
            listenThread.IsBackground = true;
            listenThread.Start();
            isRunning = true;

            logCallback?.Invoke($"서버 시작됨 (포트 {port})");
        }

        private void ListenLoop()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                logCallback?.Invoke("클라이언트 연결됨");
                
                ClientHandler handler = new ClientHandler(client, logCallback);
                clients.Add(handler);

                Thread t = new Thread(handler.Process)
                {
                    IsBackground = true,
                    Name = $"ClientThread-{clients.Count}"
                };
                clientThreads[handler] = t;
                t.Start();
            }
        }

        public void RemoveClient(ClientHandler client)
        {
            if (clients.Contains(client))
                clients.Remove(client);

            if (clientThreads.TryGetValue(client, out Thread t))
            {
                clientThreads.Remove(client);

                logCallback?.Invoke($"클라이언트 쓰레드 제거: {t.Name}");
            }
        }

    }
}
