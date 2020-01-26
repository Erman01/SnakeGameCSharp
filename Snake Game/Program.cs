using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    class Program
    {
        static void Main(string[] args)
        {


            int xPosition = 35;
            int yPosition = 20;
            int gameSpeed = 150;

            bool isGameOn = true;
            bool isWallHit = false;

            //Get the snake  to appear on screen

            Console.SetCursorPosition(xPosition, yPosition);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine((char)2);
          

            // Build boundary

            buildWall();

            // Get snake to move

            ConsoleKey command = Console.ReadKey().Key;

            do
            {
                switch (command)
                {
                    
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        xPosition--;

                        break;
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        yPosition--;

                        break;
                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        xPosition++;

                        break;

                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        yPosition++;

                        break;
                   
                   
                }
                Console.SetCursorPosition(xPosition, yPosition);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine((char)2);

                isWallHit = DidSnakeHitWall(xPosition, yPosition);
                if (isWallHit)
                {
                    isGameOn = false;
                    Console.SetCursorPosition(28, 20);
                    Console.WriteLine("Snake hit the wall and died");
                }
                if (Console.KeyAvailable) command = Console.ReadKey().Key;
                System.Threading.Thread.Sleep(gameSpeed);

            } while (isGameOn);

            //Detect when snack hits boundry
            //Slow game down
           
            
           



            //End Vid1

            //Place apple on board (random)

            //Detect when apple was eaten
            //Make snake faster
            //Make snake longer
            //keep track of how many apple was eaten

            //End vid 2

            //Build wellcome screen

            //Give player option to read direction

            //show score

            //Give player option to replay the game
        }

        private static bool DidSnakeHitWall(int xPosition, int yPosition)
        {
            if (xPosition == 1 || xPosition == 70 || yPosition == 1 || yPosition == 40) return true;return false;
           
        }

        private static void buildWall()
        {
            for (int i = 1; i < 41; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(1, i);
                Console.Write("N");
                Console.SetCursorPosition(70, i);
                Console.Write("S");
               
            }
            for (int i = 1; i < 71; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(i, 1);
                Console.Write("J");
                Console.SetCursorPosition(i,40);
                Console.Write("O");
            }
            Console.ReadLine();
        }
        
    }
}
