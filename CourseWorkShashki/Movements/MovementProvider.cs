namespace Movements
{
    using System.Collections.Generic;
    using GameField;
    
    public interface IMovementProvider
    {
        IMovementRule RuleFor(Position from, Position to, Field field, List<string> rejections);
    }
    
    public class MovementProvider : IMovementProvider
    {
        private readonly List<IMovementRule> rules = new List<IMovementRule>
        {
            new SimpleMovementRule(),
            new FightRule(),
            new DameMovementRule()
        };
        
        public IMovementRule RuleFor(Position from, Position to, Field field, List<string> rejections)
        {
            foreach (var rule in rules)
            {
                if (rule.IsValid(from, to, field, out var reason))
                {
                    return rule;
                }
                rejections.Add(rule.Name + ": " + reason);
            }
            return null;
        }
    }
}