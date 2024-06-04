using System;
using System.Collections.Generic;
using System.Linq;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;

namespace BuildingManagerLogic
{
    public abstract class Report
    {
        internal List<Request> Requests = new List<Request>();
        internal Dictionary<string, List<Request>> SortedRequests = new Dictionary<string, List<Request>>();
        private IRequestRepository requestRepository;
        private IBuildingLogic _buildingLogic;

        public Report(IRequestRepository repository, IBuildingLogic buildingLogic)
        {
            requestRepository = repository;
            _buildingLogic = buildingLogic;
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
                Building building = null;
                Guid buildingId = Guid.Empty;
                string categoryName = pair.Value.First().Category.Name;
                int? apartmentFloor = null;
                int? apartmentNumber = null;
                string ownerName = null;
                string buildingName = null;

                foreach (var request in pair.Value)
                {
                    if (request.MaintenanceStaff != null)
                    {
                        name = request.MaintenanceStaff.Name;
                    }
                    if (building == null)
                    {
                        building = _buildingLogic.GetBuildingById(request.BuildingId);
                    }
                    if (ownerName == null && building != null)
                    {
                        Owner owner = building.Apartments.First(a => a.Floor == request.ApartmentFloor && a.Number == request.ApartmentNumber).Owner;
                        ownerName = owner.Name + " " + owner.LastName;
                    }
                    buildingId = request.BuildingId;
                    apartmentFloor = request.ApartmentFloor;
                    apartmentNumber = request.ApartmentNumber;
                    buildingName = building.Name;
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
                datas.Add(new ReportData(open, close, inProgress, (int)averageTime, name, buildingId, categoryName, apartmentFloor, apartmentNumber, ownerName, buildingName));
            }

            return datas;
        }

    }
}