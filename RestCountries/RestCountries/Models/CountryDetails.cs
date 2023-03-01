using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCountries.Models
{
    public class CountryDetails
    {
        public string DefaultLang { get; set; }
        public string Name { get; set; }
        public string UserCountry { get; set; }
        public string Region { get; set; }
        public Uri FlagUrl { get; set; }
    }
}
