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

        public List<ReportData> GetReport(Guid? identifier, string filter)
        {
            List<ReportData> data = new List<ReportData>();

            LoadRequests();

            SortRequests(identifier, filter);

            data = ConvertToDatas();
            return data;
        }

        private void LoadRequests()
        {
            Requests = requestRepository.GetRequests();
        }

        internal abstract void SortRequests(Guid? identifier, string filter);

        private List<ReportData> ConvertToDatas()
        {
            List<ReportData> datas = new List<ReportData>();

            foreach (var pair in SortedRequests)
            {
                int open = 0;
                int close = 0;
                int inProgress = 0;
                long totalTime = 0;
                long averageTime = 0;
                string name = "";
                Guid buildingId = Guid.Empty;
                string categoryName = pair.Value.First().Category.Name;

                foreach (var request in pair.Value)
                {
                    if (request.MaintenanceStaff != null)
                    {
                        name = request.MaintenanceStaff.Name;
                    }
                    buildingId = request.BuildingId;
                    if (request.State == RequestState.OPEN)
                    {
                        open++;
                    }
                    else if (request.State == RequestState.CLOSE)
                    {
                        close++;
                        totalTime += (request.CompletedAt - request.AttendedAt);
                    }
                    else if (request.State == RequestState.PENDING)
                    {
                        inProgress++;
                    }
                }
                if (close > 0)
                {
                    int convertSecondsToHours = 3600;
                    averageTime = (totalTime / close) / convertSecondsToHours;
                }
                datas.Add(new ReportData(open, close, inProgress, (int)averageTime, name, buildingId, categoryName));
            }

            return datas;
        }

    }
}