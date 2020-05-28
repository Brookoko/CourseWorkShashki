namespace Checkers.Tests
{
    using GameField;
    using GameStatus;
    using NUnit.Framework;
    
    [TestFixture]
    public class UtilsTest
    {
        [Test]
        [TestCase(Status.WhiteWin)]
        [TestCase(Status.BlackWin)]
        public void WinStatusTest(Status status)
        {
            Assert.True(status.IsWinStatus());
        }
        
        [Test]
        [TestCase(Status.Menu)]
        [TestCase(Status.WhiteAttack)]
        [TestCase(Status.WhiteMove)]
        [TestCase(Status.BlackAttack)]
        [TestCase(Status.BlackMove)]
        public void NotWinStatusTest(Status status)
        {
            Assert.False(status.IsWinStatus());
        }
        
        [Test]
        [TestCase(Status.WhiteAttack)]
        [TestCase(Status.BlackAttack)]
        public void AttackStatusTest(Status status)
        {
            Assert.True(status.IsAttacking());
        }
        
        [Test]
        [TestCase(Status.Menu)]
        [TestCase(Status.WhiteWin)]
        [TestCase(Status.WhiteMove)]
        [TestCase(Status.BlackWin)]
        [TestCase(Status.BlackMove)]
        public void NotAttackStatusTest(Status status)
        {
            Assert.False(status.IsAttacking());
        }
        
        [Test]
        [TestCase(Status.WhiteMove)]
        [TestCase(Status.BlackMove)]
        public void MoveStatusTest(Status status)
        {
            Assert.True(status.IsMoving());
        }
        
        [Test]
        [TestCase(Status.Menu)]
        [TestCase(Status.WhiteWin)]
        [TestCase(Status.WhiteAttack)]
        [TestCase(Status.BlackWin)]
        [TestCase(Status.BlackAttack)]
        public void NotMoveStatusTest(Status status)
        {
            Assert.False(status.IsMoving());
        }

        [Test]
        [TestCase(Status.Menu)]
        [TestCase(Status.WhiteWin)]
        [TestCase(Status.WhiteAttack)]
        [TestCase(Status.WhiteMove)]
        public void ToColorWhiteTest(Status status)
        {
            Assert.That(Color.White, Is.EqualTo(status.ToColor()));
        }
        
        [Test]
        [TestCase(Status.BlackWin)]
        [TestCase(Status.BlackAttack)]
        [TestCase(Status.BlackMove)]
        public void ToColorBlackTest(Status status)
        {
            Assert.That(Color.Black, Is.EqualTo(status.ToColor()));
        }
        
        [Test]
        public void OpposeTest()
        {
            Assert.That(Color.Black, Is.EqualTo(Color.White.Oppose()));
            Assert.That(Color.White, Is.EqualTo(Color.Black.Oppose()));
        }
        
        [Test]
        [TestCase(Color.White, Status.WhiteWin)]
        [TestCase(Color.White, Status.WhiteAttack)]
        [TestCase(Color.White, Status.WhiteMove)]
        [TestCase(Color.Black, Status.BlackWin)]
        [TestCase(Color.Black, Status.BlackAttack)]
        [TestCase(Color.Black, Status.BlackMove)]
        public void CanMoveTest(Color color, Status status)
        {
            var pawn = new Pawn(color);
            Assert.True(pawn.CanMove(status));
        }
        
        [Test]
        [TestCase(Color.Black, Status.WhiteWin)]
        [TestCase(Color.Black, Status.WhiteAttack)]
        [TestCase(Color.Black, Status.WhiteMove)]
        [TestCase(Color.White, Status.BlackWin)]
        [TestCase(Color.White, Status.BlackAttack)]
        [TestCase(Color.White, Status.BlackMove)]
        public void CannotMoveTest(Color color, Status status)
        {
            var pawn = new Pawn(color);
            Assert.False(pawn.CanMove(status));
        }
        
        [Test]
        public void ToDirectionTest()
        {
            Assert.That(1, Is.EqualTo(Color.Black.ToDirection()));
            Assert.That(-1, Is.EqualTo(Color.White.ToDirection()));
        }
        
        [Test]
        public void DiagonalTest()
        {
            var from = new Position(0, 0);
            var to = new Position(1, 1);
            Assert.True(Utils.IsDiagonal(from, to));
        }
        
        [Test]
        public void NotDiagonalTest()
        {
            var from = new Position(0, 0);
            var to = new Position(1, 2);
            Assert.False(Utils.IsDiagonal(from, to));
        }
        
        [Test]
        public void DiagonalInDirectionTest()
        {
            var from = new Position(0, 0);
            var to = new Position(1, 1);
            Assert.True(Utils.IsDiagonalInDirection(from, to, 1));
        }
        
        [Test]
        public void NotDiagonalInDirectionTest()
        {
            var from = new Position(0, 0);
            var to = new Position(1, 1);
            Assert.False(Utils.IsDiagonalInDirection(from, to, -1));
        }
    }
}