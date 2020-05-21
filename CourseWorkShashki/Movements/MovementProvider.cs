namespace Movements
{
    using System.Collections.Generic;
    using GameField;
    
    public interface IMovementProvider
    {
        IMovementRule RuleFor(Position from, Position to, List<string> rejections);
    }
    
    public class MovementProvider : IMovementProvider
    {
        private readonly List<IMovementRule> rules = new List<IMovementRule>
        {
            new SimpleMovementRule()
        };
        
        public IMovementRule RuleFor(Position from, Position to, List<string> rejections)
        {
            foreach (var rule in rules)
            {
                if (rule.IsValid(from, to, out var reason))
                {
                    return rule;
                }
                rejections.Add(rule.GetType() + ": " + reason);
            }
            return null;
        }
    }
}