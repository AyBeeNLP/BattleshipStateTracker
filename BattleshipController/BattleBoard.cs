using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipController
{
    public class BattleBoard
    {
        private const int BOARD_WIDTH = 10;
        private const int BOARD_HEIGHT = 10;
        
        private List<Battleship> battleships = new List<Battleship>();

        private bool isBattleshipsOverlapping(Battleship battleship, ref Battleship battleshipIn)
        {
            bool overlapping = !(battleshipIn.StartPos.Y > battleship.EndPos.Y
                                 || battleshipIn.EndPos.Y < battleship.StartPos.Y
                                 || battleshipIn.StartPos.X > battleship.EndPos.X
                                 || battleshipIn.EndPos.X < battleship.StartPos.X);

            return overlapping;
        }
        public void AddBattleShip(Battleship battleshipIn)
        {
            //Check no battle ship at location then add else 
            //raise battleshi[p exists at location exception
            if (battleshipIn.StartPos.X < BOARD_WIDTH && battleshipIn.StartPos.X >= 0
                                                       && battleshipIn.StartPos.Y < BOARD_HEIGHT &&
                                                       battleshipIn.StartPos.Y >= 0)
            {
                if ((battleshipIn.Orientation == Battleship.BattleshipOrientation.HORIZONTAL
                     && battleshipIn.StartPos.X + battleshipIn.Length > BOARD_WIDTH - 1)
                    ||
                    (battleshipIn.Orientation == Battleship.BattleshipOrientation.VERTICAL
                     && battleshipIn.StartPos.Y + battleshipIn.Length > BOARD_WIDTH - 1))
                {
                    throw new Exception("Battleship size does not fit board!");
                }

                battleshipIn.Deploy();
                
                bool overlapping = false;

                foreach (var battleship in battleships)
                {
                    overlapping = isBattleshipsOverlapping(battleship, ref battleshipIn);

                    if (overlapping)
                    {
                        break;
                    }
                }

                if (!overlapping)
                {
                    battleships.Add(battleshipIn);
                }
                else
                {
                    throw new Exception("Battleship deployment failed as overlapping with existing ones!");
                }
            }
            else
            {
                throw new Exception("Invalid start coordinates for battleship!");
            }
        }

        public Battleship.ShipAttackStatus ProcessAttack(Coordinate attackCoordinate)
        {
            Battleship.ShipAttackStatus attackStatus = Battleship.ShipAttackStatus.MISS;

            if (battleships.Count > 0)
            {
                bool isAnyShipStillAlive =
                    battleships.Any(battleship => battleship.Status != Battleship.BattleshipStatus.DESTROYED);

                if (isAnyShipStillAlive)
                {
                    foreach (var battleship in battleships)
                    {
                        attackStatus = battleship.ProcessAttack(attackCoordinate);

                        if (attackStatus == Battleship.ShipAttackStatus.HIT)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    throw new Exception("All battleships have been destroyed!");
                }
            }
            else
            {
                throw new Exception("No battleships on board. Please add battleship.");
            }
            
            return attackStatus;
        }
    }
}
