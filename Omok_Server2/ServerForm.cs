using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using Omok_Network;

namespace Omok_Server2
{
    public partial class ServerForm : MetroForm
    {
        private TcpListener listener;
        private bool isServerRunning;
        private Dictionary<string, ThreadWrapper> threadDict = new Dictionary<string, ThreadWrapper>();
        private BlockingCollection<ValueTuple<TcpClient, Packet>> packetQueue;

        private System.Windows.Forms.Timer monitorTimer;

        public class ThreadWrapper
        {
            public string Name { get; set; }
            public Thread Thread { get; set; }
            public ThreadStart StartAction { get; set; }
            public string Status { get; set; }
        }

        public ServerForm()
        {
            InitializeComponent();
            isServerRunning = false;
            txtLog.Enabled = false;

            InitThreadList();
            InitMonitorTimer();
        }

        private void btn_server_Click(object sender, EventArgs e)
        {
            if (!isServerRunning)
                serverStart();
            else
                serverStop();
        }

        private void serverStart()
        {
            try
            {
                killAllThreads(); // 기존 스레드 정리

                packetQueue = new BlockingCollection<(TcpClient, Packet)>();
                isServerRunning = true;
                btn_server.Text = "서버 중지";

                listener = new TcpListener(IPAddress.Parse(NetworkConfig.Ip), NetworkConfig.Port);
                listener.Start();

                LogMessage($"서버가 시작되었습니다. {NetworkConfig.Ip}:{NetworkConfig.Port}");

                InitMonitorTimer(); // ← monitorTimer 재생성
                StartThread("listenerThread");
                StartThread("authThread");
                monitorTimer.Start();
            }
            catch (Exception ex)
            {
                LogMessage("서버 시작 중 오류 발생: " + ex.Message);
            }
        }


        private void serverStop()
        {
            try
            {
                killAllThreads();
                monitorTimer?.Stop();
                monitorTimer?.Dispose();
                monitorTimer = null;

                isServerRunning = false;
                btn_server.Text = "서버 시작";

                listener?.Stop();
                listener = null;

                LogMessage("서버가 중지되었습니다.");
            }
            catch (Exception ex)
            {
                LogMessage("서버 중지 중 오류 발생: " + ex.Message);
            }
        }

        private void killAllThreads()
        {
            listener?.Stop(); // listener 종료
            packetQueue?.CompleteAdding();

            foreach (var thread in threadDict.Values)
            {
                if (thread.Thread != null && thread.Thread.IsAlive)
                {
                    Thread t = thread.Thread;
                    Task.Run(() => t.Join()); // UI 멈춤 방지
                }

                thread.Status = "OFF";
                UpdateThreadListView(thread.Name, "OFF");
            }
        }

        private void InitThreadList()
        {
            AddThread("listenerThread", ListenerWorker);
            AddThread("authThread", AuthenticateWorker);
        }

        private void AddThread(string name, ThreadStart action)
        {
            var wrapper = new ThreadWrapper
            {
                Name = name,
                StartAction = action,
                Thread = null,
                Status = "OFF"
            };

            threadDict[name] = wrapper;
            threadList.Items.Add(new ListViewItem(new[] { name, "OFF" }));
        }

        private void StartThread(string name)
        {
            if (!isServerRunning || !threadDict.ContainsKey(name)) return;

            var wrapper = threadDict[name];

            if (wrapper.Thread == null || !wrapper.Thread.IsAlive)
            {
                wrapper.Thread = new Thread(wrapper.StartAction);
                wrapper.Thread.IsBackground = true;
                wrapper.Thread.Start();
                wrapper.Status = "RUNNING";
                UpdateThreadListView(name, "RUNNING");
            }
        }

        private void InitMonitorTimer()
        {
            monitorTimer = new System.Windows.Forms.Timer();
            monitorTimer.Interval = 3000;
            monitorTimer.Tick += (s, e) => MonitorThreads();
        }

        private void MonitorThreads()
        {
            if (!isServerRunning) return;

            foreach (var kvp in threadDict)
            {
                var name = kvp.Key;
                var thread = kvp.Value;

                if (thread.Thread == null || !thread.Thread.IsAlive)
                {
                    thread.Status = "OFF";
                    UpdateThreadListView(name, "OFF");
                    StartThread(name);
                }
            }
        }

        private void UpdateThreadListView(string threadName, string status)
        {
            SafeInvoke(() =>
            {
                foreach (ListViewItem item in threadList.Items)
                {
                    if (item.Text == threadName)
                    {
                        item.SubItems[1].Text = status;
                        break;
                    }
                }
            });
        }

        private void ListenerWorker()
        {
            try
            {
                while (isServerRunning)
                {
                    TcpClient client = listener.AcceptTcpClient();

                    Task.Run(() =>
                    {
                        try
                        {
                            NetworkStream stream = client.GetStream();
                            byte[] buffer = new byte[4096];
                            int read = stream.Read(buffer, 0, buffer.Length);
                            if (read <= 0)
                            {
                                client.Close(); // 연결 끊어진 경우엔 닫고 종료
                                return;
                            }

                            Packet packet = (Packet)Packet.Deserialize(buffer, 0, read);
                            if (!packetQueue.IsAddingCompleted)
                                packetQueue.Add((client, packet));
                        }
                        catch (Exception ex)
                        {
                            LogMessage($"[ListenerWorker 오류] {ex.Message}");
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                LogMessage($"[ListenerWorker 종료] {ex.Message}");
            }
        }

        private void AuthenticateWorker()
        {
            foreach (var item in packetQueue.GetConsumingEnumerable())
            {
                var client = item.Item1;
                var packet = item.Item2;

                if (client == null || packet == null)
                {
                    LogMessage("[AuthThread] 클라이언트나 패킷이 null입니다.");
                    continue;
                }

                if (client.Connected == false)
                {
                    LogMessage("[AuthThread] 클라이언트 연결이 끊어졌습니다.");
                    continue;
                }
                if (packet.Type != PacketType.Authenticate)
                {
                    LogMessage($"[AuthThread] 처리 대상 아님: {packet.Type}");
                    client?.Close(); // 연결은 닫아줌
                    continue;
                }

                try
                {
                    Packet response = Authentication.HandlePacket(packet, LogMessage);
                    if (response == null)
                    {
                        LogMessage("HandlePacket 결과가 null입니다. 응답 전송 생략.");
                        return;
                    }

                    byte[] data = Packet.Serialize(response);
                    if (client.Connected)
                    {
                        using (NetworkStream stream = client.GetStream())
                        {
                            stream.Write(data, 0, data.Length);
                            stream.Flush();
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogMessage("[AuthThread 오류] " + ex.Message);
                }
                finally
                {
                    Thread.Sleep(50);
                    client?.Close();
                }
            }
        }


        public void LogMessage(string msg)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string formattedMsg = $"[{timestamp}] {msg}";

            SafeInvoke(() =>
            {
                txtLog.AppendText(formattedMsg + Environment.NewLine);
            });
        }

        private void SafeInvoke(Action action)
        {
            if (InvokeRequired) Invoke(action);
            else action();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            serverStop();
            Close();
        }
    }
}
