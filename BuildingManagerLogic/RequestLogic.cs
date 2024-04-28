using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;

namespace BuildingManagerLogic
{
    public class RequestLogic : IRequestLogic
    {
        private readonly IRequestRepository _requestRepository;
        public RequestLogic(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }
        public Request CreateRequest(Request request)
        {
            return _requestRepository.CreateRequest(request);
        }
    }
}