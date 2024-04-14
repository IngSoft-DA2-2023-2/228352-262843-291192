using System;

namespace BuildingManagerIDataAccess.Exceptions
{
    public class ValueDuplicatedException: Exception
    {
        public ValueDuplicatedException(string msg) : base(msg + " already exists") { }
    }
}
