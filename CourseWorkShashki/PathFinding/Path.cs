namespace Checkers.PathFinding
{
    using System.Collections.Generic;
    using System.Linq;
    using GameField;

    public class Path
    {
        public readonly List<Position> Opponents = new List<Position>();
        public readonly List<Position> Positions = new List<Position>();
        
        public int Length => Positions.Count;
        
        public Path(List<Position> positions, List<Position> opponents)
        {
            Positions.AddRange(positions);
            Opponents.AddRange(opponents);
        }
        
        public Path(Position position)
        {
            Positions.Add(position);
        }
        
        public Position Last()
        {
            return Positions.First();
        }
        
        public Position First()
        {
            return Positions.Last();
        }
        
        public bool Has(Position position)
        {
            return Positions.Contains(position);
        }
    }
}