using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Omok_Client.Form;

namespace Omok_Client
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            while (true)
            {
                // 로그인
                using (var login = new Login())
                {
                    if (login.ShowDialog() != DialogResult.OK)
                        return;   // 프로그램 종료
                }

                // 로비
                using (var lobby = new Lobby())
                {
                    var result = lobby.ShowDialog();
                    if (result == DialogResult.Retry)
                        continue; // 로그인으로 다시 돌아옴
                    else
                        return;   // 프로그램 종료
                }
            }
        }


    }
}
