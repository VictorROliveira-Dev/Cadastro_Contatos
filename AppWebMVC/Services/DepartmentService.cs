using AppWebMVC.Data;
using AppWebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AppWebMVC.Services
{
    public class DepartmentService
    {
        private readonly AppWebMVCContext _context;

        public DepartmentService(AppWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(dep => dep.Name).ToListAsync();
        }
    }
}
