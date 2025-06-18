using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omok_Server2.Data;
using Omok_Server2.Constant;
using Omok_Server2.Helper;
using Omok_Server2.Models;

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
                case CommandType.SKILL_USE:
                    return HandleSkillUse(tokens, client);
                case CommandType.STONE_DEL:
                    return HandleStoneDel(tokens, client);
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

            var current = room.GetCurrentTurnPlayerByTeam();
            if (client != current) return "STONE_PUT_FAIL|NOT_YOUR_TURN";

            int team = room.GetTeam(client) ?? 0;
            if (team == 0) return "STONE_PUT_FAIL|NO_TEAM";

            bool win = room.PlaceStone(x, y, client);

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

        private static string HandleStoneDel(string[] tokens, ClientHandler client)
        {
            if (tokens.Length < 5) return "STONE_DEL_FAIL|FORMAT";

            string pk = tokens[1];   // 플레이어 PK
            int x = int.Parse(tokens[3]); // 삭제할 X 좌표
            int y = int.Parse(tokens[4]); // 삭제할 Y 좌표

            
            var room = RoomManager.GetRoomByUserPk(pk);
            if (room == null) return "STONE_DEL_FAIL|NO_ROOM";

            var current = room.GetCurrentTurnPlayerByTeam();
            if (client != current) return "STONE_DEL_FAIL|NOT_YOUR_TURN";  // 차례가 아니면 실패

            int team = room.GetTeam(client) ?? 0;
            if (team == 0) return "STONE_PUT_FAIL|NO_TEAM";

            // 돌 삭제
            room.DeleteStone(x, y,team);

            room.Broadcast($"STONE_DEL|{x}|{y}|{client.getNickname()}|{team}");
            //room.AdvanceTurn();
            return "STONE_DELETE_OK";
        }


        private static string HandleSkillUse(string[] tokens, ClientHandler client)
        {
            if (tokens.Length < 5) return "SKILL_USE_FAIL|FORMAT";

            string pk = tokens[1];
            string nickname = tokens[2];
            string roomCode = tokens[3];
            int skillType = int.Parse(tokens[4]);

            var room = RoomManager.GetRoom(roomCode);
            if (room == null) return "SKILL_USE_FAIL|NO_ROOM";

            // 스킬 사용 메시지를 모든 클라이언트에게 브로드캐스트
            room.Broadcast($"SKILL_USE|{pk}|{nickname}|{skillType}");

            // 바둑판 엎기 스킬인 경우 게임 종료
            if (skillType == 3)
            {
                room.Broadcast($"GAME_OVER|0"); // 0은 무승부를 의미
                room.ResetGame();
            }

            return "SKILL_USE_OK";
        }

    }
}
