using System;
using System.Collections.Generic;
using System.Linq;
using Omok_Server2.Network;

namespace Omok_Server2.Data
{
    public static class RoomManager
    {
        private static readonly Dictionary<string, Room> rooms = new();
        private static readonly Random random = new();

        public static string CreateRoom(ClientHandler host, int maxPlayers)
        {
            string code = GenerateUniqueCode();
            Room newRoom = new Room(code, host, Clamp(maxPlayers, 2, 4));
            if (newRoom == null)
                return null;

            rooms[code] = newRoom;
            return newRoom.RoomCode;
        }

        public static Room? GetRoom(string code)
        {
            if (rooms.TryGetValue(code, out Room room))
                return room;
            return null;
        }

        public static Room? GetRoomByClient(ClientHandler client)
        {
            return rooms.Values.FirstOrDefault(room => room.Clients.Contains(client));
        }

        public static Room? GetRoomByUserPk(string userpk)
        {
            return rooms.Values.FirstOrDefault(room =>
                room.Clients.Any(c => c.getUserPk() == userpk));
        }



        public static Room? JoinRandom(ClientHandler client)
        {
            foreach (Room room in rooms.Values)
            {
                if (!room.IsFull)
                {
                    bool ok = room.AddClient(client);
                    if (ok)
                        return room;
                }
            }
            return null;
        }

        public static Room? JoinByCode(string code, ClientHandler client)
        {
            if (!rooms.ContainsKey(code)) return null;

            Room room = rooms[code];
            return room.AddClient(client) ? room : null;
        }

        public static void RemoveRoom(string code)
        {
            if (rooms.ContainsKey(code))
                rooms.Remove(code);
        }

        public static void RemoveEmptyRooms()
        {
            var empty = rooms.Where(r => r.Value.IsEmpty).Select(r => r.Key).ToList();
            foreach (var code in empty)
                rooms.Remove(code);
        }

        private static string GenerateUniqueCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string code;
            do
            {
                code = new string(Enumerable.Repeat(chars, 6)
                    .Select(c => c[random.Next(c.Length)]).ToArray());
            } while (rooms.ContainsKey(code));
            return code;
        }

        public static IReadOnlyDictionary<string, Room> Rooms => rooms;

        private static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}
