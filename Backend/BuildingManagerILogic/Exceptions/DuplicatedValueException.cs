using System;

namespace BuildingManagerILogic.Exceptions
{
    public class DuplicatedValueException: Exception
    {
        public DuplicatedValueException(Exception e, string message) : base(message)
        {

        }
    }
}
