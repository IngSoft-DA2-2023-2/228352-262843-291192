using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildingManagerLogic
{
    public class ApartmentsReport : Report
    {
        public ApartmentsReport(IRequestRepository repository, IBuildingLogic buildingLogic) : base(repository, buildingLogic) { }

        internal override void SortRequests(Guid? identifier, string filter)
        {
            foreach (var request in Requests.Where(r => r.BuildingId == identifier))
            {
                string key = request.ApartmentFloor + "-" + request.ApartmentNumber;
                if (SortedRequests.ContainsKey(key))
                {
                    SortedRequests[key].Add(request);
                }
                else
                {
                    List<Request> newList = new List<Request>
                    {
                        request
                    };
                    SortedRequests[key] = newList;
                }
            }
        }
    }
}
