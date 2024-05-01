using System;
using System.Collections.Generic;
using BuildingManagerDomain.Entities;

namespace BuildingManagerILogic
{
    public interface IRequestLogic
    {
        Request AssignStaff(Guid id, Guid maintenanceStaffId);
        Request AttendRequest(Guid id, Guid managerSessionToken);
        Request CompleteRequest(Guid id, int cost);
        public Request CreateRequest(Request request, Guid managerSessionToken);
        object GetAssignedRequests(Guid managerSessionToken);
        public List<Request> GetRequests();
        List<Request> GetRequestsByManager(Guid managerSessionToken);
    }
}