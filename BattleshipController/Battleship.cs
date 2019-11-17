using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipController
{
    public class Battleship
    {
        public enum BattleshipOrientation
        {
            UNKNOWN = 0, 
            HORIZONTAL = 1,
            VERTICAL
        };

        public enum BattleshipStatus { SAFE = 0, UNDERATTACK, DESTROYED}

        public enum ShipAttackStatus {MISS = 0, HIT}

        private List<Coordinate> safeCoordinates = new List<Coordinate>();

        private List<Coordinate> hitCoordinates = new List<Coordinate>();

        
        public Coordinate StartPos { get; set; }
        public BattleshipOrientation Orientation { get; set; }
        public int Length { get; set; }
        public BattleshipStatus Status { get; set; }

        public Coordinate EndPos { get; set; }

        public void Deploy()
        {
            switch (Orientation)
            {
                case BattleshipOrientation.HORIZONTAL:
                    EndPos = new Coordinate(){X = StartPos.X + Length - 1, Y = StartPos.Y};
                    for (int xIdx = 0; xIdx < Length; ++xIdx)
                    {
                        safeCoordinates.Add(new Coordinate(){X = StartPos.X + xIdx, Y = StartPos.Y});
                    }
                    break;
                case BattleshipOrientation.VERTICAL:
                    EndPos = new Coordinate(){X = StartPos.X, Y = StartPos.Y + Length - 1};
                    for (int yIdx = 0; yIdx < Length; ++yIdx)
                    {
                        safeCoordinates.Add(new Coordinate(){X = StartPos.X, Y = StartPos.Y + yIdx});
                    }
                    break;
            }

            Status = BattleshipStatus.SAFE;
        }
        
        public ShipAttackStatus ProcessAttack(Coordinate attackCoordinate)
        {
            ShipAttackStatus attackStatus = ShipAttackStatus.MISS;

            if (Status != BattleshipStatus.DESTROYED)
            {
                switch (Orientation)
                {
                    case BattleshipOrientation.HORIZONTAL:
                        if (attackCoordinate.Y == StartPos.Y
                            && attackCoordinate.X >= StartPos.X
                            && attackCoordinate.X <= (StartPos.X + Length - 1))
                        {
                            foreach (var safeCoordinate in safeCoordinates)
                            {
                                if (safeCoordinate.X == attackCoordinate.X
                                    && safeCoordinate.Y == attackCoordinate.Y)
                                {
                                    attackStatus = ShipAttackStatus.HIT;
                                    hitCoordinates.Add(safeCoordinate);
                                    safeCoordinates.Remove(safeCoordinate);
                                    break;
                                }
                            }
                        }
                        break;
                    case BattleshipOrientation.VERTICAL:
                        if (attackCoordinate.X == StartPos.X
                            && attackCoordinate.Y >= StartPos.Y
                            && attackCoordinate.Y <= (StartPos.Y + Length - 1))
                        {
                            foreach (var safeCoordinate in safeCoordinates)
                            {
                                if (safeCoordinate.X == attackCoordinate.X
                                    && safeCoordinate.Y == attackCoordinate.Y)
                                {
                                    attackStatus = ShipAttackStatus.HIT;
                                    hitCoordinates.Add(safeCoordinate);
                                    safeCoordinates.Remove(safeCoordinate);
                                    break;
                                }
                            }
                        }
                        break;
                }

                if (safeCoordinates.Count == 0)
                {
                    Status = BattleshipStatus.DESTROYED;
                }
            }

            return attackStatus;
        }




    }
}
