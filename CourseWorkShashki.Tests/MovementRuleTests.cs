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
            movementProvider.Prepare();
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
            var rule = new NoPawnRule {Successor = new ValidRule()};
            Assert.True(rule.IsValid(move));
        }
        
        [Test]
        public void NoPawnInvalidTest()
        {
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            var rule = new NoPawnRule();
            Assert.False(rule.IsValid(move));
        }
        
        [Test]
        public void NoPawnReasonTest()
        {
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            var rule = new NoPawnRule();
            rule.IsValid(move);
            Assert.That(move.RejectionReason, Is.EqualTo("No pawn is selected"));
        }
        
        [Test]
        public void OccupiedValidTest()
        {
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            var rule = new OccupiedRule {Successor = new ValidRule()};
            Assert.True(rule.IsValid(move));
        }
        
        [Test]
        public void OccupiedInvalidTest()
        {
            field.Positions[1, 1].Pawn = new Pawn(Color.White);
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            var rule = new OccupiedRule();
            Assert.False(rule.IsValid(move));
        }
        
        [Test]
        public void OccupiedReasonTest()
        {
            field.Positions[1, 1].Pawn = new Pawn(Color.White);
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            var rule = new OccupiedRule();
            rule.IsValid(move);
            Assert.That(move.RejectionReason, Is.EqualTo("Target position is occupied"));
        }
        
        [Test]
        public void OpponentValidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            statusProvider.UpdateStatus(Status.BlackMove);
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            var rule = new OpponentMoveRule {Successor = new ValidRule()};
            Assert.True(rule.IsValid(move));
        }
        
        [Test]
        public void OpponentInvalidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            statusProvider.UpdateStatus(Status.WhiteMove);
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            var rule = new OpponentMoveRule();
            Assert.False(rule.IsValid(move));
        }
        
        [Test]
        public void OpponentReasonTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            statusProvider.UpdateStatus(Status.WhiteMove);
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            var rule = new OpponentMoveRule();
            rule.IsValid(move);
            Assert.That(move.RejectionReason, Is.EqualTo("Pawns cannot be moved during opponent turn"));
        }
        
        [Test]
        public void SimpleMoveValidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            var rule = new SimpleMovementRule {Successor = new ValidRule()};
            Assert.True(rule.IsValid(move));
        }
        
        [Test]
        public void SimpleMoveInvalidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.White);
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            var rule = new SimpleMovementRule();
            Assert.False(rule.IsValid(move));
        }
        
        [Test]
        public void SimpleMoveReasonTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.White);
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            var rule = new SimpleMovementRule();
            rule.IsValid(move);
            Assert.That(move.RejectionReason, Is.EqualTo("Invalid move direction"));
        }
        
        [Test]
        public void DameMoveValidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            var move = CreateMove(field.Positions[0, 0], field.Positions[2, 2]);
            var rule = new DameMovementRule {Successor = new ValidRule()};
            Assert.True(rule.IsValid(move));
        }
        
        [Test]
        public void DameMoveInvalidDirectionTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 2]);
            var rule = new DameMovementRule();
            Assert.False(rule.IsValid(move));
        }
        
        [Test]
        public void DameMoveInvalidPawnOnLineTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            field.Positions[1, 1].Pawn = new Pawn(Color.Black);
            var move = CreateMove(field.Positions[0, 0], field.Positions[2, 2]);
            var rule = new DameMovementRule();
            Assert.False(rule.IsValid(move));
        }
        
        [Test]
        public void DameMoveDirectionReasonTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 2]);
            var rule = new DameMovementRule();
            rule.IsValid(move);
            Assert.That(move.RejectionReason, Is.EqualTo("Invalid movement direction"));
        }
        
        [Test]
        public void DameMovePawnOnLineReasonTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            field.Positions[1, 1].Pawn = new Pawn(Color.Black);
            var move = CreateMove(field.Positions[0, 0], field.Positions[2, 2]);
            var rule = new DameMovementRule();
            rule.IsValid(move);
            Assert.That(move.RejectionReason, Is.EqualTo("Pawns are in the way"));
        }
        
        [Test]
        public void FightValidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            field.Positions[1, 1].Pawn = new Pawn(Color.White);
            var move = CreateMove(field.Positions[0, 0], field.Positions[2, 2]);
            var rule = new FightRule {Successor = new ValidRule()};
            Assert.True(rule.IsValid(move));
        }
        
        [Test]
        public void FightInvalidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            var move = CreateMove(field.Positions[0, 0], field.Positions[2, 2]);
            var rule = new FightRule();
            Assert.False(rule.IsValid(move));
        }
        
        [Test]
        public void FightReasonTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            var move = CreateMove(field.Positions[0, 0], field.Positions[2, 2]);
            var rule = new FightRule();
            rule.IsValid(move);
            Assert.That(move.RejectionReason, Is.EqualTo("No opponent pawn in the way"));
        }
        
        [Test]
        public void ContinueValidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            statusProvider.UpdateStatus(Status.BlackAttack);
            var move = CreateMove(field.Positions[0, 0], field.Positions[2, 2]);
            var rule = new ContinueAttackRule {Successor = new ValidRule()};
            Assert.True(rule.IsValid(move));
        }
        
        [Test]
        public void ContinueNotAttackValidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            statusProvider.UpdateStatus(Status.BlackMove);
            var move = CreateMove(field.Positions[0, 0], field.Positions[2, 2]);
            var rule = new ContinueAttackRule {Successor = new ValidRule()};
            Assert.True(rule.IsValid(move));
        }
        
        [Test]
        public void ContinueInvalidTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            field.Positions[1, 1].Pawn = new Pawn(Color.Black);
            statusProvider.UpdateStatus(Status.BlackAttack);
            var move = CreateMove(field.Positions[0, 0], field.Positions[2, 2]);
            var rule = new ContinueAttackRule();
            rule.IsValid(move);
            move = CreateMove(field.Positions[1, 1], field.Positions[2, 2]);
            Assert.False(rule.IsValid(move));
        }
        
        [Test]
        public void ContinueReasonTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            field.Positions[1, 1].Pawn = new Pawn(Color.Black);
            statusProvider.UpdateStatus(Status.BlackAttack);
            var move = CreateMove(field.Positions[0, 0], field.Positions[2, 2]);
            var rule = new ContinueAttackRule();
            rule.IsValid(move);
            move = CreateMove(field.Positions[1, 1], field.Positions[2, 2]);
            rule.IsValid(move);
            Assert.That(move.RejectionReason, Is.EqualTo("You should continue attack"));
        }
        
        [Test]
        public void GameOverTest()
        {
            var move = CreateMove(field.Positions[0, 0], field.Positions[2, 2]);
            var rule = new GameIsOverRule {Successor = new ValidRule()};
            Assert.False(rule.IsValid(move));
            Assert.That(move.RejectionReason, Is.EqualTo("Game is over"));
            Assert.NotNull(rule.Successor);
        }
        
        [Test]
        public void ProviderTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            statusProvider.UpdateStatus(Status.BlackMove);
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            Assert.True(movementProvider.IsValid(move));
        }
        
        [Test]
        public void ProviderToFightCommandTest()
        {
            field.Positions[0, 0].Pawn = new Pawn(Color.Black);
            field.Positions[1, 1].Pawn = new Pawn(Color.White);
            statusProvider.UpdateStatus(Status.BlackAttack);
            var move = CreateMove(field.Positions[0, 0], field.Positions[2, 2]);
            Assert.That(movementProvider.ToCommand(move).GetType(), Is.EqualTo(typeof(FightCommand)));
        }
        
        [Test]
        public void ProviderToMoveCommandTest()
        {
            statusProvider.UpdateStatus(Status.BlackMove);
            var move = CreateMove(field.Positions[0, 0], field.Positions[1, 1]);
            Assert.That(movementProvider.ToCommand(move).GetType(), Is.EqualTo(typeof(MoveCommand)));
        }
        
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
        
        private class ValidRule : IMovementRule
        {
            public IMovementRule Successor { get; set; }
            
            public bool IsValid(Move move)
            {
                return true;
            }
        }
    }
}