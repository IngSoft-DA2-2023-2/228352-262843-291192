using System.Collections.Generic;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class MaintenanceReportResponse
    {
        public List<ReportData> Datas { get; set; }

        public MaintenanceReportResponse(List<ReportData> datas)
        {
            Datas = datas;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (MaintenanceReportResponse)obj;
            foreach(var data in Datas)
            {
                foreach(var otherData in other.Datas)
                {
                    if (data.OpenRequests != otherData.OpenRequests || data.CloseRequests != otherData.CloseRequests || data.AverageClosingTime != otherData.AverageClosingTime || data.InProgressRequests != otherData.InProgressRequests || data.MaintainerName != otherData.MaintainerName)
                        return false;
                }
            }
            return true;
        }
    }
}