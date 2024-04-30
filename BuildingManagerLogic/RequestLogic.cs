using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using System.Collections.Generic;

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
            try
            {
                return _requestRepository.CreateRequest(request);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }

        }

        public List<Request> GetRequests()
        {
            return _requestRepository.GetRequests();
        }
    }
}