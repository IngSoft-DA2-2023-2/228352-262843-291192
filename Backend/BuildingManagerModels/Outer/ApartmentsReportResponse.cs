using BuildingManagerDomain.Entities;
using System.Collections.Generic;

namespace BuildingManagerModels.Outer
{
    public class ApartmentsReportResponse
    {
        public List<ApartmentsReportData> Data { get; set; }

        public ApartmentsReportResponse(List<ReportData> data)
        {
            Data = new List<ApartmentsReportData>();
            foreach (var reportData in data)
            {
                Data.Add(new ApartmentsReportData(
                    (int)reportData.ApartmentFloor,
                    (int)reportData.ApartmentNumber,
                    reportData.OwnerName,
                    reportData.OpenRequests,
                    reportData.CloseRequests,
                    reportData.InProgressRequests
                ));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (ApartmentsReportResponse)obj;
            foreach (var data in Data)
            {
                foreach (var otherData in other.Data)
                {
                    if (data.ApartmentFloor != otherData.ApartmentFloor ||
                        data.ApartmentNumber != otherData.ApartmentNumber ||
                        data.OwnerName != otherData.OwnerName ||
                        data.OpenRequests != otherData.OpenRequests ||
                        data.CloseRequests != otherData.CloseRequests ||
                        data.InProgressRequests != otherData.InProgressRequests)
                        return false;
                }
            }
            return true;
        }
    }
}
