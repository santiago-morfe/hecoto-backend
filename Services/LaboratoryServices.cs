using hecotoBackend.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace hecotoBackend.Services
{
    public class LaboratoryServices
    {
        private readonly AppDbContext _context;
        public LaboratoryServices(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // comprovar si un usuario tiene acceso a un laboratorio
        public async Task<bool> UserHasAccessToLaboratory(int userId, int laboratoryId)
        {
            return false;
        }
    }
}