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
            _petRepository = new PetRepository ();
        }

        public async Task<IEnumerable<PetsViewModel>> GetPetsAsync(SearchCriteria searchCriteria)
        {
            var allData = await _petRepository.GetAllData(searchCriteria);
            return MapPets(allData);
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

            foreach (var item in ownerList)
            {
                if (null == item.Pets) continue;
                var petView = new PetsViewModel() 
                {
                    OwnerGender = item.Gender,
                    City = item.City
                };
                foreach (var p in item.Pets)
                {
                    petView.PetName = p.Name;
                }
                petsViewModels.Add(petView);
            }
            return petsViewModels;
        }
    }
}
