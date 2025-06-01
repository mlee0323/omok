using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Omok_Network
{
    public enum PacketType
    {
        Authenticate,
        채팅메시지
    }

    [Serializable]
    public class Packet
    {
        public int Length;
        public PacketType Type;

        public Packet() { this.Length = 0; this.Type = 0; }

        public static byte[] Serialize(object o)
        {
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, o);
                return ms.ToArray();
            }
        }

        public static object Deserialize(byte[] buffer, int offset, int count)
        {
            using (var ms = new MemoryStream(buffer, offset, count))
            {
                return new BinaryFormatter().Deserialize(ms);
            }
        }
    }

    [Serializable]
    public class Authenticate_Packet : Packet
    {
        public string command;  // NAME_CHECK, ID_CHECK 등.
        public string username;
        public string password;
        public string nickname;
        public string confirm;

        public string result;   // 서버 응답 결과 저장용 필드

        public Authenticate_Packet()
        {
            this.Type = (int)PacketType.Authenticate;
        }
    }
}