using System;
using System.Collections.Generic;
using Omok_Server2.Network;

namespace Omok_Server2.Managers
{
    public class ClientManager
    {
        private static ClientManager instance;
        public static ClientManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ClientManager();
                return instance;
            }
        }

        private Dictionary<string, ClientHandler> clients;

        private ClientManager()
        {
            clients = new Dictionary<string, ClientHandler>();
        }

        public void AddClient(string clientId, ClientHandler client)
        {
            if (!clients.ContainsKey(clientId))
            {
                clients.Add(clientId, client);
            }
        }

        public void RemoveClient(string clientId)
        {
            if (clients.ContainsKey(clientId))
            {
                clients.Remove(clientId);
            }
        }

        public ClientHandler GetClient(string clientId)
        {
            if (clients.ContainsKey(clientId))
            {
                return clients[clientId];
            }
            return null;
        }

        public void Broadcast(string message)
        {
            foreach (var client in clients.Values)
            {
                client.Send(message);
            }
        }
    }
} 