using System;
using System.Collections.Generic;
using System.Linq;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;

namespace BuildingManagerLogic
{
    public class MaintenanceReport : Report
    {

        public MaintenanceReport(IRequestRepository repository) : base(repository) { }

        internal override void SortRequests(Guid? identifier, string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                Requests = Requests.Where(r => r.MaintenanceStaff != null && r.MaintenanceStaff.Name == filter).ToList();
            }
            foreach (var request in Requests.Where(r => r.BuildingId == identifier))
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