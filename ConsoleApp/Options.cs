namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Options
    {
        private readonly Dictionary<string, Action<Parameters>> options = new Dictionary<string, Action<Parameters>>();
        private readonly Dictionary<string, string[]> descriptions = new Dictionary<string, string[]>();
        
        public void AddOption(string name, string[] parameters, Action<Parameters> action)
        {
            descriptions[name] = parameters;
            options[name] = action;
        }
        
        public bool ValidateCommand(string command)
        {
            var split = command.Trim().Split(' ');
            if (split.Length == 0) return false;
            var name = split[0];
            var parameters = split.Skip(1).ToArray();
            return !string.IsNullOrEmpty(name) && options.ContainsKey(name) &&
                   SameTypes(parameters, descriptions[name]);
        }
        
        private bool SameTypes(string[] parameters, string[] patterns)
        {
            if (patterns == null) return parameters.Length == 0;
            if (parameters.Length != patterns.Length) return false;
            var param = new Parameters();
            for (var i = 0; i < patterns.Length; i++)
            {
                param.AddParameter(patterns[i], parameters[i]);
            }
            return param.Count == patterns.Length;
        }
        
        public void Execute(string command)
        {
            var (name, parameters) = ParseString(command);
            if (options.TryGetValue(name, out var action))
            {
                action?.Invoke(parameters);
            }
        }

        private (string name, Parameters param) ParseString(string command)
        {
            var split = command.Trim().Split(' ');
            var name = split[0];
            var parameters = split.Skip(1).ToArray();
            if (descriptions.TryGetValue(name, out var patterns))
            {
                return (name, CreateParameters(patterns, parameters));
            }
            return (null, null);
        }

        private Parameters CreateParameters(string[] patterns, string[] parameters)
        {
            var param = new Parameters();
            if (patterns == null) return param;
            for (var i = 1; i < patterns.Length; i++)
            {
                var pattern = patterns[i];
                var parameter = parameters[i];
                param.AddParameter(pattern, parameter);
            }
            return param;
        }

        public IEnumerable<string> GetOptions()
        {
            return descriptions.Select(p => p.Key + " " + p.Value);
        }
    }
}