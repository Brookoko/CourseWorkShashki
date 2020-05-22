namespace Movements
{
    using System;
    using System.Collections.Generic;
    using GameField;
    
    public interface IMovementProvider
    {
        IMovementRule RuleFor(Position from, Position to, Field field, List<string> rejections);
    }
    
    public class MovementProvider : IMovementProvider
    {
        private readonly List<IMovementRule> simpleMoves = new List<IMovementRule>
        {
            new SimpleMovementRule(),
            new SimpleFightRule()
        };
        
        private readonly List<IMovementRule> dameMoves = new List<IMovementRule>
        {
            new DameMovementRule(),
            new DameFightRule()
        };
        
        public IMovementRule RuleFor(Position from, Position to, Field field, List<string> rejections)
        {
            if (from.Pawn == null)
            {
                rejections.Add("No pawn at start position");
                return null;
            }
            
            return from.Pawn.IsDame ?
                GetDameRule(from, to, field, rejections) :
                GetSimpleRule(from, to, field, rejections);
        }
        
        private IMovementRule GetSimpleRule(Position from, Position to, Field field, List<string> rejections)
        {
            foreach (var rule in simpleMoves)
            {
                if (rule.IsValid(from, to, field, out var reason)) return rule;
                rejections.Add(rule.Name + ": " + reason);
            }
            return null;
        }
        
        private IMovementRule GetDameRule(Position from, Position to, Field field, List<string> rejections)
        {
            foreach (var rule in dameMoves)
            {
                if (rule.IsValid(from, to, field, out var reason)) return rule;
                rejections.Add(rule.Name + ": " + reason);
            }
            return null;
        }
    }
}