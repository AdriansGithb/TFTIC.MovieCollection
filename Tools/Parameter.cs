using System;

namespace Tools
{
    internal class Parameter
    {
        internal string Name { get; set; }
        internal object Value { get; set; }

        public Parameter(string name, object value)
        {
            Name = name;
            Value = value ?? DBNull.Value;
        }

    }
}