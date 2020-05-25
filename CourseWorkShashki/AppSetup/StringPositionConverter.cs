namespace Checkers.AppSetup
{
    using System.Linq;
    using ConsoleApp;
    using GameField;

    public class StringPositionConverter
    {
        public (Position from, Position to) ToPositions(string command, Field field)
        {
            var parameters = ToParameters(command);
            if (parameters.Strings.Count != 2) return (null, null);
            var from = ToPosition(parameters.Strings[0], field);
            var to = ToPosition(parameters.Strings[1], field);
            return (from, to);
        }
        
        private Parameters ToParameters(string command)
        {
            return command.Trim().Split(' ')
                .Aggregate(new Parameters(), (acc, cur) => acc.AddParameter("$", cur));
        }
        
        private Position ToPosition(string input, Field field)
        {
            if (input.Length != 2) return null;
            input = input.ToUpperInvariant();
            var letter = input[0];
            var number = input[1];
            if (int.TryParse(number.ToString(), out var num))
            {
                var y = letter - 65;
                var x = 8 - num;
                return field.GetPosition(x, y);
            }
            return null;
        }
    }
}