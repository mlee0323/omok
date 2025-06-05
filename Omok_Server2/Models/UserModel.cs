using System;
using System.IO;

namespace Omok_Server2.Models
{
    public static class UserModel
    {
        private static readonly string userFile =
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\DB\user.txt"));

        // 닉네임 중복 확인 로직
        public static bool IsNickDuplicate(string nickname)
        {
            if (!File.Exists(userFile)) return false;
            foreach (var line in File.ReadAllLines(userFile))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] tokens = line.Split(',');
                if (tokens.Length < 4) continue;
                if (tokens[1] == nickname)
                    return true;
            }
            return false;
        }

        public static bool IsIdDuplicate(string id)
        {
            if (!File.Exists(userFile)) return false;
            foreach (var line in File.ReadAllLines(userFile))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] tokens = line.Split(',');
                if (tokens.Length < 4) continue;
                if (tokens[2] == id)
                    return true;
            }
            return false;
        }

        private static int GetNextPk()
        {
            if (!File.Exists(userFile)) return 1;

            int maxPk = 0;
            foreach (var line in File.ReadAllLines(userFile))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] tokens = line.Split(',');
                if (tokens.Length < 4) continue;

                if (int.TryParse(tokens[0], out int pk))
                    if (pk > maxPk) maxPk = pk;
            }
            return maxPk + 1;
        }

        public static bool Register(string id, string password, string nickname, out int pk)
        {
            pk = -1;
            if (IsIdDuplicate(id) || IsNickDuplicate(nickname)) return false;

            pk = GetNextPk();
            string line = $"{pk},{nickname},{id},{password}";
            Directory.CreateDirectory(Path.GetDirectoryName(userFile));
            File.AppendAllText(userFile, line + Environment.NewLine);
            return true;
        }

        public static (int pk, string nickname) Login(string id, string password)
        {
            if (!File.Exists(userFile))
                return (-5, null);
            
            foreach (var line in File.ReadAllLines(userFile))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                
                string[] tokens = line.Split(',');
                if (tokens.Length < 4) continue;

                if (tokens[2] == id && tokens[3] == password)
                    if (int.TryParse(tokens[0], out int pk))
                        return (pk, tokens[1]);
            }
            return (-1, null);
        }
    }
}
