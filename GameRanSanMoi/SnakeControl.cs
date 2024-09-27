using System;
using System.IO;
namespace RANSANMOI
{
    public class SnakeControl
    {
        public static Point food = new Point(8, 8);
        public static bool foodExist = false;
        public static int speed = 400;
        public static int row = 20;
        public static int col = 40;
        public static string direction = "Right";
        public static int score;
        public static Point[] body = new Point[1] { new Point(4, 4) };
        public static Point _head = new Point(10, 10);
        public static string[,] board = new string[row, col];

        // Xử lý vẽ bảng
        public void Drawboard()
        {
            try
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        if (i == 0 || i == row - 1 || j == 0 || j == col - 1)
                        {
                            board[i, j] = "#";
                        }
                        else if (i == _head.X && j == _head.Y)
                        {
                            board[i, j] = "O";
                        }
                        else
                        {
                            bool isBodyPart = false;
                            for (int count = 0; count < body.Length; count++)
                            {
                                if (i == body[count].X && j == body[count].Y)
                                {
                                    board[i, j] = "+";
                                    isBodyPart = true;
                                    break;
                                }
                            }
                            if (!isBodyPart)
                            {
                                if (i == food.X && j == food.Y)
                                {
                                    board[i, j] = "@";
                                }
                                else
                                {
                                    board[i, j] = " ";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi khi ve bang: " + ex.Message);
            }
        }

        // Hiển thị bảng trò chơi
        public void setUpBoard()
        {
            try
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        Console.Write(board[i, j]);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi thiet lap bang: " + ex.Message);
            }
        }

        // Di chuyển đầu rắn
        public void MoveSnakeHead()
        {
            try
            {
                switch (direction)
                {
                    case "Right":
                        _head.Y += 1;
                        if (_head.Y == col - 1) _head.Y = 1;
                        break;
                    case "Left":
                        _head.Y -= 1;
                        if (_head.Y == 0) _head.Y = col - 1;
                        break;
                    case "Up":
                        _head.X -= 1;
                        if (_head.X == 0) _head.X = row - 1;
                        break;
                    case "Down":
                        _head.X += 1;
                        if (_head.X == row - 1) _head.X = 1;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi di chuyen dau ran: " + ex.Message);
            }
        }

        // Sinh mồi
        public void PopUpfood()
        {
            try
            {
                Random random = new Random();
                int x = random.Next(1, row - 1);
                int y = random.Next(1, col - 1);
                if (x != _head.X && y != _head.Y)
                {
                    if (foodExist == false)
                    {
                        food.X = x;
                        food.Y = y;
                        foodExist = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi tao food: " + ex.Message);
            }
        }

        // Kiểm tra rắn có ăn mồi không
        public void EatFood()
        {
            try
            {
                if (_head.X == food.X && _head.Y == food.Y)
                {
                    score += 1;
                    Array.Resize(ref body, body.Length + 1);
                    body[body.Length - 1] = new Point(-1, -1);
                    speed -= 20;
                    foodExist = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi khi ran an food: " + ex.Message);
            }
        }

        // Tạo thêm phần thân rắn
        public void SpawnBody()
        {
            try
            {
                for (int i = body.Length - 1; i > 0; i--)
                {
                    body[i].X = body[i - 1].X;
                    body[i].Y = body[i - 1].Y;
                }
                if (body.Length > 0)
                {
                    body[0].X = _head.X;
                    body[0].Y = _head.Y;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi khi tao them than ran: " + ex.Message);
            }
        }

        // Hiển thị điểm số
        public void ShowPoint()
        {
            Console.WriteLine($"Score: {score}");
        }

        // để em bổ sung phần ghi điểm
        public void SaveScore()
        {
            try
            {   
                string textPath = @"C:\Users\Admin\OneDrive\Desktop\Hoc-Code-game\C#\GameRanSanMoi\score.txt";
                using (StreamWriter writer = new StreamWriter(textPath, true))
                {
                    writer.WriteLine(score);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi ghi diem vao file: " + ex.Message);
            }
        }

        // Đọc điểm từ file
        
        
        

        // Lắng nghe các phím điều khiển và tính năng tạm dừng
        public void ListenKey()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                    switch (keyinfo.Key)
                    {
                        case ConsoleKey.P:
                            PauseGame();
                            break;
                        case ConsoleKey.RightArrow:
                            if (direction != "Left") direction = "Right";
                            break;
                        case ConsoleKey.LeftArrow:
                            if (direction != "Right") direction = "Left";
                            break;
                        case ConsoleKey.UpArrow:
                            if (direction != "Down") direction = "Up";
                            break;
                        case ConsoleKey.DownArrow:
                            if (direction != "Up") direction = "Down";
                            break;
                    }
                }
            }
        }

        // em thử pause game nhưng nó dính lặp vô hạn nên chưa xử lí đ
        public void PauseGame()
        {
            Console.WriteLine("Game pause. Press control to continue...");
            Console.ReadKey(true); // Chờ nhấn phím bất kỳ để tiếp tục
        }
    }
}
