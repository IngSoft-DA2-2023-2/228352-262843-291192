using System;

namespace BuildingManagerIDataAccess.Exceptions
{
    public class EmailDuplicatedException: Exception
    {
        public EmailDuplicatedException() : base("Email already exists") { }
    }
}
