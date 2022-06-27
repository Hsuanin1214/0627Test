using Auth_Lab.Models.DBEntity;
using Auth_Lab.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_Lab.Repository
{
    public class DBRepository : IDBRepository
    {
        private readonly AuthLabContext _bSDBContext;
        public DBRepository(AuthLabContext bSDBContext)
        {
            _bSDBContext = bSDBContext;
        }
       public AuthLabContext authDbContext { get { return _bSDBContext; } }

        public void Create<T>(T Entity) where T : class
        {
            _bSDBContext.Entry<T>(Entity).State = EntityState.Added;
        }

        public void Delete<T>(T Entity) where T : class
        {
            _bSDBContext.Entry<T>(Entity).State = EntityState.Deleted;
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _bSDBContext.Set<T>();
        }

        public void Save()
        {
            _bSDBContext.SaveChanges();
        }

        public void Update<T>(T Entity) where T : class
        {
            _bSDBContext.Entry<T>(Entity).State = EntityState.Modified;
        }
    }
}

