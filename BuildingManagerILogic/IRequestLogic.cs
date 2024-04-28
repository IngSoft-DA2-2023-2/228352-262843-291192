using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerILogic
{
    public interface IRequestLogic
    {
        public Request CreateRequest(Request request);
    }
}