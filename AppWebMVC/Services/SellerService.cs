using AppWebMVC.Data;
using AppWebMVC.Models;
using AppWebMVC.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace AppWebMVC.Services
{
    public class SellerService
    {
        private readonly AppWebMVCContext _context;

        public SellerService(AppWebMVCContext context)
        {
            _context = context;
        }   

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Sellers.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Sellers.Include(dep => dep.Department).FirstOrDefaultAsync(dep => dep.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Sellers.FindAsync(id);
                _context.Sellers.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException ex)
            {
                throw new IntegrityException("Não pode deletar o vendedor, porque ele/ela possui vendas ativas!");
            }
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny =  await _context.Sellers.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found!");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException ex)
            { 
                throw new DbConcurrencyException(ex.Message);
            }           
        }
    }
}
