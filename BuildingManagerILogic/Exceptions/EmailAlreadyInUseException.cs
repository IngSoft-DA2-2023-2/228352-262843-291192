using System;

namespace BuildingManagerILogic.Exceptions
{
    public class EmailAlreadyInUseException: Exception
    {
        public EmailAlreadyInUseException(Exception e) : base("The email is already in use")
        {

        }
    }
}
