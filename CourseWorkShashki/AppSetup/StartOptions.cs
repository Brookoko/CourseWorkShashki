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
            AddOption("--draw", _ => Draw());
            AddOption("--move #x1 #y1 #x2 #y2", Move);
        }
        
        private void Draw()
        {
            drawer.Draw(field);
        }
        
        private void Move(Parameters parameters)
        {
            var x1 = parameters.Ints[0];
            var y1 = parameters.Ints[1];
            var x2 = parameters.Ints[2];
            var y2 = parameters.Ints[3];
            var from = field.Positions[x1, y1];
            var to = field.Positions[x2, y2];
            var rejections = new List<string>();
            var movement = MovementProvider.RuleFor(from, to, field, rejections);
            if (movement == null)
            {
                foreach (var reason in rejections)
                {
                    Console.WriteLine(reason);
                }
                return;
            }
            CommandQueue.Execute(movement.ToCommand(from, to, field));
            Draw();
        }
    }
}