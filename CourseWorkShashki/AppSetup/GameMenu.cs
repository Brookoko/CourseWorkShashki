namespace AppSetup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Commands;
    using ConsoleApp;
    using DependencyInjection;
    using GameField;
    using Movements;

    public class GameMenu : IConsoleMenu
    {
        [Inject]
        public IMovementProvider MovementProvider { get; set; }
        
        [Inject]
        public ICommandQueue CommandQueue { get; set; }
        
        public string Id => "Game";
        
        private readonly Field field = new Field();
        private readonly Drawer drawer = new Drawer();
        
        public bool ValidateInput(string command)
        {
            var (from, to) = ToPositions(command);
            return ValidPositions(from, to) && TryGetMovement(from, to, out _);
        }

        private (Position from, Position to) ToPositions(string command)
        {
            var parameters = ToParameters(command);
            if (parameters.Ints.Count != 4) return (null, null);
            var from = field.GetPosition(parameters.Ints[0], parameters.Ints[1]);
            var to = field.GetPosition(parameters.Ints[2], parameters.Ints[3]);
            return (from, to);
        }

        private Parameters ToParameters(string command)
        {
            return command.Trim().Split(' ')
                .Aggregate(new Parameters(), (acc, cur) => acc.AddParameter("#", cur));
        }
        
        private bool ValidPositions(Position from, Position to)
        {
            if (from == null) Console.WriteLine("Invalid start position");
            else if (to == null) Console.WriteLine("Invalid target position");
            return from != null && to != null;
        }
        
        private bool TryGetMovement(Position from, Position to, out IMovementRule rule)
        {
            var rejections = new List<string>();
            rule = MovementProvider.RuleFor(from, to, field, rejections);
            if (rule != null) return true;
            foreach (var reason in rejections)
            {
                Console.WriteLine(reason);
            }
            return false;
        }

        public void RunCommand(string command)
        {
            var (from, to) = ToPositions(command);
            var rule = MovementProvider.RuleFor(from, to, field, new List<string>());
            CommandQueue.Execute(rule.ToCommand(from, to, field));
        }
        
        public void PrintPrompt()
        {
            drawer.Draw(field);
            Console.Write("Move: ");
        }
    }
}