using System;

namespace Omok_Server2.Data
{
    public class User
    {
        // 유저 고유 번호
        public int uid { get; set; }

        // 닉네임
        public string nickname { get; set; }

        // 아이디 (username)
        public string username { get; set; }

        // 비밀번호
        public string password { get; set; }

        // 전적
        public int winCount { get; set; }
        public int loseCount { get; set; }
        public int drawCount { get; set; }

        public int gameCount => winCount + loseCount + drawCount;

        // 현재 게임 중인 게임방 ID (없으면 null)
        public int? CurrentRoomId { get; set; }

        // ID 기반 생성자
        public User(int uid)
        {
            this.uid = uid;
        }

        public User(int uid, string nickname, string username, string password)
        {
            this.uid = uid;
            this.nickname = nickname;
            this.username = username;
            this.password = password;
        }

        // 전적 갱신 메서드 예시
        public void RecordWin() => winCount++;
        public void RecordLose() => loseCount++;
        public void RecordDraw() => drawCount++;
    }
}
