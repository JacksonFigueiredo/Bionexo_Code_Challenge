using API.Data;
using API.Model.Base;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Implementation
{
    public class GenericRepositoryImplementation<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly SqlContext _context;
        private readonly DbSet<T> dataset;

        public GenericRepositoryImplementation(SqlContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }
        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(i => i.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(long? id)
        {
            return dataset.Any(b => b.Id.Equals(id));
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindById(long id)
        {
            return dataset.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Update(T item)
        {
            if (!Exists(item.Id)) return null;

            var result = dataset.SingleOrDefault(b => b.Id == item.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
    }
}
