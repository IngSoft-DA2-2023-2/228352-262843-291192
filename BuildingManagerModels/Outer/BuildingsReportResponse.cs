using System.Collections.Generic;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class BuildingsReportResponse
    {
        public List<BuildingsReportData> Datas { get; set; }

        public BuildingsReportResponse(List<MaintenanceData> datas)
        {
            Datas = new List<BuildingsReportData>();
            foreach (var data in datas)
            {
                Datas.Add(new BuildingsReportData(
                    data.OpenRequests,
                    data.CloseRequests,
                    data.InProgressRequests,
                    data.BuildingId
                    ));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (BuildingsReportResponse)obj;
            foreach (var data in Datas)
            {
                foreach (var otherData in other.Datas)
                {
                    if (data.OpenRequests != otherData.OpenRequests ||
                    data.CloseRequests != otherData.CloseRequests ||
                    data.InProgressRequests != otherData.InProgressRequests ||
                    data.BuildingId != otherData.BuildingId)
                        return false;
                }
            }
            return true;
        }
    }
}