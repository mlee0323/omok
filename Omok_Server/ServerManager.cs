using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Pack_Server;
using System.Windows.Forms;
using System.Linq;

namespace Omok_Server
{
    public class ServerManager
    {
        private TcpListener listener;
        private Thread listenThread;
        private int port;
        private bool isRunning;

        private Dictionary<TcpClient, string> clientNicknames = new Dictionary<TcpClient, string>();
        private List<TcpClient> clients = new List<TcpClient>();

        private Form1 form;

        public ServerManager(Form1 form, int port = 9999)
        {
            this.form = form;
            this.port = port;
        }

        public void Start()
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            isRunning = true;

            listenThread = new Thread(ListenForClients);
            listenThread.IsBackground = true;
            listenThread.Start();

            form?.Invoke(new Action(() =>
            {
                MessageBox.Show($"서버가 시작되었습니다. 포트: {port}");
            }));
        }

        public void Stop()
        {
            isRunning = false;
            listener?.Stop();

            foreach (var client in clients)
            {
                client.Close();
            }
            clients.Clear();

            listenThread?.Join();
        }

        private void ListenForClients()
        {
            try
            {
                while (isRunning)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    lock (clients)
                    {
                        clients.Add(client);
                    }

                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
            }
            catch (SocketException ex)
            {
                if (isRunning)
                    form?.Invoke(new Action(() => MessageBox.Show("서버 오류: " + ex.Message)));
            }
        }

        private void BroadcastToClients(Packet packet, TcpClient except = null)
        {
            byte[] data = Packet.Serialize(packet);

            lock (clients)
            {
                foreach (var c in clients)
                {
                    if (c != except && c.Connected)
                    {
                        try
                        {
                            NetworkStream s = c.GetStream();
                            s.Write(data, 0, data.Length);
                        }
                        catch (Exception ex)
                        {
                            form?.Invoke(new Action(() =>
                            {
                                form.LogMessage("클라이언트에게 전송 실패: " + ex.Message);
                            }));
                        }
                    }
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            try
            {
                byte[] buffer = new byte[4096];
                while (isRunning && client.Connected)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    Packet packet = (Packet)Packet.Deserialize(buffer, 0, bytesRead);

                    switch (packet.Type)
                    {
                        case (int)PacketType.로그인:
                            var loginPacket = (Login_server)packet;
                            lock (clientNicknames)
                            {
                                clientNicknames[client] = loginPacket.m_strID;
                            }
                            form?.Invoke(new Action(() =>
                            {
                                form.LogMessage($"로그인: {loginPacket.m_strID}");
                            }));

                            // 접속자 리스트 새로 고침 및 클라이언트에 전송
                            SendUserListToAllClients();
                            break;

                        case (int)PacketType.채팅메시지:
                            var chat = (ChatMessage)packet;
                            form?.Invoke(new Action(() =>
                            {
                                form.LogMessage($"채팅 수신 - {chat.sender}: {chat.message}");
                            }));

                            BroadcastToClients(chat, client);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                form?.Invoke(new Action(() => form.LogMessage("클라이언트 연결 오류: " + ex.Message)));
            }
            finally
            {
                lock (clientNicknames)
                {
                    if (clientNicknames.ContainsKey(client))
                    {
                        string nick = clientNicknames[client];
                        clientNicknames.Remove(client);
                        form?.Invoke(new Action(() => form.LogMessage($"{nick} 님이 연결 종료했습니다.")));
                    }
                }
                lock (clients)
                {
                    clients.Remove(client);
                }
                client.Close();

                // 접속자 리스트 갱신
                SendUserListToAllClients();
            }
        }

        private void SendUserListToAllClients()
        {
            UserListPacket userListPacket;
            lock (clientNicknames)
            {
                userListPacket = new UserListPacket
                {
                    nicknames = clientNicknames.Values.ToList()
                };
            }

            BroadcastToClients(userListPacket);
        }
    }
}
