namespace Checkers.Tests
{
    using GameField;
    using GameStatus;
    using NUnit.Framework;

    [TestFixture]
    public class GameStatusTest
    {
        private readonly Field field = new Field();
        private readonly FieldProvider provider = new FieldProvider();
        private readonly GameStatusProvider statusProvider = new GameStatusProvider();
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    field.Positions[i, j] = new Position(i, j);
                }
            }
            provider.Field = field;
            statusProvider.FieldProvider = provider;
        }
        
        [TearDown]
        public void TearDown()
        {
            foreach (var position in field.Positions)
            {
                position.Pawn = null;
            }
        }
        
        [Test]
        public void UpdateStatusTest()
        {
            statusProvider.UpdateStatus(Status.WhiteMove);
            Assert.That(statusProvider.Status, Is.EqualTo(Status.WhiteMove));
        }
        
        [Test]
        public void GoToWinStatusTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.White);
            statusProvider.UpdateStatus(Status.WhiteMove);
            statusProvider.GoToNext();
            Assert.That(statusProvider.Status, Is.EqualTo(Status.WhiteWin));
        }
        
        [Test]
        public void GoToAttackStatusTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.White);
            field.Positions[1, 1].Pawn = new Pawn(Color.Black);
            statusProvider.UpdateStatus(Status.BlackMove);
            statusProvider.GoToNext();
            Assert.That(statusProvider.Status, Is.EqualTo(Status.WhiteAttack));
        }
        
        [Test]
        public void GoToMoveStatusTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            statusProvider.UpdateStatus(Status.WhiteMove);
            statusProvider.GoToNext();
            Assert.That(statusProvider.Status, Is.EqualTo(Status.BlackMove));
        }
    }
}