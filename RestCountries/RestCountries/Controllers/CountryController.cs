using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneNumbers;
using RestCountries.Interfaces;
using RestCountries.Models;

namespace RestCountries.Controllers
{
    [Route("api/country")]
    [ApiController]
    public class CountryController : ControllerBase
    {

        private readonly ILogger<CountryController> _logger;
        private readonly ICountryService _countryService;

        public CountryController(ILogger<CountryController> logger, ICountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }

        [HttpGet("info")]
        public async Task<IActionResult> Get([FromQuery] string phoneNumber)
        {
            try
            {
                // Manipulate the string
               string cleanedPhoneNumber = phoneNumber.Trim();
               if(!cleanedPhoneNumber.StartsWith("+"))
               {
                    cleanedPhoneNumber = cleanedPhoneNumber.Insert(0, "+");                    
               }
               PhoneNumber parsedPhoneNumber = IsValidPhoneNumber(cleanedPhoneNumber);
               if(parsedPhoneNumber == null)
                {
                    return BadRequest("Not a valid phone number");
                }
                var result = await _countryService.GetInfo(parsedPhoneNumber);
                result.PhoneNumber = phoneNumber;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"Exception {ex} occurred while adding or removeing event action.");
                return StatusCode(500);
            }
        }

        private PhoneNumber IsValidPhoneNumber(string phoneNumber)
        {
            PhoneNumber validatedNumber = null;
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
                validatedNumber = phoneUtil.Parse(phoneNumber, "GB");
            }
            catch (NumberParseException e)
            {
                _logger?.LogError($"Failed to parse phone number {e.Message}");
            }
            return validatedNumber;
        }
    }
}
