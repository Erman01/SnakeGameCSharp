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


            int[] xPosition = new int[50];
               xPosition[0]= 35;
            int[] yPosition = new int[50]; 
               yPosition[0]= 20;
            int appleXDim = 10;
            int appleYDim = 10;
            int applesEaten = 0;


            decimal gameSpeed = 150m;

            bool isGameOn = true;
            bool isWallHit = false;
            bool appleIsEaten = false;

            Random random = new Random();
            Console.CursorVisible = false;

            //Get the snake  to appear on screen

            paintSnake(applesEaten,xPosition, yPosition, out xPosition, out yPosition);

            //Set apple on the screen

            setApplePositionOnScreen(random, out appleXDim, out appleYDim);
            paintApple(appleXDim, appleYDim);

            // Build boundary

            buildWall();

            // Get snake to move

            ConsoleKey command = Console.ReadKey().Key;

            do
            {
                switch (command)
                {
                    
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        xPosition[0]--;

                        break;
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        yPosition[0]--;

                        break;
                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        xPosition[0]++;

                        break;

                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        yPosition[0]++;

                        break;
                   
                   
                }
                //Paint the snake, Make snake longerS
                paintSnake(applesEaten, xPosition, yPosition, out xPosition, out yPosition);

               

                isWallHit = DidSnakeHitWall(xPosition[0], yPosition[0]);
                if (isWallHit)
                {
                    isGameOn = false;
                    Console.SetCursorPosition(28, 20);
                    Console.WriteLine("Snake hit the wall and died");
                }
                //Detect when apple was eaten
                appleIsEaten = determineIfAppleWasEaten(xPosition[0], yPosition[0], appleXDim, appleYDim);


                //Place apple on board (random)

                if (appleIsEaten)
                {
                    setApplePositionOnScreen(random, out appleXDim, out appleYDim);
                    paintApple(appleXDim, appleYDim);

                    //keep track of how many apple was eaten
                    //Make snake longer
                    applesEaten++;

                    //Make snake faster
                    gameSpeed *= .925m;

                }
                


                if (Console.KeyAvailable) command = Console.ReadKey().Key;
                System.Threading.Thread.Sleep(Convert.ToInt32( gameSpeed));

            } while (isGameOn);

            //Detect when snack hits boundry
            //Slow game down
           


            //End Vid1

            

           
         
           
           

            //End vid 2

            //Build wellcome screen

            //Give player option to read direction

            //show score

            //Give player option to replay the game
        }

        private static void paintSnake(int applesEaten, int[] xPositionIn, int[] yPositionIn, out int[] xPositionOut, out int[] yPositionOut)
        {
            //Paint the head

            Console.SetCursorPosition(xPositionIn[0], yPositionIn[0]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine((char)214);

            //Paint the Body

            for (int i = 1; i < applesEaten+1; i++)
            {
                Console.SetCursorPosition(xPositionIn[i], yPositionIn[i]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("o");
            }

            //Erase last part of snake

            Console.SetCursorPosition(xPositionIn[applesEaten + 1], yPositionIn[applesEaten + 1]);
            Console.WriteLine(" ");

            //Record location of each body part 

            for (int i = applesEaten+1; i >0; i--)
            {
                xPositionIn[i] = xPositionIn[i - 1];
                yPositionIn[i] = yPositionIn[i - 1];
            }

            //Return the new array
            xPositionOut = xPositionIn;
            yPositionOut = yPositionIn;
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
        private static void setApplePositionOnScreen(Random random, out int appleXDim, out int appleYDim)
        {
            appleXDim = random.Next(0 + 2, 70 - 2);
            appleYDim = random.Next(0 + 2, 40 - 2);

        }

        private static void paintApple(int appleXDim, int appleYDim)
        {
            Console.SetCursorPosition(appleXDim, appleYDim);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write((char)64);
        }
        private static bool determineIfAppleWasEaten(int xPosition, int yPosition, int appleXDim, int appleYDim)
        {
            if (xPosition == appleXDim && yPosition == appleYDim) return true; return false;
            {

            }
        }

    }
}
