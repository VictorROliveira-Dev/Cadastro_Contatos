using AppWebMVC.Data;
using AppWebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AppWebMVC.Services
{
    public class SalesRecordService
    {
        private readonly AppWebMVCContext _context;

        public SalesRecordService(AppWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecords select obj;

            if (minDate.HasValue)
            {
                result = result.Where(sr => sr.Date >= minDate);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(sr => sr.Date <= maxDate);
            }

            return await result
                   .Include(sr => sr.Seller)
                   .Include(sr => sr.Seller.Department)
                   .OrderByDescending(sr => sr.Date)
                   .ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecords select obj;

            if (minDate.HasValue)
            {
                result = result.Where(sr => sr.Date >= minDate);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(sr => sr.Date <= maxDate);
            }

            return await result
                   .Include(sr => sr.Seller)
                   .Include(sr => sr.Seller.Department)
                   .OrderByDescending(sr => sr.Date)
                   .GroupBy(sr => sr.Seller.Department)
                   .ToListAsync();
        }
    }
}
