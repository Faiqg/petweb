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
            //read from config at the start of the app
            var httpClient = new HttpClient();
            var url = "https://dorsavicodechallenge.azurewebsites.net/"+(searchCriteria.City??"melbourne");
            var streamTask = httpClient.GetStreamAsync(url);
            return await JsonSerializer.DeserializeAsync<IEnumerable<Owner>>(await streamTask);
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
            return new List<string>() { "melbourne", "sydney" };
        }      
    }
}