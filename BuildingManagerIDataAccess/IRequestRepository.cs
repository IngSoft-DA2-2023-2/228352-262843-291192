using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerIDataAccess
{
    public interface IRequestRepository
    {
        Request CreateRequest (Request request);
    }
}