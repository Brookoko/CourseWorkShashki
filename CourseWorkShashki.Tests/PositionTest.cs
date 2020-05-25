namespace Checkers.Tests
{
    using GameField;
    using NUnit.Framework;
    
    [TestFixture]
    public class PositionTest
    {
        [Test]
        public void PositionEqualTest()
        {
            var first = new Position(0, 0);
            var second = new Position(0, 0);
            Assert.True(first.Equals(second));
        }

        [Test]
        public void PositionNotEqualTest()
        {
            var first = new Position(0, 0);
            var second = new Position(0, 1);
            var third = new Position(1, 0);
            Assert.False(first.Equals(second));
            Assert.False(first.Equals(third));
        }
        
        [Test]
        public void PositionNullTest()
        {
            var position = new Position(0, 0);
            Assert.False(position.Equals(null));
        }
        
        [Test]
        public void PositionEqualSameTest()
        {
            var position = new Position(0, 0);
            Assert.True(position.Equals(position));
        }
        
        [Test]
        public void PositionAnotherObjectTest()
        {
            var position = new Position(0, 0);
            Assert.False(position.Equals(new object()));
        }
        
        [Test]
        public void PositionEqualityTest()
        {
            var first = new Position(0, 0);
            var second = new Position(0, 0);
            Assert.True(first == second);
        }
        
        [Test]
        public void PositionWrongEqualityTest()
        {
            var first = new Position(0, 0);
            var second = new Position(0, 1);
            var third = new Position(1, 0);
            Assert.False(first == second);
            Assert.False(first == third);
        }
        
        [Test]
        public void PositionEqualityNullTest()
        {
            var position = new Position(0, 0);
            Assert.False(position == null);
        }
        
        [Test]
        public void PositionNotEqualityTest()
        {
            var first = new Position(0, 0);
            var second = new Position(0, 1);
            var third = new Position(1, 0);
            Assert.True(first != second);
            Assert.True(first != third);

        }
        
        [Test]
        public void PositionWrongNotEqualityTest()
        {
            var first = new Position(0, 0);
            var second = new Position(0, 0);
            Assert.False(first != second);
        }
        
        [Test]
        public void PositionNotEqualityNullTest()
        {
            var position = new Position(0, 0);
            Assert.True(position != null);
        }
        
        [Test]
        public void PositionToStringTest()
        {
            var position = new Position(0, 0);
            Assert.That(position.ToString(), Is.EqualTo("0 0"));
        }
        
        [Test]
        public void PositionHashOneTest()
        {
            var position = new Position(0, 0);
            Assert.That(position.GetHashCode(), Is.EqualTo(0));
        }
        
        [Test]
        public void PositionHashTwoTest()
        {
            var position = new Position(1, 1);
            Assert.That(position.GetHashCode(), Is.EqualTo(396));
        }
        
        [Test]
        public void PositionRemovePawnTest()
        {
            var pawn = new Pawn(Color.White);
            var position = new Position(0, 0) {Pawn = pawn};
            position.RemovePawn();
            Assert.That(position.Pawn, Is.Not.EqualTo(pawn));
        }
        
        [Test]
        public void PositionTurnDameTest()
        {
            var white = new Position(0, 0) {Pawn = new Pawn(Color.White)};
            var black = new Position(7, 7) {Pawn = new Pawn(Color.Black)};
            Assert.True(white.TryTurnToDame());
            Assert.True(black.TryTurnToDame());
        }
        
        [Test]
        public void PositionTurnDameDamePawnTest()
        {
            var position = new Position(0, 0) {Pawn = new Pawn(Color.White) {IsDame = true}};
            Assert.False(position.TryTurnToDame());
        }
        
        [Test]
        public void PositionTurnDameWrongPositionTest()
        {
            var white = new Position(1, 0) {Pawn = new Pawn(Color.White)};
            var black = new Position(6, 7) {Pawn = new Pawn(Color.White)};
            Assert.False(white.TryTurnToDame());
            Assert.False(black.TryTurnToDame());
        }
    }
}