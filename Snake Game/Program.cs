﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace Snake_Game
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Vars
            int[] xPosition = new int[50];
            xPosition[0] = 35;
            int[] yPosition = new int[50];
            yPosition[0] = 20;
            int appleXDim = 10;
            int appleYDim = 10;
            int applesEaten = 0;

            string userAction = "";


            decimal gameSpeed = 150m;

            bool isGameOn = true;
            bool isWallHit = false;
            bool appleIsEaten = false;
            bool isStayInMenu = false;

            Random random = new Random();
            Console.CursorVisible = false;

            //Build wellcome screen
            showMenu(out userAction);

            #endregion
            do
            {

                switch (userAction)
                {
                    //Give player option to read direction
                    #region Case directions
                    case "1":
                    case "d":
                    case "directions":
                        Console.Clear();
                        buildWall();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(5, 5);
                        Console.WriteLine("1) Resize the console windows so you can see all");
                        Console.SetCursorPosition(5, 6);
                        Console.WriteLine("  4 sides of playing field boarder.");
                        Console.SetCursorPosition(5, 7);
                        Console.WriteLine("2) Use the arrow key to move the snake around the field");
                        Console.SetCursorPosition(5, 8);
                        Console.WriteLine("3) The snake will die if it runs into the wall");
                        Console.SetCursorPosition(5, 9);
                        Console.WriteLine("4) You gain points by eating the apple");
                        Console.SetCursorPosition(5, 10);
                        Console.WriteLine("  but your snake will also go faster and get longer");
                        Console.SetCursorPosition(5, 12);
                        Console.WriteLine("Press enter to return to the main menu");
                        Console.ReadLine();
                        Console.Clear();
                        showMenu(out userAction);
                        break;
                    #endregion

                    #region Case Play
                    case "2":
                    case "p":
                    case "play":
                        Console.Clear();
                        #region Game Setup
                        //Get the snake  to appear on screen

                        paintSnake(applesEaten, xPosition, yPosition, out xPosition, out yPosition);

                        //Set apple on the screen

                        setApplePositionOnScreen(random, out appleXDim, out appleYDim);
                        paintApple(appleXDim, appleYDim);

                        // Build boundary

                        buildWall();
                        ConsoleKey command = Console.ReadKey().Key;

                        #endregion
                        // Get snake to move


                        do
                        {
                            #region Change Direction
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
                            #endregion

                            #region Playing the Game
                            //Paint the snake, Make snake longerS
                            paintSnake(applesEaten, xPosition, yPosition, out xPosition, out yPosition);

                            isWallHit = DidSnakeHitWall(xPosition[0], yPosition[0]);
                            if (isWallHit)
                            {
                                isGameOn = false;
                                Console.SetCursorPosition(28, 20);
                                Console.WriteLine("Snake hit the wall and died");
                                //Show score
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.SetCursorPosition(15, 21);
                                Console.Write("Your score is " + applesEaten * 100 + "!");
                                Console.SetCursorPosition(15, 22);
                                Console.WriteLine("Press Enter to Continue ...");
                                
                                applesEaten = 0;
                                Console.ReadLine();
                                Console.Clear();
                                //Give player option to replay the game
                                showMenu(out userAction);
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
                                applesEaten = applesEaten + 5;

                                //Make snake faster
                                gameSpeed *= .925m;

                            }

                            if (Console.KeyAvailable) command = Console.ReadKey().Key;
                            System.Threading.Thread.Sleep(Convert.ToInt32(gameSpeed));

                            #endregion
                        } while (isGameOn);

                        break;
                    #endregion

                    case "3":
                    case "e":
                    case "exit":
                        isStayInMenu = false;
                        Console.Clear();
                        break;


                    default:
                        Console.WriteLine("Your input was not understood, press enter and try again");
                        Console.ReadLine();
                        Console.Clear();
                        showMenu(out userAction);
                        break;
                }
            } while (isStayInMenu);


        }


        #region Methods
        #region Menu
        private static void showMenu(out string userAction)
        {

            string menu1 = "1) Directions\n2) Play\n3) Exit \n\n\n" + @"


             

                           /\|/\
                           |0/0|
                            ### |~~~                |~~~                 |~~~                    |~~~~~|
                                    |~~~~        |~~~    |~~~~       |~~~~     |~~~~        |~~~~ 
                                         |~~~~~~|             |~~~~~|                |~~~~~|
                                                                                    

                          
";
            string menu2 = "1) Directions\n2) Play\n3) Exit \n\n\n" + @"



                      /\|/\
                      |0/0| 
                       ### |~~~                                      |~~~     
                               |~~~~                            |~~~~     |~~~~                |~~~~  
                                    |~~~~~~|                |~~~~~|          |~~~~~|      |~~~~
                                             |~~~~     |~~~~                       |~~~~~|    
                                                  |~~~
";

            string menu3 = "1) Directions\n2) Play\n3) Exit \n\n\n" + @"




                /\|/\
                |0/0|
                 ### |~~~                |~~~                 |~~~                    |~~~~~|
                         |~~~~        |~~~    |~~~~       |~~~~     |~~~~        |~~~~ 
                              |~~~~~~|             |~~~~~|                |~~~~~|
";

            string menu4 = "1) Directions\n2) Play\n3) Exit \n\n\n" + @"





             /\|/\
             |0/0| 
              ### |~~~                                      |~~~     
                      |~~~~                            |~~~~     |~~~~                |~~~~  
                             |~~~~~~|                |~~~~~|          |~~~~~|      |~~~~
                                      |~~~~     |~~~~                       |~~~~~|    
                                           |~~~
";

            string menu5 = "1) Directions\n2) Play\n3) Exit \n\n\n" + @"





        /\|/\
        |0/0|
         ### |~~~                |~~~                 |~~~                    |~~~~~|
                |~~~~        |~~~    |~~~~       |~~~~     |~~~~        |~~~~ 
                     |~~~~~~|             |~~~~~|                |~~~~~|
";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(menu1);
            System.Threading.Thread.Sleep(100);
            Console.Clear();

            Console.WriteLine(menu2);
            System.Threading.Thread.Sleep(100);
            Console.Clear();

            Console.WriteLine(menu3);
            System.Threading.Thread.Sleep(100);
            Console.Clear();

            Console.WriteLine(menu4);
            System.Threading.Thread.Sleep(100);
            Console.Clear();

            Console.WriteLine(menu5);
            System.Threading.Thread.Sleep(100);

            SpeechSynthesizer toSpeak = new SpeechSynthesizer();
            toSpeak.SetOutputToDefaultAudioDevice();
            toSpeak.Speak("The Snake Game");
            toSpeak.Speak("Good Luck");

            userAction = Console.ReadLine().ToLower();

        }
        #endregion
        private static void paintSnake(int applesEaten, int[] xPositionIn, int[] yPositionIn, out int[] xPositionOut, out int[] yPositionOut)
        {
            //Paint the head

            Console.SetCursorPosition(xPositionIn[0], yPositionIn[0]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine((char)214);

            //Paint the Body

            for (int i = 1; i < applesEaten + 1; i++)
            {
                Console.SetCursorPosition(xPositionIn[i], yPositionIn[i]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("o");
            }

            //Erase last part of snake

            Console.SetCursorPosition(xPositionIn[applesEaten + 1], yPositionIn[applesEaten + 1]);
            Console.WriteLine(" ");

            //Record location of each body part 

            for (int i = applesEaten + 1; i > 0; i--)
            {
                xPositionIn[i] = xPositionIn[i - 1];
                yPositionIn[i] = yPositionIn[i - 1];
            }

            //Return the new array
            xPositionOut = xPositionIn;
            yPositionOut = yPositionIn;
        }// End Paint snake

        private static bool DidSnakeHitWall(int xPosition, int yPosition)
        {
            if (xPosition == 1 || xPosition == 70 || yPosition == 1 || yPosition == 40) return true; return false;

        }

        private static void buildWall()
        {
            for (int i = 1; i < 41; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(1, i);
                Console.Write("#");
                Console.SetCursorPosition(70, i);
                Console.Write("#");

            }
            for (int i = 1; i < 71; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(i, 1);
                Console.Write("#");
                Console.SetCursorPosition(i, 40);
                Console.Write("#");
            }

        }//End build wall
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

        }
        #endregion
    }
}
