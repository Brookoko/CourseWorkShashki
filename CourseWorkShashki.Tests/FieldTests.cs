namespace Checkers.Tests
{
    using System.Linq;
    using GameField;
    using NUnit.Framework;

    [TestFixture]
    public class FieldTests
    {
        private readonly Position[,] positions = new Position[8, 8];
        private Field field;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            field = new Field();
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    positions[i,j] = new Position(i, j);
                    field.Positions[i, j] = positions[i, j];
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var position in positions)
            {
                position.Pawn = null;
            }
        }
        
        [Test]
        public void PositionInFieldTest()
        {
            Assert.NotNull(field.GetPosition(0, 0));
            Assert.NotNull(field.GetPosition(7, 7));
        }
        
        [Test]
        public void PositionOutOfFieldTest()
        {
            Assert.Null(field.GetPosition(0, -1));
            Assert.Null(field.GetPosition(8, 8));
        }
        
        [Test]
        public void PawnsOnStraightLineHorizontalTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.White);
            field.Positions[0, 3].Pawn = new Pawn(Color.White);
            var pawnsOnLine = field.PawnsOnLine(positions[0, 0], positions[0, 7]);
            Assert.That(1, Is.EqualTo(pawnsOnLine.Count));
        }
        
        [Test]
        public void PawnsOnStraightLineVerticalTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.White);
            field.Positions[3, 0].Pawn = new Pawn(Color.White);
            var pawnsOnLine = field.PawnsOnLine(positions[0, 0], positions[7, 0]);
            Assert.That(pawnsOnLine, Has.Count.EqualTo(1));
        }
        
        [Test]
        public void PawnsOnDiagonalTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.White);
            field.Positions[3, 3].Pawn = new Pawn(Color.White);
            var pawnsOnLine = field.PawnsOnLine(positions[0, 0], positions[7, 7]);
            Assert.That(pawnsOnLine, Has.Count.EqualTo(1));
        }
        
        [Test]
        public void MultiplePawnsOnDiagonalTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.White);
            field.Positions[1, 1].Pawn = new Pawn(Color.White);
            field.Positions[2, 2].Pawn = new Pawn(Color.White);
            field.Positions[3, 3].Pawn = new Pawn(Color.White);
            field.Positions[4, 4].Pawn = new Pawn(Color.White);
            field.Positions[5, 5].Pawn = new Pawn(Color.White);
            field.Positions[6, 6].Pawn = new Pawn(Color.White);
            field.Positions[7, 7].Pawn = new Pawn(Color.White);
            var pawnsOnLine = field.PawnsOnLine(positions[0, 0], positions[7, 7]);
            Assert.That(pawnsOnLine, Has.Count.EqualTo(6));
        }

        [Test]
        public void WhiteWinTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.White);
            Assert.IsTrue(field.IsInWinState(Color.White));
        }
        
        [Test]
        public void WhiteNotWinTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.White);
            field.Positions[1, 1].Pawn = new Pawn(Color.Black);
            Assert.IsFalse(field.IsInWinState(Color.White));
        }
        
        [Test]
        public void WinEmptyTest()
        {
            Assert.IsTrue(field.IsInWinState(Color.White));
        }
        
        [Test]
        public void PossiblePositionsPawnTest()
        {
            var attacks = field.PossibleAttackPositions(positions[2, 2], false).ToList();
            Assert.That(attacks, Has.Count.EqualTo(4));
            Assert.That(attacks, Has.Member(positions[0, 0]));
            Assert.That(attacks, Has.Member(positions[0, 4]));
            Assert.That(attacks, Has.Member(positions[4, 0]));
            Assert.That(attacks, Has.Member(positions[4, 4]));
        }
        
        [Test]
        public void PossiblePositionsNotAllPawnTest()
        {
            var attacks = field.PossibleAttackPositions(positions[0, 0], false).ToList();
            Assert.That(attacks, Has.Count.EqualTo(1));
            Assert.That(attacks, Has.Member(positions[2, 2]));
        }
        
        [Test]
        public void PossiblePositionsDameTest()
        {
            var attacks = field.PossibleAttackPositions(positions[0, 0], true).ToList();
            Assert.That(attacks, Has.Count.EqualTo(21));
            for (var i = 1; i < 8; i++)
            {
                Assert.That(attacks, Has.Member(positions[0, i]));
                Assert.That(attacks, Has.Member(positions[i, 0]));
                Assert.That(attacks, Has.Member(positions[i, i]));
            }
        }

        [Test]
        public void AttackStateNoPawnTest()
        {
            Assert.False(field.IsInAttackingState(positions[0, 0]));
        }
        
        [Test]
        public void AttackStateTest()
        {
            positions[0, 0].Pawn = new Pawn(Color.White);
            positions[1, 1].Pawn = new Pawn(Color.Black);
            Assert.True(field.IsInAttackingState(positions[0, 0]));
        }

        
        [Test]
        public void AttackStateTargetOccupiedTest()
        {
            positions[0, 0].Pawn = new Pawn(Color.White);
            positions[1, 1].Pawn = new Pawn(Color.Black);
            positions[2, 2].Pawn = new Pawn(Color.Black);
            Assert.False(field.IsInAttackingState(positions[0, 0]));
        }
        
        [Test]
        public void AttackStateAllyTest()
        {
            positions[0, 0].Pawn = new Pawn(Color.White);
            positions[1, 1].Pawn = new Pawn(Color.White);
            Assert.False(field.IsInAttackingState(positions[0, 0]));
        }
        
        [Test]
        public void AttackStateMultipleEnemiesTest()
        {
            positions[0, 0].Pawn = new Pawn(Color.White) {IsDame = true};
            positions[0, 1].Pawn = new Pawn(Color.Black);
            positions[0, 2].Pawn = new Pawn(Color.Black);
            Assert.False(field.IsInAttackingState(positions[0, 0]));
        }

        [Test]
        public void AttackStateNoPawnsTest()
        {
            Assert.False(field.IsInAttackingState(Color.White));
        }
        
        [Test]
        public void AttackStateOnlyOneColorTest()
        {
            positions[0, 0].Pawn = new Pawn(Color.White);
            positions[1, 1].Pawn = new Pawn(Color.White);
            positions[7, 7].Pawn = new Pawn(Color.White);
            positions[6, 6].Pawn = new Pawn(Color.White);
            Assert.False(field.IsInAttackingState(Color.White));
        }
        
        [Test]
        public void AttackStateColorTest()
        {
            positions[0, 0].Pawn = new Pawn(Color.White);
            positions[1, 1].Pawn = new Pawn(Color.Black);
            Assert.True(field.IsInAttackingState(Color.White));
        }
        
        [Test]
        public void InitialStateTest()
        {
            var blackPositions = new[]
            {
                (0, 0), (0, 2), (0, 4), (0, 6),
                (1, 1), (1, 3), (1, 5), (1, 7),
                (2, 0), (2, 2), (2, 4), (2, 6)
            };
            var whitePositions = new[]
            {
                (7, 7), (7, 5), (7, 3), (7, 1),
                (6, 6), (6, 4), (6, 2), (6, 0),
                (5, 7), (5, 5), (5, 3), (5, 1)
            };
            var provider = new FieldProvider();
            provider.CreateNew();
            foreach (var (x, y) in blackPositions)
            {
                Assert.That(provider.Field.Positions[x, y].Pawn.Color, Is.EqualTo(Color.Black));
            }
            foreach (var (x, y) in whitePositions)
            {
                Assert.That(provider.Field.Positions[x, y].Pawn.Color, Is.EqualTo(Color.White));
            }
        }
    }
}