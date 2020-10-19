using PetData;
using PetData.Entities;
using PetWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetWeb.Services
{
    public interface IPetViewService
    {
        Task<IEnumerable<PetsViewModel>> GetPetsAsync(SearchCriteria searchCriteria);
        IEnumerable<string> GetPetTypes();
        IEnumerable<string> GetCities();
    }
}
