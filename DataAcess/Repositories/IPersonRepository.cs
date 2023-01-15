using DataAcess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories
{
    public interface IPersonRepository
    {
        Task<bool> Add(Person person);
        Task<bool> Update(Person person);
        Task<Person> GetById(int id);
        Task<bool> Delete(int id);
        Task<IEnumerable<Person>> GetAll();
    }
}
