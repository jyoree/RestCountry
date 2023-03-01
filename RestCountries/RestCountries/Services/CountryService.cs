using Microsoft.Extensions.Logging;
using RestCountries.Interfaces;
using RestCountries.Models;
using PhoneNumbers;
using System.Threading.Tasks;

namespace RestCountries.Services
{
    public class CountryService : ICountryService
    {
        private ILogger<CountryService> _logger;
        UserService _userService;
        ICountryApiService _apiService;

        public CountryService(ILogger<CountryService> logger, UserService  userService, ICountryApiService apiService)
        {
            _logger = logger;
            _userService = userService;
            _apiService = apiService;
        }

        public async Task<SalesInfo> GetInfo(PhoneNumber phoneNumber)
        {
            SalesInfo info = new SalesInfo() { CallingCode = phoneNumber.CountryCode, CountryCode = phoneNumber.CountryCode.ToString() };
            var details = await _apiService.GetCountryDetailsAsync(phoneNumber.CountryCode);
            info.Details = details;
            if(details != null && !string.IsNullOrEmpty(details.DefaultLang))
            {
                info.User = _userService.GetUser(details.DefaultLang);
            }
            else
            {
                info.User = _userService.GetDefaultUser();
            }
           
            return info;
        }

    }
}
