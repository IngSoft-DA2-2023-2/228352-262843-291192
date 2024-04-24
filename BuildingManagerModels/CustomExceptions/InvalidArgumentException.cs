using System;

namespace BuildingManagerModels.CustomExceptions
{
    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException(string argument) : base("Invalid " + argument + ".") { }
    }
}
