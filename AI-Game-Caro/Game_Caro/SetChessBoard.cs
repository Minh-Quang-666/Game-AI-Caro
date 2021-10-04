using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Caro
{
    public partial class SetChessBoard : Form
    {
        #region Properties
        #endregion
        #region Initialize
        public SetChessBoard()
        {
            InitializeComponent();
        }
        #endregion
        #region Mothods
        private void btpvp_Click(object sender, EventArgs e)
        {
            if (tbcolumn.Text.Length == 0 || tbrow.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đủ số cột và số dòng");
            }
            else
            {
                // Lấy dữ liệu từ số hàng và số cột khi người chơi nhâpk
                Common common = new Common(int.Parse(tbcolumn.Text), int.Parse(tbrow.Text));
                Common.StartPV = btpvp.Name;
                playchess playchess = new playchess();
                playchess.Show();
                this.Hide();
            }
        }

        private void btpvc_Click(object sender, EventArgs e)
        {
            if (tbcolumn.Text.Length == 0 || tbrow.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đủ số cột và số dòng");
            }
            else
            {
                // Lấy dữ liệu từ số hàng và số cột khi người chơi nhâpk
                Common common = new Common(int.Parse(tbcolumn.Text), int.Parse(tbrow.Text));
                Common.StartPV = btpvc.Name;
                playchess playchess = new playchess();
                playchess.Show();
                this.Hide();
            }
        }
        #endregion

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
