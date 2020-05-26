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
        private MovementProvider movementProvider = new MovementProvider();
        
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
            movementProvider.GameStatusProvider = statusProvider;
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
        public void NoPawnIsValidTest()
        {
            Assert.False(new NoPawnRule().IsValid());
        }
        
        [Test]
        public void NoPawnCommandTest()
        {
            Assert.Null(new NoPawnRule().ToCommand());
        }

        [Test]
        public void NoPawnReasonTest()
        {
            Assert.That(new NoPawnRule().Reason, Is.EqualTo("No pawn is selected"));
        }
        
        [Test]
        public void OccupiedIsValidTest()
        {
            Assert.False(new OccupiedRule().IsValid());
        }
        
        [Test]
        public void OccupiedCommandTest()
        {
            Assert.Null(new OccupiedRule().ToCommand());
        }

        [Test]
        public void OccupiedReasonTest()
        {
            Assert.That(new OccupiedRule().Reason, Is.EqualTo("Target position is occupied"));
        }
        
        [Test]
        public void OpponentIsValidTest()
        {
            Assert.False(new OpponentMoveRule().IsValid());
        }
        
        [Test]
        public void OpponentCommandTest()
        {
            Assert.Null(new OpponentMoveRule().ToCommand());
        }

        [Test]
        public void OpponentReasonTest()
        {
            Assert.That(new OpponentMoveRule().Reason, Is.EqualTo("Pawns cannot be moved during opponent turn"));
        }
        
        [Test]
        public void SimpleMoveValidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            var rule = new SimpleMovementRule(field.Positions[0, 0], field.Positions[1, 1]);
            Assert.True(rule.IsValid());
        } 
        
        [Test]
        public void SimpleMoveNotValidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            var rule = new SimpleMovementRule(field.Positions[0, 0], field.Positions[0, 1]);
            Assert.False(rule.IsValid());
        }
        
        [Test]
        public void SimpleMoveValidReasonTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            var rule = new SimpleMovementRule(field.Positions[0, 0], field.Positions[1, 1]);
            rule.IsValid();
            Assert.Null(rule.Reason);
        }
        
        [Test]
        public void SimpleMoveNotValidReasonTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            var rule = new SimpleMovementRule(field.Positions[0, 0], field.Positions[0, 1]);
            rule.IsValid();
            Assert.That(rule.Reason, Is.EqualTo("Invalid target position"));
        }

        [Test]
        public void DameMoveValidDiagonalTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
            var rule = new DameMovementRule(field.Positions[0, 0], field.Positions[2, 2], field);
            Assert.True(rule.IsValid());
        }
        
        [Test]
        public void DameMoveValidStraightTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
            var rule = new DameMovementRule(field.Positions[0, 0], field.Positions[0, 2], field);
            Assert.True(rule.IsValid());
        }
        
        [Test]
        public void DameMoveInvalidDirectionTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
            var rule = new DameMovementRule(field.Positions[0, 0], field.Positions[1, 2], field);
            Assert.False(rule.IsValid());
        }
        
        [Test]
        public void DameMoveInvalidPawnOnLineTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
            field.Positions[1, 1].Pawn = new Pawn(Color.Black);
            var rule = new DameMovementRule(field.Positions[0, 0], field.Positions[2, 2], field);
            Assert.False(rule.IsValid());
        }
        
        [Test]
        public void DameMoveValidReasonTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
            var rule = new DameMovementRule(field.Positions[0, 0], field.Positions[0, 2], field);
            rule.IsValid();
            Assert.Null(rule.Reason);
        }
        
        [Test]
        public void DameMoveInvalidReasonDirectionTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
            var rule = new DameMovementRule(field.Positions[0, 0], field.Positions[1, 2], field);
            rule.IsValid();
            Assert.That(rule.Reason, Is.EqualTo("Invalid movement direction"));
        }
        
        [Test]
        public void DameMoveInvalidReasonPawnsTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
            field.Positions[1, 1].Pawn = new Pawn(Color.Black) {IsDame = true};
            var rule = new DameMovementRule(field.Positions[0, 0], field.Positions[2, 2], field);
            rule.IsValid();
            Assert.That(rule.Reason, Is.EqualTo("Pawns are in the way"));
        }
        
        [Test]
        public void SimpleAttackValidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            field.Positions[1, 1].Pawn = new Pawn(Color.White);
            var rule = new FightRule(field.Positions[0, 0], field.Positions[2, 2], field);
            Assert.True(rule.IsValid());
        }
        
        [Test]
        public void SimpleAttackNotValidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            field.Positions[1, 1].Pawn = new Pawn(Color.White);
            var rule = new FightRule(field.Positions[0, 0], field.Positions[0, 2], field);
            Assert.False(rule.IsValid());
        }
        
        [Test]
        public void SimpleAttackValidReasonTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            field.Positions[1, 1].Pawn = new Pawn(Color.White);
            var rule = new FightRule(field.Positions[0, 0], field.Positions[2, 2], field);
            rule.IsValid();
            Assert.Null(rule.Reason);
        }
        
        [Test]
        public void SimpleAttackNotValidReasonTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            field.Positions[1, 1].Pawn = new Pawn(Color.White);
            var rule = new FightRule(field.Positions[0, 0], field.Positions[0, 2], field);
            rule.IsValid();
            Assert.That(rule.Reason, Is.EqualTo("No opponent pawn in the way"));
        }
        
        [Test]
        public void DameAttackValidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
            field.Positions[0, 1].Pawn = new Pawn(Color.White);
            var rule = new FightRule(field.Positions[0, 0], field.Positions[0, 2], field);
            Assert.True(rule.IsValid());
        }
        
        [Test]
        public void DameAttackNotValidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
            field.Positions[0, 1].Pawn = new Pawn(Color.White);
            var rule = new FightRule(field.Positions[0, 0], field.Positions[1, 2], field);
            Assert.False(rule.IsValid());
        }
        
        [Test]
        public void ToMoveTest()
        {
            var rule = new SimpleMovementRule(field.Positions[0, 0], field.Positions[1, 1]);
            Assert.That(rule.ToCommand().GetType(), Is.EqualTo(typeof(MoveCommand)));
        }
        
        [Test]
        public void ToMoveDameTest()
        {
            var rule = new DameMovementRule(field.Positions[0, 0], field.Positions[1, 1], field);
            Assert.That(rule.ToCommand().GetType(), Is.EqualTo(typeof(MoveCommand)));
        }
        
        [Test]
        public void ToFightTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            field.Positions[1, 1].Pawn = new Pawn(Color.White);
            var rule = new FightRule(field.Positions[0, 0], field.Positions[2, 2], field);
            Assert.That(rule.ToCommand().GetType(), Is.EqualTo(typeof(FightCommand)));
        }

        [Test]
        public void NoPawnProviderTest()
        {
            var rule = movementProvider.RuleFor(field.Positions[0, 0], field.Positions[1, 1], field);
            Assert.That(rule.GetType(), Is.EqualTo(typeof(NoPawnRule)));
        }
        
        [Test]
        public void OpponentProviderTest()
        {
            statusProvider.UpdateStatus(Status.WhiteMove);
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            var rule = movementProvider.RuleFor(field.Positions[0, 0], field.Positions[1, 1], field);
            Assert.That(rule.GetType(), Is.EqualTo(typeof(OpponentMoveRule)));
        }
        
        [Test]
        public void OccupiedProviderTest()
        {
            statusProvider.UpdateStatus(Status.BlackMove);
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            field.Positions[1, 1].Pawn = new Pawn(Color.Black);
            var rule = movementProvider.RuleFor(field.Positions[0, 0], field.Positions[1, 1], field);
            Assert.That(rule.GetType(), Is.EqualTo(typeof(OccupiedRule)));
        }
        
        [Test]
        public void MoveRuleProviderTest()
        {
            statusProvider.UpdateStatus(Status.BlackMove);
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            var rule = movementProvider.RuleFor(field.Positions[0, 0], field.Positions[1, 1], field);
            Assert.That(rule.GetType(), Is.EqualTo(typeof(SimpleMovementRule)));
        }
        
        [Test]
        public void DameMoveRuleProviderTest()
        {
            statusProvider.UpdateStatus(Status.BlackMove);
            field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
            var rule = movementProvider.RuleFor(field.Positions[0, 0], field.Positions[0, 2], field);
            Assert.That(rule.GetType(), Is.EqualTo(typeof(DameMovementRule)));
        }
        
        [Test]
        public void FightRuleProviderTest()
        {
            statusProvider.UpdateStatus(Status.BlackAttack);
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            field.Positions[1, 1].Pawn = new Pawn(Color.White);
            var rule = movementProvider.RuleFor(field.Positions[0, 0], field.Positions[2, 2], field);
            Assert.That(rule.GetType(), Is.EqualTo(typeof(FightRule)));
        }
        
        [Test]
        public void DameFightRuleProviderTest()
        {
            statusProvider.UpdateStatus(Status.BlackAttack);
            field.Positions[0, 0].Pawn = new Pawn(Color.Black) {IsDame = true};
            field.Positions[0, 1].Pawn = new Pawn(Color.White);
            var rule = movementProvider.RuleFor(field.Positions[0, 0], field.Positions[0, 2], field);
            Assert.That(rule.GetType(), Is.EqualTo(typeof(FightRule)));
        }
    }
}