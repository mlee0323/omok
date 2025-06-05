using Omok_Server2.Models;
using Omok_Server2.Constant;
using Omok_Server2.Helper;

namespace Omok_Server2.Controllers
{
    public static class AuthController
    {
        public static string Handle(CommandType cmd, string[] tokens)
        {
            if (tokens == null || tokens.Length == 0)
                return "INVALID_PACKET";

            switch (cmd)
            {
                case CommandType.CHECK_NICK:
                    return HandleCheckNick(tokens);
                case CommandType.CHECK_ID:
                    return HandleCheckId(tokens);
                case CommandType.REGISTER:
                    return HandleRegister(tokens);
                case CommandType.LOGIN:
                    return HandleLogin(tokens);
            }

            return CommandType.INVALID_COMMAND.ToString();
        }

        private static string HandleCheckNick(string[] tokens)
        {
            if (tokens.Length < 2) return CommandType.INVALID_FORMAT.ToString();
            return UserModel.IsNickDuplicate(tokens[1]) ? "NICK_DUP" : "NICK_OK";
        }

        private static string HandleCheckId(string[] tokens)
        {
            if (tokens.Length < 2) return CommandType.INVALID_FORMAT.ToString();
            return UserModel.IsIdDuplicate(tokens[1]) ? "ID_DUP" : "ID_OK";
        }

        private static string HandleRegister(string[] tokens)
        {
            if (tokens.Length < 4) return CommandType.INVALID_FORMAT.ToString();

            bool success = UserModel.Register(tokens[1], tokens[2], tokens[3], out int pk);
            return success ? $"REGISTER_SUCCESS|{pk}" : "REGISTER_FAIL";
        }

        private static string HandleLogin(string[] tokens)
        {
            if (tokens.Length < 3) return CommandType.INVALID_FORMAT.ToString();

            var (pk, nickname) = UserModel.Login(tokens[1], tokens[2]);
            return pk != -1 ? $"LOGIN_SUCCESS|{pk}|{nickname}" : "LOGIN_FAIL";
        }
    }
}
