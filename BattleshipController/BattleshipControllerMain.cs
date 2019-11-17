using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipController
{
    public class BattleshipControllerMain
    {
        
        private BattleBoard battleboard = null;

        public void CreateBoard()
        {
            if (battleboard == null)
            {
                battleboard = new BattleBoard();
            }
            else
            {
                throw new Exception("Board has already been created. Proceed to add Battleships");
            }
        }

        public void AddBattleship(Coordinate startPos, int length, Battleship.BattleshipOrientation orientation)
        {
            Battleship battleship = new Battleship() {StartPos = startPos, Length = length, Orientation = orientation};
            
            if (battleboard != null)
            {
                battleboard.AddBattleShip(battleship);
            }
            else
            {
                throw new Exception("Battle board not created. Please create board first");
            }
        }

        public Battleship.ShipAttackStatus ProcessAttack(Coordinate attackCoordinate)
        {
            Battleship.ShipAttackStatus attackStatus = Battleship.ShipAttackStatus.MISS; 
            if (battleboard != null)
            {
                attackStatus = battleboard.ProcessAttack(attackCoordinate);
            }

            return attackStatus;
        }
    }
}
