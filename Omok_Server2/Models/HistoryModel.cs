using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace Omok_Server2.Models
{
    public static class HistoryModel
    {
        private static readonly string gameFile =
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\DB\game.txt"));

        private static readonly string teamFile =
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\DB\team.txt"));

        private static readonly string stoneFile =
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\DB\stone.txt"));

        public class GameHistory
        {
            public int PK { get; set; }
            public string GameCode { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public char WinLose { get; set; }
        }

        public class StoneRecord
        {
            public int PK { get; set; }
            public int GamePK { get; set; }
            public string Username { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public char StoneColor { get; set; }

            public int Skill { get; set; }
        }

        public static List<GameHistory> LoadGames(int userPk)
        {
            var userGames = new Dictionary<int, char>();
            foreach (var line in File.ReadAllLines(teamFile))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var t = line.Split(',');
                if (t.Length < 6) continue;
                if (int.TryParse(t[2], out int u) && u == userPk
                    && int.TryParse(t[1], out int g))
                {
                    userGames[g] = t[5][0];
                }
            }

            var result = new List<GameHistory>();
            foreach (var line in File.ReadAllLines(gameFile))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var g = line.Split(',');
                if (g.Length < 4) continue;
                if (int.TryParse(g[0], out int pk) && userGames.ContainsKey(pk))
                {
                    result.Add(new GameHistory
                    {
                        PK = pk,
                        GameCode = g[1],
                        StartTime = DateTime.ParseExact(g[2].Trim(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                        EndTime = DateTime.ParseExact(g[3].Trim(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                        WinLose = userGames[pk]
                    });
                }
            }
            return result.OrderBy(g => g.StartTime).ToList();
        }

        public static List<StoneRecord> LoadStones(int gamePk)
        {
            var list = new List<StoneRecord>();
            foreach (var line in File.ReadAllLines(stoneFile))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var t = line.Split(',');
                if (t.Length < 9) continue;
                if (!int.TryParse(t[1], out int gpk) || gpk != gamePk) continue;
                list.Add(new StoneRecord
                {
                    PK = int.Parse(t[0]),
                    GamePK = gpk,
                    Username = t[3],
                    X = int.Parse(t[5]),
                    Y = int.Parse(t[6]),
                    StoneColor = t[7][0],
                    Skill = int.Parse(t[8])
                });
            }
            return list.OrderBy(s => s.PK).ToList();
        }
    }
}