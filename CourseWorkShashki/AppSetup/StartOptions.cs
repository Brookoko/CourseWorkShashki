namespace AppSetup
{
    using System;
    using System.Collections.Generic;
    using Commands;
    using ConsoleApp;
    using DependencyInjection;
    using GameField;
    using Movements;
    
    public class StartOptions : Options
    {
        [Inject]
        public IMovementProvider MovementProvider { get; set; }

        [Inject]
        public ICommandQueue CommandQueue { get; set; }
        
        public override string Id => "Start";
        
        private readonly Field field = new Field();
        private readonly Drawer drawer = new Drawer();
        
        public StartOptions()
        {
            AddOption("draw", _ => Draw());
            AddOption("move #x1 #y1 #x2 #y2", Move);
            AddOption("undo", _ => Undo());
            Draw();
        }
        
        private void Draw()
        {
            drawer.Draw(field);
        }
        
        private void Move(Parameters parameters)
        {
            var from = field.GetPosition(parameters.Ints[0], parameters.Ints[1]);
            var to = field.GetPosition(parameters.Ints[2], parameters.Ints[3]);
            if (ValidPositions(from, to) && TryGetMovement(from, to, out var rule))
            {
                CommandQueue.Execute(rule.ToCommand(from, to, field));
                Draw();
            }
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
        
        private void Undo()
        {
            CommandQueue.Undo();
            Draw();
        }
    }
}