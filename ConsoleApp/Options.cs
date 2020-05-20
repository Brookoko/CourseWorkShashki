namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Options : IOptions
    {
        private readonly Dictionary<string, Action<Parameters>> options = new Dictionary<string, Action<Parameters>>();
        private readonly Dictionary<string, string> descriptions = new Dictionary<string, string>();

        public abstract string Id { get; }

        public void AddOption(string option, Action<Parameters> action)
        {
            var parameters = option.Trim().Split(' ');
            var name = parameters[0];
            descriptions[name] = option;
            options[name] = action;
        }

        public bool ValidateInput(string option)
        {
            var name = option.Trim().Split(' ')[0];
            return !string.IsNullOrEmpty(name) && options.ContainsKey(name) &&
                   SameTypes(option, descriptions[name]);
        }

        private bool SameTypes(string option, string description)
        {
            var first = option.Trim().Split(' ').Skip(1).Select(s => s[0]).ToList();
            var second = description.Trim().Split(' ').Skip(1).Select(s => s[0]).ToList();
            return first.Count == second.Count;
        }

        public void RunOption(string option)
        {
            var (name, parameters) = ParseString(option);
            if (options.TryGetValue(name, out var action))
            {
                action?.Invoke(parameters);
            }
        }
        
        private (string name, Parameters param) ParseString(string option)
        {
            option = option.Trim();
            var parameters = option.Split(' ');
            var name = parameters[0];
            if (descriptions.TryGetValue(name, out var description))
            {
                var patterns = description.Split(' ');
                return (name, CreateParameters(patterns, parameters));
            }
            return (null, null);
        }
        
        private Parameters CreateParameters(string[] patterns, string[] parameters)
        {
            var param = new Parameters();
            for (var i = 1; i < patterns.Length; i++)
            {
                var pattern = patterns[i];
                var parameter = parameters[i];
                param.AddParameter(pattern, parameter);
            }
            return param;
        }
        
        public IEnumerable<string> OptionsList()
        {
            return descriptions.Values;
        }
    }
}