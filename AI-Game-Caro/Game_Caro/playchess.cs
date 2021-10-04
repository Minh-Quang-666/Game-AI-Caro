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
    public partial class playchess : Form
    {
        #region Properties
        ChessBoardManager chessBoard;
        #endregion
        public playchess()
        {
            InitializeComponent();

            // khởi tạo một lớp chessBoardManager để gọi hàm dựng bàn cờ
            chessBoard = new ChessBoardManager(pnchessboard, tbplayname, imgplayer);

            chessBoard.PlayerMarked += ChessBoard_PlayerMarked;
            chessBoard.EndedGame += ChessBoard_EndedGame;

            prbcooldown.Step = Common.COOL_DOWN_STEP;
            prbcooldown.Maximum = Common.COOL_DOWN_TIME;
            prbcooldown.Value = 0;

            tmcooldown.Interval = Common.COOL_DOWN_INTERVAL;
            if(Common.StartPV == "btpvp")
            {
                NewGamePvP();
            } else
            {
                NewGamePvC();
            }

        }
        #region Methods
        void EndGame()
        {
            tmcooldown.Stop();
            pnchessboard.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
           // MessageBox.Show("kết thúc game");
        }
        private void ChessBoard_EndedGame(object sender, EventArgs e)
        {
            EndGame();
        }
        // mỗi khi người chơi nhấn thì reset lại thời gian
        private void ChessBoard_PlayerMarked(object sender, EventArgs e)
        {
            tmcooldown.Start();
            prbcooldown.Value = 0;
            tbAIcom.Text = "";
        }

        private void tmcooldown_Tick(object sender, EventArgs e)
        {
            prbcooldown.PerformStep();
            // nếu như vượt quá thời gian thì kết thúc game
            if(prbcooldown.Value >= prbcooldown.Maximum)
            {
                tmcooldown.Stop();
                EndGame();
            }
        }
        void NewGamePvP()
        {
            prbcooldown.Value = 0;
            tmcooldown.Stop();
            undoToolStripMenuItem.Enabled = true;
            chessBoard.DrawChessBoard();
            computerAlgorithmToolStripMenuItem.Enabled = false;
        }
        // chơi với máy
        void NewGamePvC()
        {
            prbcooldown.Value = 0;
            tmcooldown.Stop();
            undoToolStripMenuItem.Enabled = true;
            chessBoard.DrawChessBoard();
        }
        void Undo()
        {
            chessBoard.Undo();
        }
        void Quit()
        {
              Application.Exit();
        }
   

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void playchess_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            } else
            {
                Application.Exit();
            }
        }
        #endregion

        private void PlayWithPeopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.StartPV = "pvp";
            NewGamePvP();
        }

        private void playWithTheMachineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.StartPV = "pvc";
            NewGamePvC();
        }
        private void showAlgorithmCurentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbAIcom.Text  = chessBoard.ShowComputerAlgorithm();
            tmcooldown.Start();
        }
        // Hàm này để in ra list accas thuật toán của tất cả nước đi của máy đã đánh
        private void printListAlgorithmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chessBoard.PrintComputerAlgorithm();
            if((MessageBox.Show("Bạn Đã In thuật Toán ra file Thành Công", "thông báo", MessageBoxButtons.OK)) == System.Windows.Forms.DialogResult.OK)
            {
                tmcooldown.Start();
            }
        }

        private void computerAlgorithmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmcooldown.Stop();
        }
    }
}
