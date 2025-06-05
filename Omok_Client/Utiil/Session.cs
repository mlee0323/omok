using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using Omok_Client.Network;

namespace Omok_Client.Util
{
    public static class Session
    {
        public static int Pk { get; set; }
        public static string Nickname { get; set; }

        public static string RoomCode { get; set; } = string.Empty;

        public static int Team { get; set; } = -1;
    }
}
