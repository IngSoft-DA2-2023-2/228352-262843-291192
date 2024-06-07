using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildingManagerLogic
{
    public class CategoriesReport : Report
    {
        public CategoriesReport(IRequestRepository repository, IBuildingLogic buildingLogic) : base(repository, buildingLogic) { }

        internal override void SortRequests(Guid? identifier, string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                Requests = Requests.Where(r => r.Category.Name == filter).ToList();
            }
            foreach (var request in Requests.Where(r => r.BuildingId == identifier))
            {
                if (SortedRequests.ContainsKey(request.Category.Name))
                {
                    SortedRequests[request.Category.Name].Add(request);
                }
                else
                {
                    List<Request> newList = new List<Request>
                    {
                        request
                    };
                    SortedRequests[request.Category.Name] = newList;
                }
            }
        }
    }
}
