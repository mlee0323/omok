using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omok_Server2.Data
{
    public class Player
    {
        private int pk;
        private string name;
        
        // 팀
        private int team;
        private int team_pk;

        private char stoneColor;
        private bool ready;
        private char win;
        private int order;  // 바둑돌을 두게 되는 순서

        public Player(int pk, string name, int team)
        {
            this.pk = pk;
            this.name = name;
            this.ready = false;    
            this.team = team;
        }

        public int GetPK() { return pk; }
        public string GetName() { return name; }
        public int GetTeam() { return team; }
        public char GetStoneColor() { return stoneColor; }
        public bool GetReady() { return ready; }
        public char GetWin() { return win; }
        public int GetTeamPK() { return team_pk; }

        public void SetTeam(int team) { this.team = team; }
        public void SetStoneColor(char stoneColor) { this.stoneColor = stoneColor; }
        public void SetReady(bool ready) { this.ready = ready; }
        public void SetWin(char win) { this.win = win; }
        public void SetTeamPk(int team_pk) { this.team_pk = team_pk; }
    }
}
