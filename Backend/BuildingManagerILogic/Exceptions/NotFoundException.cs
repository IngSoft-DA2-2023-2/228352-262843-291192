using System;

namespace BuildingManagerILogic.Exceptions
{
    public class NotFoundException(Exception e, string message) : Exception(message){}
}
