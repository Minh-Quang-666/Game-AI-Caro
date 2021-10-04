using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Game_Caro
{
    public class ChessBoardManager
    {
        #region properties
        private static Panel chessBoard;
        // list các người chơi 
        private List<Player> list_player_pvp;
        // list người chơi vơi máy
        private List<Player> list_player_pvc;
        // biến người chơi
        private int current_player;
        private TextBox player_name;
        private PictureBox player_icon;
        private static List<List<Button>> matrix = new List<List<Button>>();
        private static string game_mode;
        private static List<string> debug_computer = new List<string>();
        // private string
        // Bắt sự kiến nhấn nút
        private event EventHandler playerMarked;
        public event EventHandler PlayerMarked
        {
            add
            {
                playerMarked += value;
            }
            remove
            {
                playerMarked -= value;
            }
        }
        // bắt sự kiện kết thúc game
        private event EventHandler endedGame;
        public event EventHandler EndedGame
        {
            add
            {
                endedGame += value;
            }
            remove
            {
                endedGame -= value;
            }
        }
        // để lưu các điểm đã đi
        private Stack<PlayInfo> playTimeLine;
        #endregion
        #region Initialize
        public ChessBoardManager(Panel chessBoard_copy, TextBox player_name, PictureBox player_icon)
        {
            chessBoard = chessBoard_copy;
            // nếu chơi với người
            this.list_player_pvp = new List<Player>()
             {
                new Player("player1" , Image.FromFile(Application.StartupPath + "\\Resources\\icon_x.jpg")) ,
                new Player("player2" , Image.FromFile(Application.StartupPath + "\\Resources\\icon_o.jpg")) ,
             };
            // nếu chơi với máy
            this.list_player_pvc = new List<Player>()
             {
                new Player("computer" , Image.FromFile(Application.StartupPath + "\\Resources\\icon_x.jpg")) ,
                new Player("person" , Image.FromFile(Application.StartupPath + "\\Resources\\icon_o.jpg")) ,
             };
            this.player_name = player_name;
            this.player_icon = player_icon;

        }
        #endregion
        #region Methods
        // hàm dựng Bàn cờ
        public void DrawChessBoard()
        {
            game_mode = Common.StartPV;
            chessBoard.Enabled = true;
            chessBoard.Controls.Clear();
            debug_computer.Clear();
            current_player = 0;
            playTimeLine = new Stack<PlayInfo>();
            matrix.Clear();

            if (game_mode == "pvp" || game_mode == "btpvp")
            {
                ChangePlayer();
            }
            else
            {
                player_name.Text = list_player_pvc[1].Player_name;
                player_icon.Image = list_player_pvc[1].Player_icon;
            }

            Button origin_button = new Button() { Width = 0, Location = new Point(0, 0) };
            for (int i = 0; i <= Common.CHESS_BOARD_HEIGHT; i++)
            {
                matrix.Add(new List<Button>());
                for (int j = 0; j <= Common.CHESS_BOARD_WIDTH; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Common.chess_width,
                        Height = Common.chess_height,
                        Location = new Point(origin_button.Location.X + origin_button.Width, origin_button.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };
                    btn.Click += Btn_Click;
                    chessBoard.Controls.Add(btn);
                    matrix[i].Add(btn);
                    origin_button = btn;
                }
                origin_button.Location = new Point(0, origin_button.Location.Y + Common.chess_height);
                origin_button.Width = 0;
                origin_button.Height = 0;
            }
        }

        // khi nhấn vào một điểm 
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            // nếu là chơi người vs người 
            if (game_mode == "pvp" || game_mode == "btpvp")
            {
                if (btn.BackgroundImage == null)
                {
                    MarkPoint(btn);
                }
                else
                {
                    return;
                }
                // thêm vào vị trí của nước vừa đánh 
                playTimeLine.Push(new PlayInfo(getChessPoint(btn), current_player));

                if (!isEndGame(btn))
                {
                    if (current_player == 1)
                    {
                        current_player = 0;
                    }
                    else
                    {
                        current_player = 1;
                    }

                    ChangePlayer();
                    if (playerMarked != null)
                    {
                        playerMarked(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Người chơi " + list_player_pvp[current_player == 0 ? 0 : 1].Player_name + " win");
                    EndGame();
                    // in ra người chơi thắng
                }
            }
            if (game_mode == "pvc" || game_mode == "btpvc")
            {
                if (btn.BackgroundImage == null)
                {
                    btn.BackgroundImage = list_player_pvc[1].Player_icon;
                    playTimeLine.Push(new PlayInfo(getChessPoint(btn), current_player));
                    if (!isEndGame(btn))
                    {
                        player_name.Text = list_player_pvc[0].Player_name;
                        player_icon.Image = list_player_pvc[0].Player_icon;
                        Comclick();
                        if (playerMarked != null)
                        {
                            playerMarked(this, new EventArgs());
                        }
                    }
                    else
                    {
                        EndGame();
                        MessageBox.Show(list_player_pvc[1].Player_name + " win");
                    }
                }
            }
        }
        // máy đánh theo vị trí được tính toán điểm số cao nhất
        private void Comclick()
        {
            int[] location = SearchButton();
            int x = location[0], y = location[1];
            matrix[x][y].BackgroundImage = list_player_pvc[0].Player_icon;
            player_name.Text = list_player_pvc[1].Player_name;
            player_icon.Image = list_player_pvc[1].Player_icon;
            if (isEndGame(matrix[x][y]))
            {
                EndGame();
                // in ra người chơi thắn
                MessageBox.Show(list_player_pvc[0].Player_name + " win");
            }
        }

        public long[] Defend_Score = new long[7] { 0, 3, 27, 99, 729, 6561, 59049 };//Mảng điểm chặn
        public long[] Attack_Score = new long[7] { 0, 9, 54, 162, 1458, 13112, 118008 };//Mảng điểm tấn công
        // hàm tìm kiếm vị trí
        public int[] SearchButton()
        {
            string debug = "";
            int[] location = new int[2];
            long diemMax = 0;
            for (int i = 0; i < Common.CHESS_BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < Common.CHESS_BOARD_WIDTH; j++)
                {
                    long diemTanCong = 0;
                    long diemPhongNgu = 0; 
                    if (matrix[i][j].BackgroundImage == null)
                    {
                        diemTanCong += Score_Attack_Duyet_Doc(i, j);
                        diemTanCong += Score_Attack_Duyet_Ngang(i, j);
                        diemTanCong += Score_Attack_Duyet_Cheo_Nguoc(i, j);
                        diemTanCong += Score_Attack_Duyet_Cheo_Xuoi(i, j);

                        diemPhongNgu += Score_Defend_Duyet_Doc(i, j);
                        diemPhongNgu += Score_Defend_Duyet_Ngang(i, j);
                        diemPhongNgu += Score_Defend_Duyet_Cheo_Nguoc(i, j);
                        diemPhongNgu += Score_Defend_Duyet_Cheo_Xuoi(i, j);

                        // lấy ra điểm lớn nhất tại 1 điểm
                        if (diemPhongNgu <= diemTanCong)
                        {
                            if (diemMax <= diemTanCong)
                            {
                                diemMax = diemTanCong;
                                location[0] = i;
                                location[1] = j;
                            }
                        }
                        else
                        {
                            if (diemMax <= diemPhongNgu)
                            {
                                diemMax = diemPhongNgu;
                                location[0] = i;
                                location[1] = j;
                            }
                        }
                    }
                }
            }
            debug += "Tại Vị Trí i = " + location[0] + ", j = " + location[1] + " : \n" + "Điểm tấn công duyệt dọc , ngang , chéo xuôi , chéo ngược lần lượt là : " + Score_Attack_Duyet_Doc(location[0], location[1]) + "," + Score_Attack_Duyet_Ngang(location[0], location[1]) + "," + Score_Attack_Duyet_Cheo_Xuoi(location[0], location[1]) + "," +  Score_Attack_Duyet_Cheo_Nguoc(location[0], location[1]) + "\n";
            debug += "Điểm phòng ngự duyệt dọc , ngang , chéo xuôi , chéo ngược lần lượt là : " + Score_Defend_Duyet_Doc(location[0], location[1]) + "," + Score_Defend_Duyet_Ngang(location[0], location[1]) + "," + Score_Defend_Duyet_Cheo_Xuoi(location[0], location[1]) + "," + Score_Defend_Duyet_Cheo_Nguoc(location[0], location[1]) + "\n";
            long diemTC = (Score_Attack_Duyet_Doc(location[0], location[1]) + Score_Attack_Duyet_Ngang(location[0], location[1]) + Score_Attack_Duyet_Cheo_Xuoi(location[0], location[1]) + Score_Attack_Duyet_Cheo_Nguoc(location[0], location[1]));
            debug += "Điểm Tấn Công : " + diemTC + "\n";
            long diemPN = (Score_Defend_Duyet_Doc(location[0], location[1]) + Score_Defend_Duyet_Ngang(location[0], location[1]) + Score_Defend_Duyet_Cheo_Xuoi(location[0], location[1]) + Score_Defend_Duyet_Cheo_Nguoc(location[0], location[1]));
            debug += "Điểm Phòng Ngự " + diemPN + "\n";
            debug += "Điểm Max Tại Điểm Này : " + (diemTC > diemPN ? diemTC : diemPN) + " Lớn hơn tất cả mọi điểm chưa được đánh nên điểm i = " + location[0] + ", j = " + location[1] + " được chọn";
            debug_computer.Add(debug);
            return location;
        }
        #region Attack
        // tính điểm tấn công theo chiều dọc
        private long Score_Attack_Duyet_Doc(int Dong, int Cot)
        {
            long DiemTong = 0;
            long diemattack = 0;
            int iSoQuanTa = 0;
            int iSoQuanDich = 0;
            for (int Dem = 1; Dem < 6 && Dong + Dem < Common.CHESS_BOARD_HEIGHT; Dem++)
            { 
                if (matrix[Dong + Dem][Cot].BackgroundImage == list_player_pvc[0].Player_icon)
                    iSoQuanTa++;
                if (matrix[Dong + Dem][Cot].BackgroundImage == list_player_pvc[1].Player_icon)
                {
                    iSoQuanDich++;
                    DiemTong -= 9;
                    break;
                }
                if (matrix[Dong + Dem][Cot].BackgroundImage == null)
                    break;
            }

            for (int Dem = 1; Dem < 6 && Dong - Dem >= 0; Dem++)
            {
                if (matrix[Dong - Dem][Cot].BackgroundImage == list_player_pvc[0].Player_icon)
                    iSoQuanTa++;
                if (matrix[Dong - Dem][Cot].BackgroundImage == list_player_pvc[1].Player_icon)
                {
                    DiemTong -= 9;
                    iSoQuanDich++;
                    break;
                }
                if (matrix[Dong - Dem][Cot].BackgroundImage == null)
                    break;
            }
            if (iSoQuanDich == 2)
                return 0;
            diemattack += Attack_Score[iSoQuanTa];
            diemattack -= Attack_Score[iSoQuanDich];
            DiemTong += diemattack;

            return DiemTong;
        }
        // tính điểm tấn công theo chiều ngang
        private long Score_Attack_Duyet_Ngang(int Dong, int Cot)
        { 
            long diemTong = 0;
            long diemattack = 0;
            int iSoQuanTa = 0;
            int iSoQuanDich = 0;
            for (int Dem = 1; Dem < 6 && Cot + Dem < Common.CHESS_BOARD_WIDTH; Dem++)
            {
                if (matrix[Dong][Cot + Dem].BackgroundImage == list_player_pvc[0].Player_icon)
                    iSoQuanTa++;
                if (matrix[Dong][Cot + Dem].BackgroundImage == list_player_pvc[1].Player_icon)
                {
                    iSoQuanDich++;
                    diemTong -= 9;
                    break;
                }
                if (matrix[Dong][Cot + Dem].BackgroundImage == null)
                    break;
            }

            for (int Dem = 1; Dem < 6 && Cot - Dem >= 0; Dem++)
            {
                if (matrix[Dong][Cot - Dem].BackgroundImage == list_player_pvc[0].Player_icon)
                    iSoQuanTa++;
                if (matrix[Dong][Cot - Dem].BackgroundImage == list_player_pvc[1].Player_icon)
                {
                    diemTong -= 9;
                    iSoQuanDich++;
                    break;
                }
                if (matrix[Dong][Cot - Dem].BackgroundImage == null)
                    break;
            }
            if (iSoQuanDich == 2)
                return 0;
            diemattack += Attack_Score[iSoQuanTa];
            diemattack -= Attack_Score[iSoQuanDich];
            diemTong += diemattack;

            return diemTong;
        }
        // tính điểm tấn công theo chéo chính
        private long Score_Attack_Duyet_Cheo_Xuoi(int Dong, int Cot)
        {
            long diemTong = 0;
            long diemAttack = 0;
            int iSoQuanTa = 0;
            int iSoQuanDich = 0;
            for (int Dem = 1; Dem < 6 && Cot + Dem < Common.CHESS_BOARD_WIDTH && Dong + Dem < Common.CHESS_BOARD_HEIGHT; Dem++)
            {
                if (matrix[Dong + Dem][Cot + Dem].BackgroundImage == list_player_pvc[0].Player_icon)
                    iSoQuanTa++;
                if (matrix[Dong + Dem][Cot + Dem].BackgroundImage == list_player_pvc[1].Player_icon)
                {
                    diemTong -= 9;
                    iSoQuanDich++;
                    break;
                }
                if (matrix[Dong + Dem][Cot + Dem].BackgroundImage == null)
                    break;
            }

            for (int iDem = 1; iDem < 6 && Cot - iDem >= 0 && Dong - iDem >= 0; iDem++)
            {
                if (matrix[Dong - iDem][Cot - iDem].BackgroundImage == list_player_pvc[0].Player_icon)
                    iSoQuanTa++;
                if (matrix[Dong - iDem][Cot - iDem].BackgroundImage == list_player_pvc[1].Player_icon)
                {
                    diemTong -= 9;
                    iSoQuanDich++;
                    break;
                }
                if (matrix[Dong - iDem][Cot - iDem].BackgroundImage == null)
                    break;
            }
            if (iSoQuanDich == 2)
                return 0;
            diemAttack += Attack_Score[iSoQuanTa];
            diemAttack -= Attack_Score[iSoQuanDich];
            diemTong += diemAttack;

            return diemTong;
        }
        // tính điểm tấn công theo chéo phụ
        private long Score_Attack_Duyet_Cheo_Nguoc(int Dong, int Cot)
        {
            long diemTong = 0;
            long diemAttack = 0;
            int iSoQuanTa = 0;
            int iSoQuanDich = 0;
            for (int Dem = 1; Dem < 6 && Cot - Dem >= 0 && Dong + Dem < Common.CHESS_BOARD_HEIGHT; Dem++)
            {
                if (matrix[Dong + Dem][Cot - Dem].BackgroundImage == list_player_pvc[0].Player_icon)
                    iSoQuanTa++;
                if (matrix[Dong + Dem][Cot - Dem].BackgroundImage == list_player_pvc[1].Player_icon)
                {
                    diemTong -= 9;
                    iSoQuanDich++;
                    break;
                }
                if (matrix[Dong + Dem][Cot - Dem].BackgroundImage == null)
                    break;
            }

            for (int Dem = 1; Dem < 6 && Cot + Dem < Common.CHESS_BOARD_WIDTH && Dong - Dem >= 0; Dem++)
            {
                if (matrix[Dong - Dem][Cot + Dem].BackgroundImage == list_player_pvc[0].Player_icon)
                    iSoQuanTa++;
                if (matrix[Dong - Dem][Cot + Dem].BackgroundImage == list_player_pvc[1].Player_icon)
                {
                    diemTong -= 9;
                    iSoQuanDich++;
                    break;
                }
                if (matrix[Dong - Dem][Cot + Dem].BackgroundImage == null)
                    break;
            }
            if (iSoQuanDich == 2)
                return 0;
            diemAttack += Attack_Score[iSoQuanTa];
            diemAttack -= Attack_Score[iSoQuanDich];
            diemTong += diemAttack;

            return diemTong;
        }
        #endregion

        // Tính điểm phòng ngự
        #region Defense

        private long Score_Defend_Duyet_Doc(int Dong, int Cot)
        {
            long diemTong = 0;
            long diemDefend = 0;
            int iSoQuanDich = 0;
            int iSoQuanTa = 0;
            for (int Dem = 1; Dem < 6 && Dong + Dem < Common.CHESS_BOARD_HEIGHT; Dem++)
            {
                if (matrix[Dong + Dem][Cot].BackgroundImage == list_player_pvc[0].Player_icon)
                {
                    iSoQuanTa++;
                    break;
                }
                if (matrix[Dong + Dem][Cot].BackgroundImage == list_player_pvc[1].Player_icon)
                    iSoQuanDich++;
                if (matrix[Dong + Dem][Cot].BackgroundImage == null)
                    break;
            }
            for (int Dem = 1; Dem < 6 && Dong - Dem >= 0; Dem++)
            {
                if (matrix[Dong - Dem][Cot].BackgroundImage == list_player_pvc[0].Player_icon)
                {
                    iSoQuanTa++;
                    break;
                }
                if (matrix[Dong - Dem][Cot].BackgroundImage == list_player_pvc[1].Player_icon)
                    iSoQuanDich++;
                if (matrix[Dong - Dem][Cot].BackgroundImage == null)
                    break;
            }
            if (iSoQuanTa == 2)
                return 0;

            diemDefend += Defend_Score[iSoQuanDich];
            if (iSoQuanDich > 0)
                diemDefend -= Attack_Score[iSoQuanTa] * 2;
            diemTong += diemDefend;

            return diemTong;
        }
        private long Score_Defend_Duyet_Ngang(int Dong, int Cot)
        {
            long diemTong = 0;
            long diemDefend = 0;
            int iSoQuanDich = 0;
            int iSoQuanTa = 0;
            for (int Dem = 1; Dem < 6 && Cot + Dem < Common.CHESS_BOARD_WIDTH; Dem++)
            {
                if (matrix[Dong][Cot + Dem].BackgroundImage == list_player_pvc[0].Player_icon)
                {
                    iSoQuanTa++;
                    break;
                }
                if (matrix[Dong][Cot + Dem].BackgroundImage == list_player_pvc[1].Player_icon)
                    iSoQuanDich++;
                if (matrix[Dong][Cot + Dem].BackgroundImage == null)
                    break;
            }

            for (int Dem = 1; Dem < 6 && Cot - Dem >= 0; Dem++)
            {
                if (matrix[Dong][Cot - Dem].BackgroundImage == list_player_pvc[0].Player_icon)
                {
                    iSoQuanTa++;
                    break;
                }
                if (matrix[Dong][Cot - Dem].BackgroundImage == list_player_pvc[1].Player_icon)
                    iSoQuanDich++;
                if (matrix[Dong][Cot - Dem].BackgroundImage == null)
                    break;
            }
            if (iSoQuanTa == 2)
                return 0;
            diemDefend += Defend_Score[iSoQuanDich];
            if (iSoQuanDich > 0)
                diemDefend -= Attack_Score[iSoQuanTa] * 2;
            diemTong += diemDefend;

            return diemTong;
        }
        private long Score_Defend_Duyet_Cheo_Xuoi(int Dong, int Cot)
        {
            long diemTong = 0;
            long diemDefend = 0;
            int iSoQuanDich = 0; 
            int iSoQuanTa = 0;
            for (int Dem = 1; Dem < 6 && Cot + Dem < Common.CHESS_BOARD_WIDTH && Dong + Dem < Common.CHESS_BOARD_HEIGHT; Dem++)
            {
                if (matrix[Dong + Dem][Cot + Dem].BackgroundImage == list_player_pvc[0].Player_icon)
                {
                    iSoQuanTa++;
                    break;
                }
                if (matrix[Dong + Dem][Cot + Dem].BackgroundImage == list_player_pvc[1].Player_icon)
                    iSoQuanDich++;
                if (matrix[Dong + Dem][Cot + Dem].BackgroundImage == null)
                    break;
            }

            for (int Dem = 1; Dem < 6 && Cot - Dem >= 0 && Dong - Dem >= 0; Dem++)
            {
                if (matrix[Dong - Dem][Cot - Dem].BackgroundImage == list_player_pvc[0].Player_icon)
                {
                    iSoQuanTa++;
                    break;
                }

                if (matrix[Dong - Dem][Cot - Dem].BackgroundImage == list_player_pvc[1].Player_icon)
                    iSoQuanDich++;
                if (matrix[Dong - Dem][Cot - Dem].BackgroundImage == null)
                    break;
            }
            if (iSoQuanTa == 2)
                return 0;
            diemDefend += Defend_Score[iSoQuanDich];
            if (iSoQuanDich > 0)
                diemDefend -= Attack_Score[iSoQuanTa] * 2;
            diemTong += diemDefend;

            return diemTong;
        }

        private long Score_Defend_Duyet_Cheo_Nguoc(int Dong, int Cot)
        {
            long diemTong = 0;
            long diemDefend = 0;
            int iSoQuanDich = 0;
            int iSoQuanTa = 0;
            for (int Dem = 1; Dem < 6 && Cot - Dem >= 0 && Dong + Dem < Common.CHESS_BOARD_HEIGHT; Dem++)
            {
                if (matrix[Dong + Dem][Cot - Dem].BackgroundImage == list_player_pvc[0].Player_icon)
                {
                    iSoQuanTa++;
                    break;
                }
                if (matrix[Dong + Dem][Cot - Dem].BackgroundImage == list_player_pvc[1].Player_icon)
                    iSoQuanDich++;
                if (matrix[Dong + Dem][Cot - Dem].BackgroundImage == null)
                    break;
            }
      
            for (int Dem = 1; Dem < 6 && Cot + Dem < Common.CHESS_BOARD_WIDTH && Dong - Dem >= 0; Dem++)
            {
                if (matrix[Dong - Dem][Cot + Dem].BackgroundImage == list_player_pvc[0].Player_icon)
                {
                    iSoQuanTa++;
                    break;
                }
                if (matrix[Dong - Dem][Cot + Dem].BackgroundImage == list_player_pvc[1].Player_icon)
                    iSoQuanDich++;
                if (matrix[Dong - Dem][Cot + Dem].BackgroundImage == null)
                    break;
            }
            if (iSoQuanTa == 2)
            {
                return 0;
            }
            diemDefend += Defend_Score[iSoQuanDich];
            if (iSoQuanDich > 0)
                diemDefend -= Attack_Score[iSoQuanTa] * 2;
            diemTong += diemDefend;

            return diemTong;
        }
        #endregion

        // Hàm này để show ra thuật toán của computer vừa đánh 
        public string ShowComputerAlgorithm()
        {
            string debug = "Nước Đi Máy đánh dã Sử dụng Hàm SearchButton Để Tính Vị Trí Như Sau : " + debug_computer[debug_computer.Count - 1];
            return debug;
        }
        // hàm này để in ra thuật toán của computer 
        public void PrintComputerAlgorithm()
        {
           TextWriter writer = new StreamWriter(@"D:\Web\Learn-CS\debug_computer.txt");
           for(int i = 0; i < debug_computer.Count; i++)
            {
                writer.WriteLine("Nước Đi Máy Đánh " + (i + 1) + " : Đã Sử dụng Hàm SearchButton Để Tính Vị Trí Như Sau : " + debug_computer[i]);
                writer.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
            writer.Close();
        }
        // hàm thay đổi người chơi trong đánh với người
        public void ChangePlayer()
        {
            player_name.Text = list_player_pvp[current_player].Player_name;
            player_icon.Image = list_player_pvp[current_player].Player_icon;
        }
        // kiểm tra xem có đi lại được hay ko 
        public bool Undo()
        {
            if (playTimeLine.Count <= 0)
                return false;

            PlayInfo old_play = playTimeLine.Pop();
            Button btn = matrix[old_play.Point.Y][old_play.Point.X];
            btn.BackgroundImage = null;

            if (playTimeLine.Count <= 0)
            {
                current_player = 0;
            }
            else
            {
                old_play = playTimeLine.Peek();
                current_player = playTimeLine.Peek().CurrentPlayer == 1 ? 0 : 1;
            }
            ChangePlayer();
            return true;
        }
        // hàm endgame
        public void EndGame()
        {
            if (endedGame != null)
            {
                endedGame(this, new EventArgs());
            }
        }
        // kiểm tra có win hay ko
        public bool isEndGame(Button btn)
        {
            return isEndGameHorizontal(btn) || isEndGameVertical(btn) || isEndGamePrimaryDiagonal(btn) || isEndGameSecondaryDiagonal(btn);
        }
        // lấy ra tọa độ 1 điểm
        private Point getChessPoint(Button btn)
        {
            int vartical = Convert.ToInt32(btn.Tag);
            int Horizontal = matrix[vartical].IndexOf(btn);
            Point point = new Point(Horizontal, vartical);
            return point;
        }
        //  > 5 điểm liên tiếp theo hàng ngang
        public bool isEndGameHorizontal(Button btn)
        {
            // lấy ra tọa độ
            Point point = getChessPoint(btn);
            // matrix là matranaj cấc button
            int countLeft = 0, countLeft_Blocked = 0;
            for (int i = point.X; i >= 0; i--)
            {
                if (matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                }
                else if (matrix[point.Y][i].BackgroundImage != btn.BackgroundImage && matrix[point.Y][i].BackgroundImage != null)
                {
                    countLeft_Blocked++;
                    break;
                }
                else
                {
                    break;
                }
            }

            int countRight = 0, countRight_Blocked = 0;
            for (int i = point.X + 1; i < Common.CHESS_BOARD_WIDTH; i++)
            {
                if (matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else if (matrix[point.Y][i].BackgroundImage != btn.BackgroundImage && matrix[point.Y][i].BackgroundImage != null)
                {
                    countRight_Blocked++;
                    break;
                }
                else
                {
                    break;
                }
            }
            // kiểm tra xem đã đủ 5 điểm và không bị chặn 2 đầu ko
            if ((countLeft + countRight) > 5)
            {
                return true;
            } else if((countLeft + countRight) == 5)
            {
                if((countLeft_Blocked + countRight_Blocked) < 2)
                {
                    return true;
                } else
                {
                    return false;
                }
            } else
            {
                return false;
            }
        }
        // 5 điểm liên tiếp theo hàng dọc
        public bool isEndGameVertical(Button btn)
        {
            Point point = getChessPoint(btn);

            int countTop = 0, count_Top_blocked = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else if (matrix[i][point.X].BackgroundImage != btn.BackgroundImage && matrix[i][point.X].BackgroundImage != null)
                {
                    count_Top_blocked++;
                    break;
                }
                else
                {
                    break;
                }
            }

            int countBottom = 0, count_Bottom_blocked = 0;
            for (int i = point.Y + 1; i < Common.CHESS_BOARD_HEIGHT; i++)
            {
                if (matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else if (matrix[i][point.X].BackgroundImage != btn.BackgroundImage && matrix[i][point.X].BackgroundImage != null)
                {
                    count_Bottom_blocked++;
                    break;
                }
                else
                {
                    break;
                }
            }
            if ((countTop + countBottom) > 5)
            {
                return true;
            }
            else if ((countTop + countBottom) == 5)
            {
                if ((count_Top_blocked + count_Bottom_blocked) < 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        // 5 điểm liên tục theo chéo chính
        public bool isEndGamePrimaryDiagonal(Button btn)
        {

            Point point = getChessPoint(btn);

            int countTop = 0, countTop_Blocked = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X - i < 0 || point.Y - i < 0)
                    break;

                if (matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else if (matrix[point.Y - i][point.X - i].BackgroundImage != btn.BackgroundImage && matrix[point.Y - i][point.X - i].BackgroundImage != null)
                {
                    countTop_Blocked++;
                    break;
                }
                else
                {
                    break;
                }
            }

            int countBottom = 0, countBottom_Blocked = 0;
            for (int i = 1; i <= Common.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if (point.Y + i >= Common.CHESS_BOARD_HEIGHT || point.X + i >= Common.CHESS_BOARD_WIDTH)
                    break;

                if (matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else if (matrix[point.Y + i][point.X + i].BackgroundImage != btn.BackgroundImage && matrix[point.Y + i][point.X + i].BackgroundImage != null)
                {
                    countBottom_Blocked++;
                    break;
                }
                else
                {
                    break;
                }
            }
            if ((countTop + countBottom) > 5)
            {
                return true;
            }
            else if ((countTop + countBottom) == 5)
            {
                if ((countTop_Blocked + countBottom_Blocked) < 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        // 5 điểm liên tục theo chéo phụ
        public bool isEndGameSecondaryDiagonal(Button btn)
        {
            Point point = getChessPoint(btn);

            int countTop = 0, countTop_Blocked = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X + i > Common.CHESS_BOARD_WIDTH || point.Y - i < 0)
                    break;

                if (matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else if (matrix[point.Y - i][point.X + i].BackgroundImage != btn.BackgroundImage && matrix[point.Y - i][point.X + i].BackgroundImage != null)
                {
                    countTop_Blocked++;
                    break;
                }
                else
                {
                    break;
                }
            }

            int countBottom = 0, countBottom_Blocked = 0;
            for (int i = 1; i <= Common.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if (point.Y + i >= Common.CHESS_BOARD_HEIGHT || point.X - i < 0)
                    break;

                if (matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else if (matrix[point.Y + i][point.X - i].BackgroundImage != btn.BackgroundImage && matrix[point.Y + i][point.X - i].BackgroundImage != null)
                {
                    countTop_Blocked++;
                    break;
                }
                else
                {
                    break;
                }
            }
            if ((countTop + countBottom) > 5)
            {
                return true;
            }
            else if ((countTop + countBottom) == 5)
            {
                if ((countTop_Blocked + countBottom_Blocked) < 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        // chọn điểm 
        public void MarkPoint(Button btn)
        {
                btn.BackgroundImage = list_player_pvp[current_player].Player_icon;
        }
        #endregion
    }
}
