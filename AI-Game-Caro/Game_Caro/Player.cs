using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Caro
{
    // lưu những hình ảnh và name của người chơi
    public class Player
    {
        private string player_name;
        private Image player_icon;

        public Player(string player_name, Image player_icon)
        {
            this.Player_name = player_name;
            this.Player_icon = player_icon;
        }

        public string Player_name { get => player_name; set => player_name = value; }
        public Image Player_icon { get => player_icon; set => player_icon = value; }
    }
}
