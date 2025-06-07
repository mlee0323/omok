using System;
using System.Collections.Generic;
using Omok_Network;
using Omok_Server2.Managers;

namespace Omok_Server2.Controllers
{
    public class ChatController
    {
        private static ChatController instance;
        public static ChatController Instance
        {
            get
            {
                if (instance == null)
                    instance = new ChatController();
                return instance;
            }
        }

        private ChatController() { }

        public void HandleChat(string clientId, string message)
        {
            try
            {
                var client = ClientManager.Instance.GetClient(clientId);
                if (client == null) return;

                string chatPacket = Packet.CreateChatPacket(clientId, client.Nickname, message);
                ClientManager.Instance.Broadcast(chatPacket);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chat handling error: {ex.Message}");
            }
        }
    }
} 