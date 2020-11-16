using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Repository
{
    public class SqlAdmissionRepo<T> : IAdmissionRepo<T> where T : class
    {
        private readonly AddmissionContext _context;

        public SqlAdmissionRepo(AddmissionContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public int GetCount()
        {
           return _context.Set<T>().Count();
        }

        public async Task<IEnumerable<T>> GetT()
        {
            var t= await _context.Set<T>().ToListAsync();
            return t; 
        }

        public async Task<T> GetTById(int id)
        {
            var t = await _context.Set<T>().FindAsync(id);
            return t;
        }

        public async Task<T> SaveAsync(T entity)
        {
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
