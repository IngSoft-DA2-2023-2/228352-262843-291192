using BuildingManagerDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagerModels.Outer
{
    public class CategoriesReportResponse
    {
        public List<CategoriesReportData> Data { get; set; }

        public CategoriesReportResponse(List<ReportData> data)
        {
            Data = new List<CategoriesReportData>();
            foreach (var reportData in data)
            {
                Data.Add(new CategoriesReportData(
                    reportData.CategoryName,
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

            var other = (CategoriesReportResponse)obj;
            foreach (var data in Data)
            {
                foreach (var otherData in other.Data)
                {
                    if (data.CategoryName != otherData.CategoryName ||
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
