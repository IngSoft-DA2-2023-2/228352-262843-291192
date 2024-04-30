using System;
using System.Collections.Generic;
using System.Linq;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;

namespace BuildingManagerLogic
{
    public abstract class Report
    {
        internal List<Request> Requests = new List<Request>();
        internal Dictionary<string, List<Request>> SortedRequests = new Dictionary<string, List<Request>>();
        private IRequestRepository requestRepository;

        public Report(IRequestRepository repository)
        {
            requestRepository = repository;
        }

        public List<MaintenanceData> GetReport(Guid? identifier, string filter)
        {
            List<MaintenanceData> datas = new List<MaintenanceData>();

            LoadRequests();

            SortRequests(identifier, filter);

            datas = ConvertToDatas();
            return datas;
        }

        private void LoadRequests()
        {
            Requests = requestRepository.GetRequests();
        }

        internal abstract void SortRequests(Guid? identifier, string filter);

        private List<MaintenanceData> ConvertToDatas()
        {
            List<MaintenanceData> datas = new List<MaintenanceData>();

            foreach (var pair in SortedRequests)
            {
                int open = 0;
                int close = 0;
                int inProgress = 0;
                int averageTime = 0;
                string name = "";
                Guid buildingId;

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