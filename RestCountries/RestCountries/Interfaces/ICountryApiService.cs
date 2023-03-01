using RestCountries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCountries.Interfaces
{
    public interface ICountryApiService
    {
        Task<CountryDetails> GetCountryDetailsAsync(int countryCode);
    }
}
