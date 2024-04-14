using System;

namespace BuildingManagerILogic.Exceptions
{
    public class DuplicatedValueException: Exception
    {
        public DuplicatedValueException(Exception e, string value) : base("The " + value + " is already in use")
        {

        }
    }
}
