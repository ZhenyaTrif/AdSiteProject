using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ICategoryWrapper
    {
        IEnumerable<Category> GetAll();

        void Delete(int id);

        void Update(Category category);

        int Insert(Category category);
    }
}
