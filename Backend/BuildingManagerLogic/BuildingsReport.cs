using System;
using System.Collections.Generic;
using System.Linq;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;

namespace BuildingManagerLogic
{
    public class BuildingsReport : Report
    {

        public BuildingsReport(IRequestRepository repository) : base(repository) { }

        internal override void SortRequests(Guid? identifier, string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                Requests = Requests.Where(r => r.BuildingId != null && r.BuildingId.ToString() == filter).ToList();
            }
            foreach (var request in Requests)
            {
                if (SortedRequests.ContainsKey(request.BuildingId.ToString()))
                {
                    SortedRequests[request.BuildingId.ToString()].Add(request);
                }
                else
                {
                    List<Request> newList = new List<Request>
                    {
                        request
                    };
                    SortedRequests[request.BuildingId.ToString()] = newList;
                }
            }
        }
    }
}