using System;
using System.Collections.Generic;
using BuildingManagerDomain.Entities;

namespace BuildingManagerILogic
{
    public interface IRequestLogic
    {
        Request AssignStaff(Guid id, Guid maintenanceStaffId);
        Request AttendRequest(Guid id, Guid managerSessionToken);
        public Request CreateRequest(Request request);
        public List<Request> GetRequests();
    }
}