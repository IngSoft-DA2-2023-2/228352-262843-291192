using System;
using System.Collections.Generic;
using System.Linq;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;

namespace BuildingManagerLogic
{
    public class MaintenanceReport : Report
    {

        public MaintenanceReport(IRequestRepository repository, IBuildingLogic buildingLogic) : base(repository, buildingLogic) { }

        internal override void SortRequests(Guid? identifier, string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                Requests = Requests.Where(r => r.MaintenanceStaff != null && r.MaintenanceStaff.Name == filter).ToList();
            }
            foreach (var request in Requests.Where(r => r.BuildingId == identifier))
            {
                if (request.MaintainerStaffId != Guid.Empty && request.MaintainerStaffId != null)
                {
                    if (SortedRequests.ContainsKey(request.MaintainerStaffId.ToString()))
                    {
                        SortedRequests[request.MaintainerStaffId.ToString()].Add(request);
                    }
                    else
                    {
                        List<Request> newList = new List<Request>
                    {
                        request
                    };
                        SortedRequests[request.MaintainerStaffId.ToString()] = newList;
                    }
                }
            }
        }
    }
}