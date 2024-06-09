using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class ListRequestResponse
    {
        public List<RequestResponse> Requests { get; set; }

        public ListRequestResponse(List<Request> requests)
        {
            Requests = new List<RequestResponse>();
            foreach (var request in requests)
            {
                Requests.Add(new RequestResponse(
                   request
                    ));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (ListRequestResponse)obj;
            foreach (var request in Requests)
            {
                foreach (var otherRequest in other.Requests)
                {
                    if (request.ApartmentFloor != otherRequest.ApartmentFloor ||
                    request.ApartmentNumber != otherRequest.ApartmentNumber ||
                    request.AttendedAt != otherRequest.AttendedAt ||
                    request.Id != otherRequest.Id ||
                    request.BuildingId != otherRequest.BuildingId ||
                    request.BuildingName != otherRequest.BuildingName ||
                    request.CategoryId != otherRequest.CategoryId ||
                    request.CategoryName != otherRequest.CategoryName ||
                    request.CompletedAt != otherRequest.CompletedAt ||
                    request.Cost != otherRequest.Cost ||
                    request.Description != otherRequest.Description ||
                    request.ManagerId != otherRequest.ManagerId ||
                    request.MaintainerStaffId != otherRequest.MaintainerStaffId ||
                    request.MaintainerStaffName != otherRequest.MaintainerStaffName ||
                    request.State != otherRequest.State)
                        return false;
                }
            }
            return true;
        }
    }
}