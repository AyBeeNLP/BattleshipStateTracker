using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using BattleshipController;

namespace BattleshipStateTracker
{
    class Program
    {
        static void Main(string[] args)
        {

            BattleshipControllerMain battleShipController = new BattleshipControllerMain();

            string strInputOption = "";
            do
            {
                try
                {
                    Console.WriteLine("1 for Create Board");
                    Console.WriteLine("2 for Add Battleship");
                    Console.WriteLine("3 for Attack");
                    Console.WriteLine("q or Q for Quit");
                    strInputOption = Console.ReadLine();
                    
                    switch (strInputOption.ToLower())
                    {
                        case "1":
                            battleShipController.CreateBoard();
                            Console.WriteLine("Board created!");
                            break;
                        case "2":
                            Console.WriteLine("Enter Battleship start coordinates (X,Y): ");
                            
                            string strCoordinates = Console.ReadLine();
                            char[] splitters = new char[] {','};

                            Coordinate startCoordinate = null;
                            try
                            {
                                string[] startXY = strCoordinates.Split(splitters);
                                int xPart = Convert.ToInt32(startXY[0]);
                                int yPart = Convert.ToInt32(startXY[1]);
                                startCoordinate = new Coordinate(){X = xPart, Y = yPart};
                            }
                            catch (Exception)
                            {
                                throw new Exception("Invalid coordinates entered!");
                            }

                            Console.WriteLine("Battleship orientation (h for horizontal, v for vertical : ");
                            
                            string strOrientation = Console.ReadLine();
                            Battleship.BattleshipOrientation orientation = Battleship.BattleshipOrientation.UNKNOWN;
                            if (strOrientation.ToLower() == "h")
                            {
                                orientation = Battleship.BattleshipOrientation.HORIZONTAL;
                            }
                            else if (strOrientation.ToLower() == "v")
                            {
                                orientation = Battleship.BattleshipOrientation.VERTICAL;
                            }
                            else
                            {
                                throw new Exception("Invalid orientation entered!");
                            }
                            
                            Console.WriteLine("Enter length of Battleship: ");
                            
                            string strShipLen = Console.ReadLine();
                            int shipLen = 0;
                            try
                            {
                                shipLen = Convert.ToInt32(strShipLen);
                            }
                            catch (Exception)
                            {
                                throw new Exception("Invalid length entered!");
                            }

                            battleShipController.AddBattleship(startCoordinate, shipLen, orientation);

                            Console.WriteLine("Battleship added successfully!");

                            break;
                        case "3":
                            Console.WriteLine("Enter attack coordinates (X,Y) : ");
                            string strAttackCoordinates = Console.ReadLine();
                            Coordinate attackCoordinate = null;
                            try
                            {
                                char[] strSplitters = new char[] {','};
                                string[] attackXY = strAttackCoordinates.Split(strSplitters);
                                int xAttackPart = Convert.ToInt32(attackXY[0]);
                                int yAttackPart = Convert.ToInt32(attackXY[1]);
                                attackCoordinate = new Coordinate(){X = xAttackPart, Y = yAttackPart};

                                
                            }
                            catch (Exception)
                            {
                                throw new Exception( "Invalid coordinates entered!");
                            }
                            Battleship.ShipAttackStatus shipAttackStatus = battleShipController.ProcessAttack(attackCoordinate);

                            Console.WriteLine($"Attack result - {shipAttackStatus} !!");
                            break;
                        case "q": 
                        case "Q":
                            break;
                        default:
                            Console.WriteLine("Invalid selection, please try again");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }

            } while (strInputOption.ToLower() != "q" && strInputOption.ToLower() != "Q");


        }
    }
}
