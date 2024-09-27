using System;
using System.Threading;
using System.IO;
namespace RANSANMOI
{
    class Program
    {
        public static void Main(string[] args)
        {
            SnakeControl snakeGame = new SnakeControl(); // Khởi tạo đối tượng SnakeControl

            

            Thread _game = new Thread(snakeGame.ListenKey); // Khởi tạo luồng lắng nghe các phím bấm
            _game.Start();

            do
            {
                Console.Clear();                 // Xóa màn hình để vẽ lại bảng trong mỗi lần cập nhật
                snakeGame.Drawboard();           // Vẽ bảng và các đối tượng (rắn, mồi, tường)
                snakeGame.setUpBoard();          // Thiết lập bảng trò chơi
                snakeGame.MoveSnakeHead();       // Di chuyển đầu rắn dựa trên hướng
                snakeGame.EatFood();             // Kiểm tra xem rắn có ăn mồi không
                snakeGame.SpawnBody();           // Sinh mồi mới nếu mồi đã bị ăn
                snakeGame.PopUpfood();           // Vẽ mồi mới
                snakeGame.ShowPoint();           // Hiển thị điểm số
                Task.Delay(SnakeControl.speed).Wait(); // Tạm dừng để điều chỉnh tốc độ di chuyển của rắn

            } while (true);  // Vòng lặp vô hạn để game chạy liên tục
        }
    }
}
