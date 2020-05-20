namespace AppSetup
{
    using ConsoleApp;
    using GameField;
    
    public class StartOptions : Options
    {
        public override string Id => "Start";
        
        public StartOptions()
        {
            AddOption("--draw", _ => Draw());
        }
        
        private void Draw()
        {
            var field = new Field();
            var drawer = new Drawer();
            drawer.Draw(field);
        }
    }
}