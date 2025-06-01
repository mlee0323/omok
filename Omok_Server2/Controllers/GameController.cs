using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omok_Server2.Data;
using Omok_Server2.Constant;
using Omok_Server2.Helper;

namespace Omok_Server2.Controllers
{
    public static class GameController
    {
        public static string Handle(CommandType cmd, string[] tokens, ClientHandler client)
        {
            if (tokens == null || tokens.Length == 0)
                return "INVALID_PACKET";

            switch (cmd)
            {
                case CommandType.TEAM_INFO:
                    return HandleTeamInfo(tokens, client);
                case CommandType.EXIT_ROOM:
                    return HandleRoomExit(tokens, client);
                case CommandType.GAME_READY:
                    return HandleGameReady(tokens, client);
                case CommandType.STONE_PUT:
                    return HandleStonePut(tokens, client);
            }
            return "INVALID_COMMAND";
        }

        private static string HandleTeamInfo(string[] tokens, ClientHandler client)
        {
            if (tokens.Length < 4)
                return "TEAM_INFO_FAIL|FORMAT";

            string roomCode = tokens[3];
            Room room = RoomManager.GetRoom(roomCode);
            if (room == null)
                return "TEAM_INFO_FAIL|NO_ROOM";

            foreach (ClientHandler member in room.Clients)
            {
                int team = room.GetTeam(member) ?? 1;
                bool ready = room.IsReady(member);
                client.Send($"TEAM_INFO|{member.getUserPk()}|{member.getNickname()}|{team}|{ready}", "GameController > HandleTeamInfo");
            }

            client.Send("TEAM_INFO_END");
            return null;
        }

        private static string HandleRoomExit(string[] tokens, ClientHandler client)
        {
            if (tokens.Length < 4)
                return "ROOM_EXIT_FAIL|FORMAT";

            string roomCode = tokens[3];
            Room? room = RoomManager.GetRoom(roomCode);
            if (room == null)
                return "ROOM_EXIT_FAIL|NO_ROOM";

            // 방에서 제거
            bool removed = room.RemoveClient(client);
            if (!removed)
                return "ROOM_EXIT_FAIL|NOT_IN_ROOM";

            // 방이 비면 제거
            if (room.IsEmpty)
            {
                RoomManager.RemoveRoom(roomCode);
                return "ROOM_EXIT_OK|REMOVED";
            }

            // 남은 인원에게 이 플레이어가 나갔음을 알림
            foreach (var member in room.Clients)
            {
                int team = room.GetTeam(member) ?? 1;
                member.Send($"PLAYER_EXIT|{client.getUserPk()}|{client.getNickname()}|{team}");
            }

            return "ROOM_EXIT_OK";
        }

        private static string HandleGameReady(string[] tokens, ClientHandler client)
        {
            if (tokens.Length < 5) return $"{CommandType.GAME_READY_FAIL}|FORMAT";

            string roomCode = tokens[3];
            bool isReady = bool.Parse(tokens[4]);
            var room = RoomManager.GetRoom(roomCode);
            if (room == null) return $"{CommandType.GAME_READY_FAIL}|NO_ROOM";

            room.SetReady(client, isReady);
            int team = room.GetTeam(client) ?? 1;

            room.Broadcast($"PLAYER_READY|{client.getUserPk()}|{client.getNickname()}|{team}|{isReady}");
            
            return $"{CommandType.GAME_READY_OK}";
        }

        private static string HandleStonePut(string[] tokens, ClientHandler client)
        {
            if (tokens.Length < 5) return "STONE_PUT_FAIL|FORMAT";

            string pk = tokens[1];
            int x = int.Parse(tokens[3]);
            int y = int.Parse(tokens[4]);

            var room = RoomManager.GetRoomByUserPk(pk);
            if (room == null) return "STONE_PUT_FAIL|NO_ROOM";

            var current = room.GetCurrentTurnPlayer();
            if (client != current) return "STONE_PUT_FAIL|NOT_YOUR_TURN";

            int team = room.GetTeam(client) ?? 0;
            if (team == 0) return "STONE_PUT_FAIL|NO_TEAM";

            bool win = room.PlaceStone(x, y, team);

            room.Broadcast($"STONE_PUT|{x}|{y}|{client.getNickname()}|{team}");

            if (win)
            {
                room.Broadcast($"GAME_OVER|{team}");
                room.ResetGame();
            }
            else
            {
                room.AdvanceTurn();
            }

            return "STONE_PUT_OK";
        }

    }
}
