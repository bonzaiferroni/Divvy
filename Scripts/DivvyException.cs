using System;

namespace Bonwerk.Divvy
{
    public class DivvyException : Exception
    {
        public DivvyException(string s) : base(s) { }
    }
}