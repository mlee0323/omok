using Omok_Server2.Data;
using Omok_Server2.Network;
using Omok_Server2.Constant;
using System;

namespace Omok_Server2.Controllers
{
    public static class RoomController
    {
        const int maxPlayers = 4; // 최대 플레이어 수

        public static string Handle(CommandType cmd, string[] tokens, ClientHandler client)
        {
            switch (cmd.ToString())
            {
                case "CREATE_ROOM":
                    return HandleCreate(tokens, client);

                case "JOIN_CODE":
                    return HandleJoinCode(tokens, client);

                case "JOIN_RANDOM":
                    return HandleJoinRandom(tokens, client);
                default:
                    return "INVALID_ROOM_COMMAND";
            }
        }

        private static string HandleCreate(string[] tokens, ClientHandler client)
        {
            if (tokens.Length < 2)
                return "CREATE_FAIL|INVALID_FORMAT";

            string roomCode = RoomManager.CreateRoom(client, maxPlayers);
            if (roomCode == null)
                return "CREATE_FAIL|ROOM_EXISTS";
            Room room = RoomManager.JoinByCode(roomCode, client);
            
            if (room != null)
            {
                room.BroadcastNotMe($"TEAM_CHANGED|{client.getUserPk()}|{client.getNickname()}|{room.GetTeam(client)}", client);
                return $"JOIN_SUCCESS|{room.RoomCode}";
            }
            return "JOIN_FAIL|ROOM_NOT_FOUND";            
        }

        private static string HandleJoinRandom(string[] tokens, ClientHandler client)
        {
            if (tokens.Length < 3)
                return "JOIN_FAIL|INVALID_FORMAT";

            try
            {
                Room room = RoomManager.JoinRandom(client);
                if (room != null)
                {
                    room.BroadcastNotMe($"TEAM_CHANGED|{client.getUserPk()}|{client.getNickname()}|{room.GetTeam(client)}", client);
                    return $"JOIN_SUCCESS|{room.RoomCode}";
                }
            }
            catch (Exception ex)
            {
                return $"JOIN_FAIL|{ex.Message}";
            }

            return "JOIN_FAIL|NO_AVAILABLE_ROOM";
        }

        private static string HandleJoinCode(string[] tokens, ClientHandler client)
        {
            if (tokens.Length < 3)
                return "JOIN_FAIL|INVALID_FORMAT";

            string roomCode = tokens[2];
            var room = RoomManager.JoinByCode(roomCode, client);

            if (room == null)
                return "JOIN_FAIL|NOT_FOUND_OR_FULL";

            return $"JOIN_SUCCESS|{room.RoomCode}";
        }
    }
}
