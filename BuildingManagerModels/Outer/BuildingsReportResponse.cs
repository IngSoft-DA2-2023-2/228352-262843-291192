using System.Collections.Generic;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class BuildingsReportResponse
    {
        public List<MaintenanceData> Datas { get; set; }

        public BuildingsReportResponse(List<MaintenanceData> datas)
        {
            Datas = datas;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (BuildingsReportResponse)obj;
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