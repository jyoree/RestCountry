using Newtonsoft.Json;
using RestCountries.Interfaces;
using RestCountries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestCountries.Services
{
    public class CountryApiService : ICountryApiService
    {
        private const string BaseUrl = "https://restcountries.com/v2/callingcode/";
        private readonly HttpClient _client;

        public CountryApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<CountryDetails> GetCountryDetailsAsync(int countryCode)
        {
            var countryDetails = new CountryDetails();
            var url = BaseUrl + countryCode.ToString();
            var httpResponse = await _client.GetAsync(url);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<List<RestCountry>>(content);

            var defaultCountry = task.FirstOrDefault();

            if(task != null)
            {
                countryDetails.Name = defaultCountry.name;
                countryDetails.Region = defaultCountry.region;
                countryDetails.FlagUrl = defaultCountry.flag;
                countryDetails.DefaultLang = defaultCountry.languages.FirstOrDefault()?.iso639_1;
                countryDetails.UserCountry = defaultCountry.languages.FirstOrDefault()?.nativeName ?? "English";
            }           

            return countryDetails;
        }
    }
}
