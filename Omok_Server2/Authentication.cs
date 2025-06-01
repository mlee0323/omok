using System;
using System.IO;
using Omok_Network;

namespace Omok_Server2
{
    public static class Authentication
    {
        private static readonly string userDBPath = Path.GetFullPath(Path.Combine("..", "..", "..", "db", "user.txt"));
        private static readonly object fileLock = new object();

        public static Packet HandlePacket(Packet packet, Action<string> log = null)
        {
            var auth = (Authenticate_Packet)packet;
            var response = new Authenticate_Packet
            {
                command = auth.command
            };

            log?.Invoke($"[Auth] 로그인");

            switch (auth.command.ToUpper())
            {
                case "LOGIN":
                    var id = Login(auth.username, auth.password);
                    if (id != null)
                    {
                        log?.Invoke($"[Auth] 로그인 성공: {auth.username} (id={id})");
                        response.result = $"LOGIN_SUCCESS|{id}";
                    }
                    else
                    {
                        log?.Invoke($"[Auth] 로그인 실패: {auth.username}");
                        response.result = "LOGIN_FAILED";
                    }
                    break;

                case "ID_CHECK":
                    bool idExists = IsUsernameDuplicate(auth.username);
                    response.result = idExists ? "ID_DUPLICATE" : "ID_OK";
                    log?.Invoke($"[Auth] 아이디 중복 확인: {auth.username} → {response.result}");
                    break;

                case "NAME_CHECK":
                    bool nameExists = IsNicknameDuplicate(auth.nickname);
                    response.result = nameExists ? "NAME_DUPLICATE" : "NAME_OK";
                    log?.Invoke($"[Auth] 닉네임 중복 확인: {auth.nickname} → {response.result}");
                    break;

                case "REGISTER":
                    response.result = Register(auth.nickname, auth.username, auth.password, auth.confirm, log);
                    break;

                default:
                    response.result = "UNKNOWN_COMMAND";
                    log?.Invoke($"[Auth] 알 수 없는 명령: {auth.command}");
                    break;
            }

            return response;
        }


        public static int? Login(string username, string password)
        {
            if (!File.Exists(userDBPath))
                return null;

            foreach (string line in File.ReadLines(userDBPath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] tokens = line.Split(',');
                if (tokens.Length < 4) continue;

                string id = tokens[0];
                string dbUsername = tokens[2];
                string dbPassword = tokens[3];

                if (dbUsername == username && dbPassword == password)
                {
                    if (int.TryParse(id, out int intId))
                        return intId;
                }
            }

            return null;
        }
        public static bool IsNicknameDuplicate(string nickname)
        {
            if (!File.Exists(userDBPath)) return false;

            foreach (string line in File.ReadLines(userDBPath))
            {
                string[] tokens = line.Split(',');
                if (tokens[1] == nickname)
                    return true;
            }

            return false;
        }

        public static bool IsUsernameDuplicate(string username)
        {
            if (!File.Exists(userDBPath)) return false;

            foreach (string line in File.ReadLines(userDBPath))
            {
                string[] tokens = line.Split(',');
                if (tokens.Length >= 3 && tokens[1] == username)
                    return true;
            }

            return false;
        }

        public static string Register(string name, string username, string password, string confirm, Action<string> log = null)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirm))
            {
                log?.Invoke("[Auth] 회원가입 실패: 빈 칸 있음");
                return "REGISTER_FAIL|EMPTY_FIELD";
            }

            if (password != confirm)
            {
                log?.Invoke("[Auth] 회원가입 실패: 비밀번호 불일치");
                return "REGISTER_FAIL|PASSWORD_MISMATCH";
            }

            lock (fileLock)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(userDBPath)); // 디렉터리 생성 보장

                var lines = File.Exists(userDBPath) ? File.ReadAllLines(userDBPath) : Array.Empty<string>();
                foreach (var line in lines)
                {
                    string[] tokens = line.Split(',');
                    if (tokens.Length >= 3 && tokens[2] == username)
                    {
                        log?.Invoke("[Auth] 회원가입 실패: 아이디 중복");
                        return "REGISTER_FAIL|EXISTS";
                    }
                }

                int newId = lines.Length + 1;
                string newLine = $"{newId},{name},{username},{password}";
                File.AppendAllText(userDBPath, newLine + Environment.NewLine);

                log?.Invoke($"[Auth] 회원가입 성공: {username} (id={newId})");
            }
            return "REGISTER_SUCCESS";
        }
    }
}
