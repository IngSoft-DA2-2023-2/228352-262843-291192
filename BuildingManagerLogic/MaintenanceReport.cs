using System;
using System.Collections.Generic;
using System.Linq;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;

namespace BuildingManagerLogic
{
    public class MaintenanceReport
    {
        private List<Request> Requests = new List<Request>();
        private Dictionary<string, List<Request>> SortedRequests = new Dictionary<string, List<Request>>();
        private IRequestRepository requestRepository;

        public MaintenanceReport(IRequestRepository repository)
        {
            requestRepository = repository;
        }

        public List<MaintenanceData> GetReport(Guid buildingId)
        {
            List<MaintenanceData> datas = new List<MaintenanceData>();
            Requests = requestRepository.GetRequests();

            foreach (var request in Requests.Where(r => r.BuildingId == buildingId))
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

            foreach (var pair in SortedRequests)
            {
                int open = 0;
                int close = 0;
                int inProgress = 0;
                int averageTime = 0;
                string name = "";

                foreach (var request in pair.Value)
                {
                    name = request.MaintenanceStaff.Name;
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
                datas.Add(new MaintenanceData(open, close, inProgress, averageTime, name));
            }

            return datas;
        }
    }
}