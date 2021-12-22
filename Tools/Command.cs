using System;
using System.Collections.Generic;

namespace Tools
{
    public class Command
    {
        internal string Query { get; private set; }
        internal bool IsStoredProcedure { get; private set; }
        internal Dictionary<string, Parameter> Parameters { get; private set; }

        public Command(string query, bool isStoredProcedure)
        {
            if(string.IsNullOrWhiteSpace(query))
                throw new ArgumentException(nameof(query));

            Query = query;
            IsStoredProcedure = isStoredProcedure;
            Parameters = new Dictionary<string, Parameter>();
        }

        public void AddParameter(string parameterName, object value)
        {
            Parameters.Add(parameterName, new Parameter(parameterName, value));
        }
    }
}