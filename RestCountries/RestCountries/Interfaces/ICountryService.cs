using PhoneNumbers;
using RestCountries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCountries.Interfaces
{
    public interface ICountryService
    {
        public Task<SalesInfo> GetInfo(PhoneNumber phoneNumber);
    }
}
