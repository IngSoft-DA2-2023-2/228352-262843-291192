using System;

namespace BuildingManagerIDataAccess.Exceptions
{
    public class ValueNotFoundException: Exception
    {
        public ValueNotFoundException(string msg) : base(msg) { }
    }
}
