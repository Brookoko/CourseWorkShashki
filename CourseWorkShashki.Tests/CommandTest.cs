namespace Checkers.Tests
{
    using System;
    using Commands;
    using DependencyInjection;
    using GameField;
    using GameStatus;
    using NUnit.Framework;
    using PathFinding;

    [TestFixture]
    public class CommandTest
    {
        private readonly Field field = new Field();
        private readonly GameStatusProvider statusProvider = new GameStatusProvider();
        private readonly FieldProvider fieldProvider = new FieldProvider();
        private readonly CommandQueue queue = new CommandQueue();
        
        [OneTimeSetUp]
        public void SetUp()
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
            queue.InjectionBinder = new Injector();
        }
        
        [TearDown]
        public void TearDown()
        {
            foreach (var position in field.Positions)
            {
                position.Pawn = null;
            }
            queue.Reset();
        }
        
        [Test]
        public void MoveTest()
        {
            var pawn = new Pawn(Color.Black);
            var move = CreateMove(pawn, field.Positions[2, 2]);
            move.Execute();
            Assert.That(field.Positions[2, 2].Pawn, Is.EqualTo(pawn));
        }

        [Test]
        public void MoveUndoTest()
        {
            var pawn = new Pawn(Color.Black);
            var move = CreateMove(pawn, field.Positions[2, 2]);
            move.Execute();
            move.Undo();
            Assert.That(field.Positions[0, 0].Pawn, Is.EqualTo(pawn));
        }
        
        [Test]
        public void MoveTurnDameTest()
        {
            var pawn = new Pawn(Color.Black);
            var move = CreateMove(pawn, field.Positions[7, 7]);
            move.Execute();
            Assert.True(pawn.IsDame);
        }
        
        [Test]
        public void MoveNotTurnDameTest()
        {
            var pawn = new Pawn(Color.Black);
            var move = CreateMove(pawn, field.Positions[7, 7]);
            move.Execute();
            move.Undo();
            Assert.False(pawn.IsDame);
        }
        
        private MoveCommand CreateMove(Pawn pawn, Position target)
        {
            field.Positions[0, 0].Pawn = pawn;
            return new MoveCommand(field.Positions[0, 0], target)
            {
                GameStatusProvider = statusProvider
            };
        }
        
        [Test]
        public void FightTest()
        {
            var pawn = new Pawn(Color.Black);
            var opponent = new Pawn(Color.White);
            var fight = CreateFight(pawn, opponent, field.Positions[2, 2]);
            fight.Execute();
            Assert.That(field.Positions[2, 2].Pawn, Is.EqualTo(pawn));
            Assert.That(field.Positions[1, 1].Pawn, Is.Not.EqualTo(opponent));
        }
        
        [Test]
        public void FightUndoTest()
        {
            var pawn = new Pawn(Color.Black);
            var opponent = new Pawn(Color.White);
            var fight = CreateFight(pawn, opponent, field.Positions[2, 2]);
            fight.Execute();
            fight.Undo();
            Assert.That(field.Positions[0, 0].Pawn, Is.EqualTo(pawn));
            Assert.That(field.Positions[1, 1].Pawn, Is.EqualTo(opponent));
        }
        
        [Test]
        public void FightTurnDameTest()
        {
            var pawn = new Pawn(Color.Black);
            var opponent = new Pawn(Color.White);
            var fight = CreateFight(pawn, opponent, field.Positions[7, 7]);
            fight.Execute();
            Assert.True(pawn.IsDame);
        }
        
        [Test]
        public void FightNotTurnDameTest()
        {
            var pawn = new Pawn(Color.Black);
            var opponent = new Pawn(Color.White);
            var fight = CreateFight(pawn, opponent, field.Positions[7, 7]);
            fight.Execute();
            fight.Undo();
            Assert.True(pawn.IsDame);
        }
        
        [Test]
        public void FightStatusTest()
        {
            statusProvider.UpdateStatus(Status.BlackAttack);
            var pawn = new Pawn(Color.Black);
            var opponent = new Pawn(Color.White);
            field.Positions[3, 3].Pawn = new Pawn(Color.White);
            var fight = CreateFight(pawn, opponent, field.Positions[2, 2]);
            fight.Execute();
            fight.Undo();
            Assert.That(statusProvider.Status, Is.EqualTo(Status.BlackAttack));
        }
        
        private FightCommand CreateFight(Pawn pawn, Pawn opponent, Position target)
        {
            field.Positions[0, 0].Pawn = pawn;
            field.Positions[1, 1].Pawn = opponent;
            var path = new Path(field.Positions[0, 0]);
            path.Opponents.Add(field.Positions[1, 1]);
            path.Positions.Add(field.Positions[2, 2]);
            var fight = new FightCommand(field.Positions[0, 0], target, path)
            {
                GameStatusProvider = statusProvider,
                FieldProvider = fieldProvider
            };
            return fight;
        }

        [Test]
        public void QueueExecuteTest()
        {
            var pawn = new Pawn(Color.Black);
            var move = CreateMove(pawn, field.Positions[2, 2]);
            queue.Execute(move);
            Assert.That(field.Positions[2, 2].Pawn, Is.EqualTo(pawn));
        }
        
        [Test]
        public void QueueUndoTest()
        {
            var pawn = new Pawn(Color.Black);
            var move = CreateMove(pawn, field.Positions[2, 2]);
            queue.Execute(move);
            queue.Undo();
            Assert.That(field.Positions[0, 0].Pawn, Is.EqualTo(pawn));
        }
        
        [Test]
        public void QueueUndoEmptyTest()
        {
            queue.Undo();
        }
        
        private class Injector : IInjectionBinder
        {
            public object Inject(object obj)
            {
                return obj;
            }

            public IBinder Bind<T>()
            {
                throw new NotImplementedException();
            }

            public IBinder Bind(Type type)
            {
                throw new NotImplementedException();
            }

            public void Unbind<T>()
            {
                throw new NotImplementedException();
            }

            public void Unbind(Type type)
            {
                throw new NotImplementedException();
            }

            public T Get<T>() where T : class
            {
                throw new NotImplementedException();
            }

            public object Get(Type type)
            {
                throw new NotImplementedException();
            }
        }
    }
}