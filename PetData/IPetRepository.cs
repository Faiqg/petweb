using PetData.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetData
{
    public interface IPetRepository
    {
        public Task<IEnumerable<Owner>> GetAllData(SearchCriteria searchCriteria);
        public IEnumerable<string> GetPetTypes();
        public IEnumerable<string> GetCities();
    }
}
