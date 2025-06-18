using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Omok_Server2.Controllers;
using Omok_Server2.Constant;
using Omok_Server2.Helper;
using Omok_Server2.Data;
using Omok_Server2.Network;
using Omok_Network;
using Omok_Server2.Managers;

namespace Omok_Server2
{
    public class ClientHandler
    {
        private TcpClient client;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        private ServerForm form;
        private string userpk;
        private string nickname;
        private readonly Action<string> logCallback;

        public string Nickname { get { return nickname; } }

        public ClientHandler(TcpClient client, Action<string> logCallback)
        {
            this.client = client;
            this.logCallback = logCallback;
            this.stream = client.GetStream();
            this.reader = new StreamReader(stream, Encoding.UTF8);
            this.writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
        }

        private void HandlePacket(string msg)
        {
            string[] tokens = msg.Split('|');
            if (tokens.Length == 0) return;

            string rawCmd = tokens[0];
            
            // 채팅 패킷 처리
            if (rawCmd == "CHAT" && tokens.Length >= 4)
            {
                if (string.IsNullOrEmpty(userpk))
                {
                    userpk = tokens[1];
                    ClientManager.Instance.AddClient(userpk, this);
                }
                ChatController.Instance.HandleChat(tokens[1], tokens[3]);
                return;
            }

            // 이모지 패킷 처리
            if (rawCmd == "EMOJI" && tokens.Length >= 4)
            {
                if (string.IsNullOrEmpty(userpk))
                {
                    userpk = tokens[1];
                    ClientManager.Instance.AddClient(userpk, this);
                }
                ChatController.Instance.HandleEmoji(tokens[1], tokens[3]);
                return;
            }

            CommandType cmd = CommandParser.Parse(rawCmd);
            string category = CommandParser.GetCategory(cmd);
            string response = "";

            switch (category)
            {
                case "Auth":
                    response = AuthController.Handle(cmd, tokens);
                    //if (response.StartsWith("LOGIN_SUCCESS"))

                    break;

                case "Room":
                    userpk = tokens[1];
                    nickname = tokens[2];
                    ClientManager.Instance.AddClient(userpk, this);
                    response = RoomController.Handle(cmd, tokens, this);
                    break;

                case "Game":
                    userpk = tokens[1];
                    nickname = tokens[2];
                    response = GameController.Handle(cmd, tokens, this);
                    break;

                case "History":
                    userpk = tokens[1];
                    response = HistoryController.Handle(cmd, tokens, this);
                    break;

                default:
                    Log("알 수 없는 명령어: " + msg);
                    break;
            }

            if (response != "")
            {
                Log($"응답: {response}");
                writer.WriteLine(response);
                return;
            }
        }

        private void Log(string msg)
        {
            logCallback?.Invoke(msg);
        }

        public void Process()
        {
            try
            {
                while (true)
                {
                    string msg = reader.ReadLine();
                    if (msg == null) break;

                    Log($"수신: {msg}");
                    HandlePacket(msg);
                }
            }
            catch (Exception e)
            {
                // Log(e.ToString());
                Log("클라이언트 통신 오류");
            }
            finally
            {
                Disconnect();
            }
        }

        public void Send(string message, string type = "Unknown")
        {
            if (writer == null || !client.Connected)
            {
                Log("클라이언트가 연결되어 있지 않음");
                return;
            }

            Log($"응답:[{type}] {message}");
            writer.WriteLine(message);
        }

        public string getUserPk()
        {
            return userpk;
        }

        public string getNickname()
        {
            return nickname;
        }

        private void Disconnect()
        {
            writer?.Close();
            reader?.Close();
            stream?.Close();
            client?.Close();

            Log("클라이언트 연결 종료");

            // 방에서 제거하고 방도 비우기
            if (!string.IsNullOrEmpty(userpk))
            {
                var room = RoomManager.GetRoomByUserPk(userpk);
                if (room != null)
                {
                    room.RemoveClient(this);

                    // 아무도 없으면 방 제거
                    if (room.IsEmpty)
                    {
                        RoomManager.RemoveRoom(room.RoomCode);
                        Log($"방 {room.RoomCode} 제거됨 (빈 방)");
                    }
                    else
                    {
                        // 나머지 유저들에게 TEAM_INFO 리프레시 전송
                        // room.BroadcastNotMe();
                    }
                }
            }
            ServerCore.Instance?.RemoveClient(this);
        }

    }
}
