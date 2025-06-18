using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omok_Server2.Constant;

namespace Omok_Server2.Helper
{
    public static class CommandParser
    {
        public static CommandType Parse(string cmd)
        {
            return cmd switch
            {
                "LOGIN" => CommandType.LOGIN,
                "REGISTER" => CommandType.REGISTER,
                "CHECK_ID" => CommandType.CHECK_ID,
                "CHECK_NICK" => CommandType.CHECK_NICK,
                "CREATE_ROOM" => CommandType.CREATE_ROOM,
                "JOIN_CODE" => CommandType.JOIN_CODE,
                "JOIN_RANDOM" => CommandType.JOIN_RANDOM,
                "TEAM_INFO" => CommandType.TEAM_INFO,
                "TEAM_CHANGED" => CommandType.TEAM_CHANGED,
                "EXIT_ROOM" => CommandType.EXIT_ROOM,
                "GAME_READY" => CommandType.GAME_READY,
                "GAME_START" => CommandType.GAME_START,
                "STONE_PUT" => CommandType.STONE_PUT,
                "SKILL_USE" => CommandType.SKILL_USE,
                "STONE_DEL" => CommandType.STONE_DEL,
                _ => CommandType.Unknown
            };
        }

        public static string GetCategory(CommandType type)
        {
            return type switch
            {
                CommandType.LOGIN or CommandType.REGISTER or CommandType.CHECK_ID or CommandType.CHECK_NICK => "Auth",
                CommandType.CREATE_ROOM or CommandType.JOIN_CODE or CommandType.JOIN_RANDOM => "Room",
                
                CommandType.TEAM_INFO or
                CommandType.TEAM_CHANGED or
                CommandType.GAME_READY or
                CommandType.EXIT_ROOM or
                CommandType.GAME_START or
                CommandType.STONE_PUT or
                CommandType.SKILL_USE or
                CommandType.STONE_DEL
                => "Game",
                
                _ => "Unknown"
            };
        }
    }

}
