using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Pack_Server
{
    public enum PacketType
    {
        초기화 = 0,
        로그인,
        채팅메시지,
        접속자목록  // 새로 추가
    }

    [Serializable]
    public class Packet
    {
        public int Lenght;
        public int Type;

        public Packet()
        {
            this.Lenght = 0;
            this.Type = 0;
        }

        public static byte[] Serialize(Object o)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, o);
                return ms.ToArray();
            }
        }

        public static object Deserialize(byte[] buffer, int offset, int count)
        {
            using (MemoryStream ms = new MemoryStream(buffer, offset, count))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return bf.Deserialize(ms);
            }
        }
    }

    [Serializable]
    public class Initialize : Packet
    {
        public int Data = 0;
    }

    [Serializable]
    public class Login_server : Packet
    {
        public string m_strID;

        public Login_server()
        {
            this.Type = (int)PacketType.로그인;
            this.m_strID = null;
        }
    }

    [Serializable]
    public class ChatMessage : Packet
    {
        public string sender;
        public string message;

        public ChatMessage()
        {
            this.Type = (int)PacketType.채팅메시지;
        }
    }

    [Serializable]
    public class UserListPacket : Packet
    {
        public List<string> nicknames;

        public UserListPacket()
        {
            this.Type = (int)PacketType.접속자목록;
            nicknames = new List<string>();
        }
    }
}
