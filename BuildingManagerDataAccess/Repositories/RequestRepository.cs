using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccess.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly DbContext _context;
        public RequestRepository(DbContext context)
        {
            _context = context;
        }
        public Request CreateRequest(Request request)
        {
            try
            {
                _context.Set<Request>().Add(request);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new ValueNotFoundException("CategoryId or ApartmentNumber with ApartmentFloor in BuildingId not found.");
            }
            
            return request;
        }

        public List<Request> GetRequests()
        {
            throw new NotImplementedException();
        }
    }
}