using System;
using System.Collections.Generic;
using System.Linq;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;

namespace BuildingManagerLogic
{
    public class BuildingsReport
    {
        private List<Request> Requests = new List<Request>();
        private Dictionary<string, List<Request>> SortedRequests = new Dictionary<string, List<Request>>();
        private IRequestRepository requestRepository;

        public BuildingsReport(IRequestRepository repository)
        {
            requestRepository = repository;
        }

        public List<MaintenanceData> GetReport(Guid? identifier, string filter)
        {
            List<MaintenanceData> datas = new List<MaintenanceData>();
            Requests = requestRepository.GetRequests();
            if (!string.IsNullOrEmpty(filter)){
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

            foreach (var pair in SortedRequests)
            {
                int open = 0;
                int close = 0;
                int inProgress = 0;
                int averageTime = 0;

                foreach (var request in pair.Value)
                {
                    if (request.State == RequestState.OPEN)
                    {
                        open++;
                    }
                    else if (request.State == RequestState.CLOSE)
                    {
                        close++;
                    }
                    else if (request.State == RequestState.PENDING)
                    {
                        inProgress++;
                    }
                }
                datas.Add(new MaintenanceData(open, close, inProgress, averageTime, pair.Key));
            }

            return datas;
        }
    }
}