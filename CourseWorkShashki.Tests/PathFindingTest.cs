namespace Checkers.Tests
{
    using System.Collections.Generic;
    using GameField;
    using NUnit.Framework;
    using PathFinding;

    [TestFixture]
    public class PathFindingTest
    {
        private readonly Field field = new Field();
        
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
        public void PathLengthTest()
        {
            var path = new Path(new Position(0 , 0));
            path.Positions.Add(new Position(0, 0));
            path.Positions.Add(new Position(0, 0));
            Assert.That(path.Length, Is.EqualTo(3));
        }
        
        [Test]
        public void PathHasTest()
        {
            var path = new Path(new Position(0 , 0));
            Assert.True(path.Has(new Position(0, 0)));
        }
        
        [Test]
        public void PathFirstTest()
        {
            var position = new Position(0, 0);
            var path = new Path(position);
            Assert.That(position, Is.EqualTo(path.First()));
        }
        
        [Test]
        public void BranchValidTest()
        {
            var path = new Path(new Position(0 , 0));
            var opponent = new Position(1, 1) {Pawn = new Pawn(Color.White)};
            var branch = new Branch(path, new Position(2, 2), new List<Position> {opponent});
            Assert.True(branch.IsValid(new Pawn(Color.Black)));
        }
        
        [Test]
        public void BranchNewPathTest()
        {
            var path = new Path(new Position(0 , 0));
            var target = new Position(2, 2);
            var opponent = new Position(1, 1) {Pawn = new Pawn(Color.White)};
            var branch = new Branch(path, target, new List<Position> {opponent});
            var newPath = branch.CreatePath();
            Assert.That(newPath.Length, Is.EqualTo(2));
            Assert.That(newPath.Positions, Has.Member(target));
            Assert.That(newPath.Opponents, Has.Member(opponent));
        }
        
        [Test]
        public void SimplePathTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            field.Positions[1, 1].Pawn = new Pawn(Color.White);
            var path = new BFS(field.Positions[0, 0], field.Positions[2, 2], field).FindPath();
            Assert.That(path.Opponents, Has.Member(field.Positions[1, 1]));
            Assert.That(path.Positions, Has.Member(field.Positions[2, 2]));
        }
        
        [Test]
        public void ComplexPathTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            field.Positions[1, 1].Pawn = new Pawn(Color.White);
            field.Positions[3, 3].Pawn = new Pawn(Color.White);
            field.Positions[3, 5].Pawn = new Pawn(Color.White);
            var path = new BFS(field.Positions[0, 0], field.Positions[2, 6], field).FindPath();
            Assert.That(path.Opponents, Has.Member(field.Positions[1, 1]));
            Assert.That(path.Opponents, Has.Member(field.Positions[3, 3]));
            Assert.That(path.Opponents, Has.Member(field.Positions[3, 5]));
            Assert.That(path.Positions, Has.Member(field.Positions[2, 2]));
            Assert.That(path.Positions, Has.Member(field.Positions[4, 4]));
            Assert.That(path.Positions, Has.Member(field.Positions[2, 6]));
        }
        
        [Test]
        public void DamePathTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
            field.Positions[0, 1].Pawn = new Pawn(Color.White);
            field.Positions[6, 7].Pawn = new Pawn(Color.White);
            var path = new BFS(field.Positions[0, 0], field.Positions[7, 7], field).FindPath();
            Assert.That(path.Opponents, Has.Member(field.Positions[0, 1]));
            Assert.That(path.Opponents, Has.Member(field.Positions[6, 7]));
            Assert.That(path.Positions, Has.Member(field.Positions[0, 7]));
            Assert.That(path.Positions, Has.Member(field.Positions[7, 7]));
        }
    }
}