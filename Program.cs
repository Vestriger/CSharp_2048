using System;

namespace sharp2048
{
    class Program
    {
        static int BoardSize = 4, SCORE, KeyEvent;
        static int[,] board = new int[4, 4];
        static bool GAME, WIN;
        static void Main(string[] args)
        {
            Random random = new Random();
            while (true)
            {
                SCORE = 0;
                GAME = true;
                WIN = false;
                for (int i = 0; i < BoardSize; i++)
                {
                    for (int j = 0; j < BoardSize; j++)
                    {
                        board[i, j] = 0;
                    }
                }

                board[random.Next(BoardSize - 1), random.Next(BoardSize - 1)] = random.Next(1, 3) * 2;

                DrawFrames();
                
                while(GAME)
                {
                    if(KeyDown() > -1)
                    {
                        if(Logic() != 0)
                        {
                            Console.WriteLine("\n\tGAME OVER");
                            GAME = false;
                            break;
                        }
                        DrawFrames();
                    }
                }

                Console.WriteLine("\nSpace - restart\nEsc - exit ");
                while (!GAME)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKey key = Console.ReadKey().Key;
                        switch (key)
                        {
                            case (ConsoleKey)32: GAME = true; break;
                            case (ConsoleKey)27: Environment.Exit(0); break;
                        }
                    }
                }
            }
        }
        static void DrawFrames() 
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t2048");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            if(WIN)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("   YOU ARE WIN!");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Magenta;
            }

            Console.WriteLine();

            for(int i = 0 ; i < BoardSize; i++)
            {
                Console.Write("   ");
                for (int j = 0; j < BoardSize; j++)
                {
                    switch (board[i, j])
                    {
                        case 0:
                            if (((i + j) % 2) != 0)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            break;
                        case 2:
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black; break;
                        case 4:
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.Black; break;
                        case 8:
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Black; break;
                        case 16:
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.ForegroundColor = ConsoleColor.Black; break;
                        case 32:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.Black; break;
                        case 64:
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.ForegroundColor = ConsoleColor.Black; break;
                        case 128:
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Black; break;
                        case 256:
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.Black; break;
                        case 512:
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.ForegroundColor = ConsoleColor.Black; break;
                        case 1024:
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                            Console.ForegroundColor = ConsoleColor.Black; break;
                        case 2048:
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            Console.ForegroundColor = ConsoleColor.Black; break;
                        default:
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black; break;
                    }
                    Console.Write($"  {board[i,j]}".PadRight(4));
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                if (i == 1) Console.Write($"    SCORE: {SCORE}");

                Console.WriteLine();
            }
        }
        static int KeyDown()
        {
            KeyEvent = -1;
            if (Console.KeyAvailable)
            {
                ConsoleKey KEY = Console.ReadKey().Key;
                switch (KEY)
                {
                    case (ConsoleKey)37: KeyEvent = 0; break;
                    case (ConsoleKey)40: KeyEvent = 1; break;
                    case (ConsoleKey)39: KeyEvent = 2; break;
                    case (ConsoleKey)38: KeyEvent = 3; break;
                }
            }
            return KeyEvent;
        }
        static int Logic()
        {
            for (int i = 0; i < KeyEvent; i++) BoardRotate();

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = BoardSize - 2; j > -1; j--)
                {
                    if (board[i,j] == 0 && board[i,j + 1] > 0)
                    {
                        for (int l = j; l < BoardSize - 1; l++)
                            board[i,l] = board[i,l + 1];
                        board[i,BoardSize - 1] = 0;
                    }
                }

                for (int j = 0; j < BoardSize - 1; j++)
                {
                    if (board[i, j] == board[i, j + 1])
                    {
                        for (int l = j; l < BoardSize - 1; l++)
                            board[i, l] = board[i, l + 1];
                        board[i, BoardSize - 1] = 0;

                        SCORE += (board[i, j] *= 2);

                        if (board[i, j] >= 2048) WIN = true;

                    }
                }
            }
           for (int i = KeyEvent; i < 4; i++) BoardRotate();

            int k = 0;
            for (int i = 0; i < BoardSize; i++)
                for (int j = 0; j < BoardSize; j++)
                    if (board[i,j] != 0) k++;
            if (k == BoardSize * BoardSize) return 1;

            int ri, rj;
            Random random = new Random();
            while (true)
            {
                ri = random.Next(BoardSize);
                rj = random.Next(BoardSize);
                if (board[ri,rj] == 0)
                {
                    board[ri,rj] = random.Next(1, 3) * 2;
                    return 0;
                }
            }
        }
        static void BoardRotate()
        {
            int tmp;
            for (int i = 0; i < board.GetLength(0) - 1; i++)
            {
                for (int j = i + 1; j < board.GetLength(1); j++)
                {
                    tmp = board[i, j];
                    board[i, j] = board[j, i];
                    board[j, i] = tmp;
                }
            }
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1) / 2; j++)
                {
                    tmp = board[i, j];
                    board[i, j] = board[i, BoardSize - j - 1];
                    board[i, BoardSize - j - 1] = tmp;
                }
            }
        }
    }
}
