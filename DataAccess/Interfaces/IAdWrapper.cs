using Common.Entities;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IAdWrapper
    {
        IEnumerable<Ad> GetAll();

        void Delete(int id);

        void Update(Ad ad);

        void Insert(Ad ad);
    }
}
