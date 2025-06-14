using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omok_Server2.Constant
{
    public enum CommandType
    {
        Unknown,
        LOGIN,
        REGISTER,
        CHECK_ID,
        CHECK_NICK,
        CREATE_ROOM,
        JOIN_CODE,
        JOIN_RANDOM,

        // TEAM
        TEAM_INFO,
        TEAM_INFO_END,
        TEAM_CHANGED,

        // ROOM
        EXIT_ROOM,

        // GAME
        GAME_READY,
        GAME_READY_FAIL,
        GAME_READY_OK,
        GAME_START,
        STONE_PUT,

        // CHAT
        CHAT,

        INVALID_COMMAND,
        INVALID_FORMAT,
        INVALID_PACKET
    }

}
