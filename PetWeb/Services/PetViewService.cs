using PetWeb.Models;
using PetData;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PetData.Entities;
using System;

namespace PetWeb.Services
{
    public class PetViewService : IPetViewService
    {
        private readonly PetRepository _petRepository;
        public PetViewService()
        {
            //consider injecting this instead
            _petRepository = new PetRepository ();
        }

        public async Task<IEnumerable<PetsViewModel>> GetPetsAsync(SearchCriteria searchCriteria)
        {
            var allData = await _petRepository.GetAllData(searchCriteria);
            return FilterData(MapPets(allData), searchCriteria);
        }

        private IEnumerable<PetsViewModel> FilterData(IEnumerable<PetsViewModel> petData, SearchCriteria search)
        {
            if (search.PetType == null && search.PetName == null) return petData;
            var result = petData;
            if (search.PetName != null && search.PetName.Length > 0)
            {
                result = petData
                     .Where(x => x.PetName.Contains(search.PetName, StringComparison.OrdinalIgnoreCase));
            }
            if (search.PetType != null && search.PetType.Length > 0)
            {
                result = result.Where(x => search.PetType.Contains(x.PetType, StringComparison.OrdinalIgnoreCase));
            }
            return result;
        }

        public IEnumerable<string> GetPetTypes()
        {
            return _petRepository.GetPetTypes();
        }
        public IEnumerable<string> GetCities()
        {
            return _petRepository.GetCities();
        }
        
        private IEnumerable<PetsViewModel> MapPets(IEnumerable<Owner> ownerList)
        {
            var petsViewModels = new List<PetsViewModel>();
            foreach (var owner in ownerList)
            {
                if (owner.Pets == null) continue;
                foreach (var p in owner.Pets)
                {
                    var petView = new PetsViewModel
                    {
                        PetName = p.Name,
                        PetType = p.PetType,
                        City = owner.City,
                        OwnerAge = owner.Age,
                        OwnerGender = owner.Gender,
                        OwnerName = owner.Name
                    };
                    petsViewModels.Add(petView);
                }
            }
            return petsViewModels;
        }
    }
}
