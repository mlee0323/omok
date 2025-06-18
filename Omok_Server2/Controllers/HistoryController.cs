using Omok_Server2.Constant;
using Omok_Server2.Models;

namespace Omok_Server2.Controllers
{
    public static class HistoryController
    {
        public static string Handle(CommandType cmd, string[] tokens, ClientHandler client)
        {
            switch (cmd)
            {
                case CommandType.HISTORY_LOAD:
                    return LoadGames(tokens, client);
                case CommandType.HISTORY_STONES_LOAD:
                    return LoadStones(tokens, client);
                default:
                    return "HISTORY_FAIL|INVALID";
            }
        }

        private static string LoadGames(string[] tokens, ClientHandler client)
        {
            if (tokens.Length < 2)
                return "HISTORY_LOAD_FAIL|FORMAT";
            
            int user_pk = int.Parse(tokens[1]);

            var games = HistoryModel.LoadGames(user_pk);
            foreach (var g in games)
            {
                client.Send($"HISTORY_GAME|{g.PK}|{g.GameCode}|{g.StartTime:yyyy-MM-dd HH:mm:ss}|{g.EndTime:yyyy-MM-dd HH:mm:ss}|{g.WinLose}");
            }
            client.Send("HISTORY_LOAD_END");
            return null;
        }

        private static string LoadStones(string[] tokens, ClientHandler client)
        {
            if (tokens.Length < 2 || !int.TryParse(tokens[1], out int gamePk))
                return "HISTORY_STONES_LOAD|FORMAT";
            var stones = HistoryModel.LoadStones(gamePk);
            int turn = 1;
            foreach (var s in stones)
            {
                client.Send($"HISTORY_STONE|{turn}|{s.Username}|{s.X}|{s.Y}|{s.StoneColor}|{s.Skill}");
                if (s.Skill == 0)
                    turn++;
            }
            client.Send("HISTORY_STONES_LOAD_END");
            return null;
        }
    }
}