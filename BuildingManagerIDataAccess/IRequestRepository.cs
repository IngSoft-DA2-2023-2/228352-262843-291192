using System;
using System.Collections.Generic;
using BuildingManagerDomain.Entities;

namespace BuildingManagerIDataAccess
{
    public interface IRequestRepository
    {
        Request AssignStaff(Guid id, Guid maintenanceStaffId);
        Request AttendRequest(Guid id, Guid managerSessionToken);
        Request CompleteRequest(Guid id, int cost);
        Request CreateRequest (Request request);
        object GetAssignedRequests(Guid managerSessionToken);
        List<Request> GetRequests();
        List<Request> GetRequestsByManager(Guid managerSessionToken);
    }
}