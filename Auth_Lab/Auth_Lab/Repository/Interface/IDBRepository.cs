using Auth_Lab.Models.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_Lab.Repository.Interface
{
    public interface IDBRepository
    {
        public AuthLabContext authDbContext { get; }
      
        public void Create<T>(T Entity) where T : class;
        public void Update<T>(T Entity) where T : class;
        public void Delete<T>(T Entity) where T : class;
        public IQueryable<T> GetAll<T>() where T : class;
        public void Save();
    }
}
