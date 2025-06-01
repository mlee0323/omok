using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Omok_Network
{
    public class NetworkManager
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;
        private volatile bool isReceiving = false;

        public event Action<Packet> OnPacketReceived;

        public void Connect()
        {
            try
            {
                client = new TcpClient(NetworkConfig.Ip, NetworkConfig.Port);
                stream = client.GetStream();

                isReceiving = true;
                receiveThread = new Thread(ReceiveLoop);
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[NetworkManager] 서버 연결 실패: " + ex.Message);
            }
        }

        public void Disconnect()
        {
            isReceiving = false;
            receiveThread?.Join();
            stream?.Close();
            client?.Close();
        }

        private void ReceiveLoop()
        {
            try
            {
                byte[] buffer = new byte[4096];

                while (isReceiving && client.Connected)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read == 0) break;

                    Packet packet = (Packet)Packet.Deserialize(buffer, 0, read);
                    OnPacketReceived?.Invoke(packet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[NetworkManager] 수신 중 오류: " + ex.Message);
            }
        }

        public static Packet SendPacketSync(Packet packet)
        {
            try
            {
                using (TcpClient tempClient = new TcpClient(NetworkConfig.Ip, NetworkConfig.Port))
                using (NetworkStream tempStream = tempClient.GetStream())
                {
                    byte[] data = Packet.Serialize(packet);
                    tempStream.Write(data, 0, data.Length);

                    byte[] buffer = new byte[4096];
                    int read = tempStream.Read(buffer, 0, buffer.Length);
                    if (read == 0)
                        return null;

                    return (Packet)Packet.Deserialize(buffer, 0, read);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[NetworkManager] 패킷 동기 전송 실패: " + ex.Message);
                return null;
            }
        }

        public static async Task SendPacketAsync(Packet packet)
        {
            try
            {
                using (TcpClient tempClient = new TcpClient())
                {
                    await tempClient.ConnectAsync(NetworkConfig.Ip, NetworkConfig.Port);
                    using (NetworkStream tempStream = tempClient.GetStream())
                    {
                        byte[] data = Packet.Serialize(packet);
                        await tempStream.WriteAsync(data, 0, data.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[NetworkManager] 패킷 비동기 전송 실패: " + ex.Message);
            }
        }
    }
}
