using System;
using System.Threading;

namespace spaceWar2
{
    class Program
    {
        public Random random = new Random();
        public ConsoleKeyInfo keyPress = new ConsoleKeyInfo();

        int score, playerX, playerY, enemyX, enemyY, firePosY, firePosX;

        const int height = 20;
        const int width = 80;

        bool gameOver, reset, shoot;
        string dir;

        void Load()
        {

            Console.SetWindowSize(width, height + 4);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorVisible = false;

            for (int load1 = 0; load1 < 15; load1++)
            {
                Console.SetCursorPosition(width / 2 - 6, height / 2);
                Console.Write("Loading");
                Thread.Sleep(200);
                Console.Write(" .");
                Thread.Sleep(200);
                Console.Write(" .");
                Thread.Sleep(200);
                Console.Write(" .");
                Thread.Sleep(200);
                Console.Clear();
            }
        }

        void ShowBanner()
        {
            Console.WriteLine("||============================================================================||");
            Console.WriteLine("||----------------------------------------------------------------------------||");
            Console.WriteLine("||-------------------------- Welcome to Spacewar! ----------------------------||");
            Console.WriteLine("||----------------------------------------------------------------------------||");
            Console.WriteLine("||============================================================================||");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                            PRESS ANY KEY TO PLAY :)                            ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("          Controller:- Use Arrow buttons UP and DOWN to move the plane          ");
            Console.WriteLine("                            - Press Space to shoot                              ");
            Console.WriteLine("                            - Press S to pause                                  ");
            Console.WriteLine("                            - Press ESC to quit game                            ");

            keyPress = Console.ReadKey(true);
            if (keyPress.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        void Setup()
        {
            dir = "";
            score = 0;

            gameOver = false;
            reset = false;
            shoot = false;

            playerX = 1;
            playerY = 10;

            enemyX = width - 1;
            enemyY = random.Next(1, height - 1);
        }

        void CheckInput()
        {
            while (Console.KeyAvailable)
            {
                keyPress = Console.ReadKey();
                if (keyPress.Key == ConsoleKey.Escape)
                    Environment.Exit(0);

                if (keyPress.Key == ConsoleKey.S)
                {
                    dir = "STOP";
                }
                else if (keyPress.Key == ConsoleKey.UpArrow && playerY > 1)
                {
                    dir = "UP";
                }
                else if (keyPress.Key == ConsoleKey.DownArrow && playerY < 18)
                {
                    dir = "DOWN";
                }
                else if (keyPress.Key == ConsoleKey.Spacebar)
                {
                    shoot = true;
                    firePosX = playerX;
                    firePosY = playerY;
                }
            }
        }

        void Logic()
        {
            switch (dir)
            {
                case "UP":
                    playerY--;
                    break;
                case "DOWN":
                    playerY++;
                    break;
                case "STOP":
                    while (true)
                    {
                        Console.Clear();
                        Console.CursorLeft = width / 2 - 6;
                        Console.WriteLine("GAME PAUSED");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("   - Press S to resume game");
                        Console.WriteLine("   - Press R to reset game");
                        Console.Write("   - Press ESC to quit game");
                        keyPress = Console.ReadKey(true);
                        if (keyPress.Key == ConsoleKey.Escape)
                            Environment.Exit(0);
                        if (keyPress.Key == ConsoleKey.R)
                        {
                            reset = true;
                            break;
                        }
                        if (keyPress.Key == ConsoleKey.S)
                            break;
                    }
                    break;
            }
            if (firePosX == enemyX && firePosY == enemyY || firePosX == enemyX + 1 && firePosY == enemyY)
            {
                score += 10;
                enemyX = width - 1;
                enemyY = random.Next(1, height - 1);
                firePosX = width;
            }

            if (enemyX == 0)
            {
                gameOver = true;
            }

            dir = "";
        }

        void Render()
        {
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == height - 1)
                    {
                        Console.Write("#");
                    }
                    else if (j == 0 || j == width - 1)
                    {
                        Console.Write("#");
                    }
                    else if (j == playerX && i == playerY)
                    {
                        Console.Write(">");
                    }
                    else if (j == enemyX && i == enemyY)
                    {
                        Console.Write("<");
                    }
                    else if (shoot && i == firePosY && j == firePosX + 1)
                    {
                        Console.Write("-");
                    }
                    else
                    {
                            Console.Write(" ");
                    }

                }
                Console.WriteLine();
            }
            Console.WriteLine("Your score: " + score);
            firePosX++;
            enemyX--;
        }

        void Lose()
        {
            Console.CursorTop = height + 1;
            Console.CursorLeft = width / 2 - 4;
            Console.WriteLine("YOU LOSED");
            Console.WriteLine("Press R to reset game");
            Console.Write("Press ESC to quit game");
            while (true)
            {
                keyPress = Console.ReadKey(true);
                if (keyPress.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                if (keyPress.Key == ConsoleKey.R)
                {
                    reset = true;
                    break;
                }
            }
        }

        void Update()
        {
            while (!gameOver)
            {
                CheckInput();
                Logic();
                Render();
                if (reset)
                    break;
            }
            if (gameOver)
                Lose();
        }

        static void Main(string[] args)
        {
            Program spacewar = new Program();
            spacewar.Load();
            spacewar.ShowBanner();
            while (true)
            {
                spacewar.Setup();
                spacewar.Update();
                Console.Clear();
            }
        }
    }
}
