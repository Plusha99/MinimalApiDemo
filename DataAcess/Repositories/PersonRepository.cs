using DataAcess.Data;
using DataAcess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonContext _context;

        public PersonRepository(PersonContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Person person)
        {
            try
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var person = await GetById(id);
                if (person == null)
                {
                    return false;
                }

                _context.People.Remove(person);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<Person> GetById(int id)
        {
            var person = await _context.People.FindAsync(id);
            return person;
        }

        public async Task<bool> Update(Person person)
        {
            try
            {
                _context.Update(person);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
