using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Caro
{
    // Lớp chung chưa nhưng thuộc tính hay sử dụng
    public class Common
    {
        #region Properties
        // kích thước 1 ô
        public static int chess_width = 30;
        public static int chess_height = 30;
        // số hàng và cột của bảng
        public static int CHESS_BOARD_WIDTH;
        public static int CHESS_BOARD_HEIGHT;

        // xét thời gian đánh
        public static int COOL_DOWN_STEP = 100; // 0.1s
        public static int COOL_DOWN_TIME = 15000; // 10 s
        public static int COOL_DOWN_INTERVAL = 100;

        // xác định xem khi mới vào thì chơi với máy hay với người 
        public static string StartPV;
        #endregion
        #region Initialize
    // Hàm tạo để lấy dữ liệu nhập vào số dòng và số cột
    public Common(int chess_board_column_copy , int chess_board_row_copy)
        {
            CHESS_BOARD_WIDTH = chess_board_column_copy;
            CHESS_BOARD_HEIGHT = chess_board_row_copy;
        }
        #endregion
        #region Methods
        #endregion
    }
}
