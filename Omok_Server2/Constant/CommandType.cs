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
        CHANGE_TEAM,
        SHUFFLE_TEAM,

        // ROOM
        EXIT_ROOM,

        // GAME
        GAME_READY,
        GAME_READY_FAIL,
        GAME_READY_OK,
        GAME_START,
        STONE_DEL,
        STONE_PUT,
        SKILL_USE,
        SKILL_USE_FAIL,
        SKILL_USE_OK,
        

        // CHAT
        CHAT,
        EMOJI,

        INVALID_COMMAND,
        INVALID_FORMAT,
        INVALID_PACKET,

        // History
        HISTORY_LOAD,
        HISTORY_STONES_LOAD,
    }

}
