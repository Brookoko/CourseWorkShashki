namespace AppSetup
{
    using System;
    using ConsoleApp;
    using GameField;
    using Movements;
    
    public class StartOptions : Options
    {
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
        
        private async void Move(Parameters parameters)
        {
            var x1 = parameters.Ints[0];
            var y1 = parameters.Ints[1];
            var x2 = parameters.Ints[2];
            var y2 = parameters.Ints[3];
            var from = field.Positions[x1, y1];
            var to = field.Positions[x2, y2];
            var movement = new Movement(from, to);
            if (!movement.IsValid())
            {
                Console.WriteLine("Invalid move");
                return;
            }
            var command = movement.ToCommand();
            command.Execute();
            Draw();
        }
    }
}