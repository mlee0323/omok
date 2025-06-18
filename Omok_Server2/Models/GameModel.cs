// 파일: Omok_Server2/Models/GameModel.cs
using Omok_Server2.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Omok_Server2.Models
{
    public static class GameModel
    {
        private static string GetGamePath() =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\DB", "game.txt");
        private static string GetTeamPath() =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\DB", "team.txt");
        private static string GetStonePath() =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\DB", "stone.txt");

        // 방 정보 저장
        public static int SaveRoom(int pk, string roomCode, DateTime startTime, DateTime endTime)
        {
            string path = GetGamePath();
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            // 새 방 정보 추가
            int newPk;
            newPk = GetNextPk(path);
            string strStart = startTime.ToString("yyyy-MM-dd HH:mm:ss");
            string record = $"{newPk},{roomCode},{strStart}";

            using (var writer = new StreamWriter(path, true, Encoding.UTF8))
                writer.WriteLine(record);

            return newPk;
        }

        public static void UpdateRoom(int pk, string roomCode, DateTime startTime, DateTime endTime)
        {
            string path = GetGamePath();
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            string strStart = startTime.ToString("yyyy-MM-dd HH:mm:ss");
            string strEnd = endTime.ToString("yyyy-MM-dd HH:mm:ss");
            var lines = File.ReadAllLines(path, Encoding.UTF8).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                var tokens = lines[i].Split(',');
                if (tokens.Length < 3) continue;
                if (int.TryParse(tokens[0], out int existingPk) && existingPk == pk)
                {
                    lines[i] = $"{pk},{roomCode},{strStart},{strEnd}";
                    break;
                }
            }
            File.WriteAllLines(path, lines, Encoding.UTF8);
        }

        public static int SaveTeam(int room_pk, int user_pk, int team, char stoneColor)
        {
            string path = GetTeamPath();
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            int pk = GetNextPk(path);
            string record = $"{pk},{room_pk},{user_pk},{team},{stoneColor},-1";

            using (var writer = new StreamWriter(path, true, Encoding.UTF8))
                writer.WriteLine(record);

            return pk;
        }

        public static void UpdateTeam(int team_pk, int room_pk, int user_pk, int team, char stoneColor, char win)
        {
            string path = GetTeamPath();
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            var lines = new List<string>(File.ReadAllLines(path, Encoding.UTF8));
            for (int i = 0; i < lines.Count; i++)
            {
                var tokens = lines[i].Split(',');
                if (int.TryParse(tokens[0], out int existingPk) && existingPk == team_pk)
                {
                    lines[i] = $"{team_pk},{room_pk},{user_pk},{team},{stoneColor},{win}";
                    break;
                }
            }
            File.WriteAllLines(path, lines, Encoding.UTF8);
        }


        public static void RecordStone(int room_pk, int user_pk, string username,
                                int team, int x, int y, char stoneColor, int skill = 0)
        {
            string path = GetStonePath();
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            int pk = GetNextPk(path);
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string record = $"{pk},{room_pk},{user_pk},{username},{team},{x},{y}," +
                            $"{stoneColor},{skill},{timestamp}";

            using (var writer = new StreamWriter(path, true, Encoding.UTF8))
                writer.WriteLine(record);
        }


        private static int GetNextPk(string filePath)
        {
            if (!File.Exists(filePath)) return 1;

            int maxPk = 0;
            foreach (var line in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] tokens = line.Split(',');

                if (int.TryParse(tokens[0], out int pk))
                    if (pk > maxPk) maxPk = pk;
            }
            return maxPk + 1;
        }
    }
}
