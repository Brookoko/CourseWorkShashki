namespace Checkers.Tests
{
    using AppSetup;
    using GameField;
    using NUnit.Framework;

    [TestFixture]
    public class PositionConverterTest
    {
        private readonly StringPositionConverter converter = new StringPositionConverter();
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
        public void ConvertToPositionTest()
        {
            var (from, to) = converter.ToPositions("a8 b7", field);
            Assert.That(from, Is.EqualTo(field.Positions[0, 0]));
            Assert.That(to, Is.EqualTo(field.Positions[1, 1]));
        }
        
        [Test]
        public void ConvertToPositionUpperCaseTest()
        {
            var (from, to) = converter.ToPositions("A8 B7", field);
            Assert.That(from, Is.EqualTo(field.Positions[0, 0]));
            Assert.That(to, Is.EqualTo(field.Positions[1, 1]));
        }
        
        [Test]
        public void ConvertToPositionOnlyOneTest()
        {
            var (from, to) = converter.ToPositions("a8", field);
            Assert.Null(from);
            Assert.Null(to);
        }
        
        [Test]
        public void ConvertToPositionInvalidNumberTest()
        {
            var (from, to) = converter.ToPositions("a8 b9", field);
            Assert.That(from, Is.EqualTo(field.Positions[0, 0]));
            Assert.Null(to);
        }
        
        [Test]
        public void ConvertToPositionNotNumberTest()
        {
            var (from, to) = converter.ToPositions("a8 ba", field);
            Assert.That(from, Is.EqualTo(field.Positions[0, 0]));
            Assert.Null(to);
        }
        
        [Test]
        public void ConvertToPositionInvalidLetterTest()
        {
            var (from, to) = converter.ToPositions("a8 q7", field);
            Assert.That(from, Is.EqualTo(field.Positions[0, 0]));
            Assert.Null(to);
        }
        
        [Test]
        public void ConvertToPositionInvalidFormatTest()
        {
            var (from, to) = converter.ToPositions("a8 b70", field);
            Assert.That(from, Is.EqualTo(field.Positions[0, 0]));
            Assert.Null(to);
        }
        
        [Test]
        public void ConvertToPositionEmptyStringTest()
        {
            var (from, to) = converter.ToPositions("", field);
            Assert.Null(from);
            Assert.Null(to);
        }
    }
}