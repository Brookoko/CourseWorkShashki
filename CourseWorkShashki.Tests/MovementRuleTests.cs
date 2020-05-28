namespace Checkers.Tests
{
    using Commands;
    using GameField;
    using GameStatus;
    using Movements;
    using NUnit.Framework;

    [TestFixture]
    public class MovementRuleTests
    {
        private readonly Field field = new Field();
        private readonly FieldProvider fieldProvider = new FieldProvider();
        private readonly GameStatusProvider statusProvider = new GameStatusProvider();
        private readonly MovementProvider movementProvider = new MovementProvider();
        
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
            fieldProvider.Field = field;
            statusProvider.FieldProvider = fieldProvider;
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
        public void NoPawnValidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            Assert.True(new NoPawnRule().IsValid(move));
        }
        
        [Test]
        public void NoPawnInvalidTest()
        {
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            Assert.False(new NoPawnRule().IsValid(move));
        }
        
        [Test]
        public void NoPawnReasonTest()
        {
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            var rule = new NoPawnRule();
            rule.IsValid(move);
            Assert.That(move.RejectionReason, Is.EqualTo("No pawn is selected"));
        }
        
        // [Test]
        // public void OccupiedIsValidTest()
        // {
        //     Assert.False(new OccupiedRule().IsValid());
        // }
        //
        // [Test]
        // public void OccupiedReasonTest()
        // {
        //     Assert.That(new OccupiedRule().Reason, Is.EqualTo("Target position is occupied"));
        // }
        //
        // [Test]
        // public void OpponentIsValidTest()
        // {
        //     Assert.False(new OpponentMoveRule().IsValid());
        // }
        //
        // [Test]
        // public void OpponentReasonTest()
        // {
        //     Assert.That(new OpponentMoveRule().Reason, Is.EqualTo("Pawns cannot be moved during opponent turn"));
        // }
        //
        // [Test]
        // public void SimpleMoveValidTest()
        // {
        //     field.Positions[0, 0].Pawn = new Pawn(Color.Black);
        //     var rule = new SimpleMovementRule(field.Positions[0, 0], field.Positions[1, 1]);
        //     Assert.True(rule.IsValid());
        // } 
        //
        // [Test]
        // public void SimpleMoveNotValidTest()
        // {
        //     field.Positions[0, 0].Pawn = new Pawn(Color.Black);
        //     var rule = new SimpleMovementRule(field.Positions[0, 0], field.Positions[0, 1]);
        //     Assert.False(rule.IsValid());
        // }
        //
        // [Test]
        // public void SimpleMoveNotValidReasonTest()
        // {
        //     field.Positions[0, 0].Pawn = new Pawn(Color.Black);
        //     var rule = new SimpleMovementRule(field.Positions[0, 0], field.Positions[0, 1]);
        //     rule.IsValid();
        //     Assert.That(rule.Reason, Is.EqualTo("Invalid target position"));
        // }
        //
        // [Test]
        // public void DameMoveValidDiagonalTest()
        // {
        //     field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
        //     var rule = new DameMovementRule(field.Positions[0, 0], field.Positions[2, 2], field);
        //     Assert.True(rule.IsValid());
        // }
        //
        // [Test]
        // public void DameMoveValidStraightTest()
        // {
        //     field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
        //     var rule = new DameMovementRule(field.Positions[0, 0], field.Positions[0, 2], field);
        //     Assert.True(rule.IsValid());
        // }
        //
        // [Test]
        // public void DameMoveInvalidDirectionTest()
        // {
        //     field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
        //     var rule = new DameMovementRule(field.Positions[0, 0], field.Positions[1, 2], field);
        //     Assert.False(rule.IsValid());
        // }
        //
        // [Test]
        // public void DameMoveInvalidPawnOnLineTest()
        // {
        //     field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
        //     field.Positions[1, 1].Pawn = new Pawn(Color.Black);
        //     var rule = new DameMovementRule(field.Positions[0, 0], field.Positions[2, 2], field);
        //     Assert.False(rule.IsValid());
        // }
        //
        // [Test]
        // public void DameMoveInvalidReasonDirectionTest()
        // {
        //     field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
        //     var rule = new DameMovementRule(field.Positions[0, 0], field.Positions[1, 2], field);
        //     rule.IsValid();
        //     Assert.That(rule.Reason, Is.EqualTo("Invalid movement direction"));
        // }
        //
        // [Test]
        // public void DameMoveInvalidReasonPawnsTest()
        // {
        //     field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
        //     field.Positions[1, 1].Pawn = new Pawn(Color.Black) {IsDame = true};
        //     var rule = new DameMovementRule(field.Positions[0, 0], field.Positions[2, 2], field);
        //     rule.IsValid();
        //     Assert.That(rule.Reason, Is.EqualTo("Pawns are in the way"));
        // }
        //
        // [Test]
        // public void SimpleAttackValidTest()
        // {
        //     field.Positions[0, 0].Pawn = new Pawn(Color.Black);
        //     field.Positions[1, 1].Pawn = new Pawn(Color.White);
        //     var rule = new FightRule(field.Positions[0, 0], field.Positions[2, 2], field);
        //     Assert.True(rule.IsValid());
        // }
        //
        // [Test]
        // public void SimpleAttackNotValidTest()
        // {
        //     field.Positions[0, 0].Pawn = new Pawn(Color.Black);
        //     field.Positions[1, 1].Pawn = new Pawn(Color.White);
        //     var rule = new FightRule(field.Positions[0, 0], field.Positions[0, 2], field);
        //     Assert.False(rule.IsValid());
        // }
        //
        // [Test]
        // public void SimpleAttackNotValidReasonTest()
        // {
        //     field.Positions[0, 0].Pawn = new Pawn(Color.Black);
        //     field.Positions[1, 1].Pawn = new Pawn(Color.White);
        //     var rule = new FightRule(field.Positions[0, 0], field.Positions[0, 2], field);
        //     rule.IsValid();
        //     Assert.That(rule.Reason, Is.EqualTo("No opponent pawn in the way"));
        // }
        //
        // [Test]
        // public void DameAttackValidTest()
        // {
        //     field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
        //     field.Positions[0, 1].Pawn = new Pawn(Color.White);
        //     var rule = new FightRule(field.Positions[0, 0], field.Positions[0, 2], field);
        //     Assert.True(rule.IsValid());
        // }
        //
        // [Test]
        // public void DameAttackNotValidTest()
        // {
        //     field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
        //     field.Positions[0, 1].Pawn = new Pawn(Color.White);
        //     var rule = new FightRule(field.Positions[0, 0], field.Positions[1, 2], field);
        //     Assert.False(rule.IsValid());
        // }
        
        private Move CreateMove(Position from, Position to)
        {
            return new Move
            {
                From = from,
                To = to,
                Field = field,
                Status = statusProvider.Status
            };
        }
    }
}