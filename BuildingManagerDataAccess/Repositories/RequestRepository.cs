using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
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
            _context.Set<Request>().Add(request);
            _context.SaveChanges();

            return request;
        }
    }
}