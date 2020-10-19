using PetData.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetData
{
    public class PetRepository : IPetRepository
    {
        public PetRepository()
        {

        }
#nullable enable
        public async Task<IEnumerable<Owner>> GetAllData(SearchCriteria searchCriteria)
        {
            var allOwners = new List<Owner>();
            var httpClient = new HttpClient();
            foreach (var url in GetRepoUrls(searchCriteria.City))
            {
                var streamTask = httpClient.GetStreamAsync(url.Value);
                var ownerList = await JsonSerializer.DeserializeAsync<IEnumerable<Owner>>(await streamTask);
                foreach (var owner in ownerList)
                {
                    if (null == owner.Pets) continue;
                    var newOwner = new Owner() { Age = owner.Age, City = url.Key, Name = owner.Name, Gender=owner.Gender, Pets = new List<Pet>() { } };
                    foreach (var pet in owner.Pets)
                    {
                        var newPets = new List<Pet>();
                        if ((searchCriteria.PetType == null && searchCriteria.PetName == null) ||
                            (searchCriteria.PetName != null && pet.Name.Contains(searchCriteria.PetName, StringComparison.CurrentCultureIgnoreCase)) ||
                            (searchCriteria.PetType != null && searchCriteria.PetType.Contains(pet.PetType, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            newPets.Add(pet);
                        }
                        newOwner.Pets = newOwner.Pets.Concat(newPets);
                    }
                    if (newOwner.Pets.Count() > 0)
                        allOwners.Add(newOwner);
                    else
                        continue;
                }
                //Need to analyse and fix the LINQ query to replace the error prone implmentation above
                //if (null != searchCriteria.PetName || null != searchCriteria.PetType)
                //{
                //    var filter = ownerList
                //       .Select(z => new Owner
                //       {
                //           Name = z.Name,
                //           Age = z.Age,
                //           City = url.Key,
                //           Gender = z.Gender,
                //           Pets = z.Pets
                //           .Where(y => y.Name.Contains(searchCriteria.PetName??"") || y.PetType.Contains(searchCriteria.PetType??""))
                //       });
                //    allOwners = allOwners.Concat(filter);
                //}
                //else
                //{
                //    allOwners = allOwners.Concat(ownerList.Select(c => { c.City = url.Key; return c; }));
                //}
            }
            return allOwners.OrderBy(o=>o.Gender);
        }
        
#nullable disable

            public IEnumerable<string> GetPetTypes()
        {
            //read from repository instead
            return new List<string>() { "", "dogs", "fish", "cat" };
        }
        public IEnumerable<string> GetCities()
        {
            //read from repository instead
            return new List<string>() { "", "melbourne", "sydney" };
        }
        private IDictionary<string, string> GetRepoUrls(string region)
        {
            //read from config instead
            string url = "https://dorsavicodechallenge.azurewebsites.net/";
            var result = new Dictionary<string, string>();
            if (null == region || region.Length == 0)
            {
                //read from config instead
                result.Add("melbourne", $"{url}/melbourne");
                result.Add("sydney", $"{url}/sydney" );
            }
            else
            {
                result.Add(region, $"{url}/{region}");
            }
            return result;
        }
        
    }
}